using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player1 : MonoBehaviour {
    Transform thisTransform;
    public float speed;
    public bool moving;

    int time;
    int count;
    public Text timerText;

    // Use this for initialization
    void Start () {
        thisTransform = transform;
        moving = false;
        count = 0;
        time = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Client.getPlayer() == 1)
        {
            InputMovement();
        }

        time ++;
        if (time % 60 == 1)
        {
            count += 1;
        }
        timerText.text = "Timer : " + count;
    }

    private void InputMovement()
    {
        Vector2 pos = thisTransform.position;
        if (Input.GetKey(KeyCode.W))
        {
            pos.y += speed;
            Client.moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed;
            Client.moving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed;
            Client.moving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed;
            Client.moving = true;
        }

        thisTransform.position = pos;
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Client.moving = false;
        }
    }

    public void setPosition(int x , int y)
    {  
        this.transform.position = new Vector3(x,y,0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "BlueBall" || coll.gameObject.tag == "RedBall")
        {
            Application.LoadLevel("ExitScene");
            Debug.Log("SUPPOSED TO GO TO EXIT SCENE");
        }

        if (coll.gameObject.CompareTag("BlueBall") || coll.gameObject.CompareTag("RedBall"))
        {
            Application.LoadLevel("ExitScene");
            Debug.Log("SUPPOSED TO GO TO EXIT SCENE");
        }
    }
}
