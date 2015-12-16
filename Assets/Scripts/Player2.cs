using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {
    Transform thisTransform;
    public float speed;

    // Use this for initialization
    void Start () {
        thisTransform = transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (Client.getPlayer() == 2)
        {
            InputMovement();
        }
        /*if (Client.getPlayer() == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector3 position = this.transform.position;
                position.x--;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector3 position = this.transform.position;
                position.x++;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector3 position = this.transform.position;
                position.y++;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector3 position = this.transform.position;
                position.y--;
                this.transform.position = position;
            }
        }*/

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
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Client.moving = false;
        }
    }

    public void setPosition(int x, int y)
    {
        this.transform.position = new Vector3(x, y, 0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("CAUGHT");
    }
}
