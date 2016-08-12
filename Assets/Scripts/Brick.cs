using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
    public int strength = -1;
    public Sprite shadow;
    public AudioClip brickhit0;
    public AudioClip brickhit1;
    public AudioClip brickhit2;
    public GameObject smoke;

    public Sprite cracked_brick;
    public Sprite broke_brick;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(strength > 0)
        {
            strength--;
            switch (strength)
            {
                case 0:
                    AudioSource.PlayClipAtPoint(brickhit0, transform.position, 2.0f);
                    break;
                case 1:
                    AudioSource.PlayClipAtPoint(brickhit1, transform.position, 0.5f);
                    this.GetComponent<SpriteRenderer>().sprite = broke_brick;
                    break; 
                default:
                    AudioSource.PlayClipAtPoint(brickhit2, transform.position, 0.5f);
                    this.GetComponent<SpriteRenderer>().sprite = cracked_brick;

                    break;
            }
            if (strength <= 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = shadow;
                this.GetComponent<BoxCollider2D>().enabled = false;
                GameObject puff = Instantiate(smoke, this.transform.position, Quaternion.identity) as GameObject;
                puff.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
                gameObject.tag = "Killed";
                if (!GameObject.FindGameObjectWithTag("GameBrick"))
                {
                    Ball ball = GameObject.FindObjectOfType<Ball>();
                    ball.destroy();
                    MainGame.noMoreBricks();
                }else
                {
                    //Debug.Log("Found: " + GameObject.FindGameObjectWithTag("GameBrick"));
                }
               
            }
        }
    }
}
