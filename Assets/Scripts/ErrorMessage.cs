using UnityEngine;
using System.Collections;

public class ErrorMessage : MonoBehaviour {

    public UnityEngine.UI.Text text;
	// Use this for initialization
	void Start () {
        text.text = MainGame.errorMessage;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
