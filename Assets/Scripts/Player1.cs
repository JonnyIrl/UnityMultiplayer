using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {
    Transform thisTransform;
    public float speed;

    // Use this for initialization
    void Start () {
        thisTransform = transform;
    }
	
	// Update is called once per frame
	void Update () {
        //if (GetComponent<NetworkView>().isMine)
        //{
        if (Client.getPlayer() == 1)
        {
            InputMovement();
        }
        //}

        /*if (Client.getPlayer() == 1)
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
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed;
        }

        thisTransform.position = pos;
    }

    public void setPosition(int x , int y)
    {  
        this.transform.position = new Vector3(x,y,0);
    }
}
