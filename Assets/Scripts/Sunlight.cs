using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunlight : MonoBehaviour {
    public Vector3 Center;
    public Vector3 Beginposition;
    public Vector3 Endposition;
    public float speed = 10f;
    public float dayend = 0;
    public bool ifday = true;
    public float retime;
    public GameObject Light;
	// Update is called once per frame
	void Update () {
        retime = Time.time;
        Vector3 temp;
        temp.x = Beginposition.x - (Beginposition.x - Endposition.x) * (Time.time - dayend) / speed;
        temp.y = Beginposition.y - (Beginposition.y - Endposition.y) * (Time.time - dayend) / speed;
        temp.z = Beginposition.z - (Beginposition.z - Endposition.z) * (Time.time - dayend) / speed;
        if (ifday) Light.transform.position = temp;
        if (Time.time - dayend > speed)
            if (!ifday)
        {
            dayend = Time.time;
            Light.transform.position = Beginposition;
            ifday = true;
            Light.SetActive(true);
        }
        else
        {
            dayend = Time.time;
            ifday = false;
            Light.SetActive(false);
        }
        
	}
}
