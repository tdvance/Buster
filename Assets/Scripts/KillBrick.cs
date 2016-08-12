using UnityEngine;
using System.Collections;

public class KillBrick : MonoBehaviour
{
    public int strength = 1;
    public AudioClip die;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(die, transform.position);

        if (strength > 0)
        {
            strength--;
            if (strength <= 0)
            {
                Debug.Log("Kill!");
                Ball ball = GameObject.FindObjectOfType<Ball>();
                ball.destroy();
                Paddle paddle = GameObject.FindObjectOfType<Paddle>();
                paddle.destroy();
                MainGame.loadLevel("Lose", 2.5f);
            }
        }
    }
}
