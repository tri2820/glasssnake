using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float Height = 0;
    public float moveup = 0;
    public float speed = 1;
    // Update is called once per frame
    Vector3 temp;
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow))
        {
            temp = transform.position;
            temp.z += speed;
            transform.position = temp;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            temp = transform.position;
            temp.z -= speed;
            transform.position = temp;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            temp = transform.position;
            temp.x -= speed;
            transform.position = temp;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            temp = transform.position;
            temp.x += speed;
            transform.position = temp;
        }
        if (Height==0)
        if (Input.GetKey(KeyCode.Space))
        {
                moveup = 1;Height = 1;
        }
        if (moveup==1)
        {
            temp = transform.position;
            temp.y += (float)(0.1);
            transform.position = temp;
            if (temp.y >= 1) moveup = -1;
        }
        if (moveup==-1)
        {
            temp = transform.position;
            temp.y -= (float)(0.1);
            transform.position = temp;
            if (temp.y <= 0) { temp.y = 0; transform.position = temp; moveup = 0; Height = 0; }
        }
    }
}
