using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public float speed = 1;
    public Transform target;
    public Camera cam;
    
    
	void LateUpdate () {
        Move();
	}
    
    public void Move()
    {
              
        transform.position = Vector3.Slerp(transform.position, target.position, Time.deltaTime * speed);
        transform.rotation = target.rotation;

    }
}
