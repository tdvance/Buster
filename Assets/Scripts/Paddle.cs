using UnityEngine;
using System.Collections;
using System;

public class Paddle : MonoBehaviour {
    private float width = 16f;
    private float y = 0;
    private float z = 0;
    private float paddlewidth = 1.5f;
    private Ball ball;
    public AudioClip bounce;


    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(bounce, transform.position, 2.0f);
    }


    // Use this for initialization
    void Start () {
        y = transform.position.y;
        z = transform.position.z;
        ball = GameObject.FindObjectOfType<Ball>();
    }
	
	// Update is called once per frame
	void Update () {
        float x;
        if (MainGame.getInstance().autoplay)
        {
            x = moveWithBall();
        }
        else
        {
             x = moveWithMouse();
        }
        transform.position = new Vector3(x, y, z);
	}

    private float moveWithBall()
    {
        if (ball)
        {
            return Mathf.Clamp(ball.transform.position.x,  paddlewidth/2f + 0.5f, width - paddlewidth/2f - 0.5f);
        }else
        {
            return this.transform.position.x;
        }

    }

    private float moveWithMouse()
    {
        return Mathf.Clamp(Input.mousePosition.x / Screen.width * width, paddlewidth / 2f + 0.5f, width - paddlewidth / 2f - 0.5f);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
