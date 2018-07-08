using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunlight : MonoBehaviour {
    public Vector3 center;
    public Vector3 beginPosition;
    public Vector3 endPosition;
    public float speed = 10f;
    public float dayend = 0;
    public bool ifday = true;
    public float retime;

    // light is set using unity-editor
    public GameObject light;
	// Update is called once per frame
	void Update () {
        retime = Time.time;
        Vector3 temp;
        temp.x = beginPosition.x - (beginPosition.x - endPosition.x) * (Time.time - dayend) / speed;
        temp.y = beginPosition.y - (beginPosition.y - endPosition.y) * (Time.time - dayend) / speed;
        temp.z = beginPosition.z - (beginPosition.z - endPosition.z) * (Time.time - dayend) / speed;
        if (ifday) light.transform.position = temp;
        if (Time.time - dayend > speed)
            if (!ifday)
        {
            dayend = Time.time;
            light.transform.position = beginPosition;
            ifday = true;
            light.SetActive(true);
        }
        else
        {
            dayend = Time.time;
            ifday = false;
            light.SetActive(false);
        }
        
	}
}
