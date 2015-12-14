using UnityEngine;
using System.Collections;

public class PlayGameClick : MonoBehaviour {

    bool playGamePressed;
    // Use this for initialization
    void Start()
    {
        playGamePressed = false;
    }

    // Update is called once per frame
    void Update () {
	    if(playGamePressed)
        {
            Application.LoadLevel("PlayGame");
        }
	}

    void CheckPlayGamePressed()
    {
        playGamePressed = true;
    }
}
