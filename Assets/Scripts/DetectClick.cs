using UnityEngine;
using System.Collections;

public class DetectClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void RestartClick()
    {
        Application.LoadLevel("PlayGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

   
}
