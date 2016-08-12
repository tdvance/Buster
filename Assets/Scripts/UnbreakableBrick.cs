using UnityEngine;
using System.Collections;

public class UnbreakableBrick : MonoBehaviour {
    public AudioClip bounce;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(bounce, transform.position, 2.0f);

    }
}
