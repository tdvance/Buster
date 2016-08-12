using UnityEngine;
using System.Collections;
using System;

public class Ball : MonoBehaviour {
    private Paddle paddle = null;
    private float y = 0;
    private float z = 0;
    public float initial_vel_x = 2.0f;
    public float initial_vel_y = 7.5f;
    public float min_velocity = 5.5f;
    public float max_velocity = 14.0f;

    private static float min_velocity2 = 5.5f * 5.5f;
    private static float max_velocity2 = 14.0f * 14.0f;
    private static float min_slope = 0.5f;
    private static float max_slope = 4.0f;
    private Rigidbody2D body = null;

    private float minx = 0.75f;
    private float maxx = 15.25f;
    private float miny = 0.75f;
    private float maxy = 11.25f;

    private bool moving = false;

    public bool running = false;
    float delay = -1f;

    // Use this for initialization
    void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        if (paddle)
        {
            y = paddle.transform.position.y + 0.25f;
            z = paddle.transform.position.z;
        }
        body = gameObject.GetComponent<Rigidbody2D>();
        min_velocity2 = min_velocity * min_velocity;
        max_velocity2 = max_velocity * max_velocity;
        delay = 3.0f;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (running)
        {
            body.velocity = adjust_velocity(body.velocity);
            //Debug.Log("velocity = " + body.velocity + ": " + Mathf.Sqrt(body.velocity.x* body.velocity.x+ body.velocity.y* body.velocity.y));        
        }
    }


    // Update is called once per frame
    void Update () {
        if (running)
        {
            if (!moving)
            {
                launch();
            }else
            {
                if(body.position.x < minx - 1f || body.position.x > maxx + 1f || body.position.y < miny - 1f || body.position.y > maxy + 1f)
                {
                    float x = Mathf.Clamp(body.position.x, minx, maxx);
                    float y = Mathf.Clamp(body.position.y, miny, maxy);
                    body.position = new Vector2(x, y);

                }
            }
        } else if (MainGame.getInstance().autoplay) {
            if (delay > 0)
            {

                delay -= Time.deltaTime;
            }
            else
            {
                launch();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            launch();
        }
        else
        {
            float x = moveWithPaddle();
            transform.position = new Vector3(x, y, z);
        }


    }

    private void launch()
    {
        running = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(initial_vel_x, initial_vel_y);
        moving = true;
    }

    private float moveWithPaddle()
    {
        return paddle.transform.position.x;
    }

    Vector2 adjust_velocity(Vector2 velocity)
    {
        float x = velocity.x;
        float y = velocity.y;
        x += UnityEngine.Random.Range(0f, 0.05f);
        y += UnityEngine.Random.Range(0f, 0.05f);

        if (x == 0)
        {
            x = 0.1f;
        }
        if (y == 0)
        {
            y = 0.1f;
        }
        float slope = y / x;
        if (slope > max_slope)
        {
            x *= slope / max_slope;
        }
        else if (-slope > max_slope)
        {
            x *= -slope / max_slope;
        }
        else if (-min_slope < slope && slope < min_slope)
        {
            x *= Mathf.Abs(slope) / min_slope;
        }
        float d = x * x + y * y;
        if (d < min_velocity2)
        {
            float f = Mathf.Sqrt(min_velocity2 / d);
            x *= f;
            y *= f;
        }
        else if (d > max_velocity2)
        {
            float f = Mathf.Sqrt(max_velocity2 / d);
            x *= f;
            y *= f;
        }

        return new Vector2(x, y);
    }

}
