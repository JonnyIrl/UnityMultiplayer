using UnityEngine;
using System.Collections;

public class ButtonDisable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisableButton()
    {
        this.transform.position = new Vector3(-10000,50000,0);
    }
}
