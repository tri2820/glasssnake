using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeBehavior : MonoBehaviour {

    public List<Transform> Body = new List<Transform>();
    public GameObject Bodyprefab;
    public GameObject Headprefab;
    public float speed = 1;
    public Vector3 beginposition = new Vector3(0, 0, 0);
    public Quaternion beginrotation = Quaternion.identity;
    public float rotatespeed = 1;
    public int inti = 1;
    public float mindist = 0.5f;
    public GameObject DeadScreen;
    public float TimeLastPlay;
    public Text CurrentScore;
    public Text ScoreText;
    public bool IfAlive;
    private Transform curbody;
    private Transform prebody;
    public int behavior=0;
    public float bhrate=0;
	// Use this for initialization
	void Start () {
        StartGame(inti);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (IfAlive)
            move();
        if (Input.GetKey(KeyCode.Q)) AddBody();
        if (Input.GetKey(KeyCode.R)) StartGame(inti);
	}

    void StartGame(int x)
    {
        DeadScreen.SetActive(false);
        TimeLastPlay = Time.time;
        inti = x;
        IfAlive = true;
        for (int i = Body.Count-1; i > 0; i--)
        {
            Destroy(Body[i].gameObject);
            Body.Remove(Body[i]);
        }
        
        Body[0].position = beginposition;
        Body[0].rotation = beginrotation;
        CurrentScore.gameObject.SetActive(true);
        CurrentScore.text = "Score : 0";
        for (int i = 0; i < inti - 1; i++) AddBody();

    }

    public void move(){
        float realspeed = speed;
        if (Input.GetKey(KeyCode.UpArrow))
            realspeed = speed * 2;
        if (Input.GetKey(KeyCode.DownArrow))
            realspeed = speed / 2;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Body[0].rotation = Body[0].rotation * Quaternion.Euler(0, rotatespeed, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Body[0].rotation = Body[0].rotation * Quaternion.Euler(0, -rotatespeed, 0);
        }
        Body[0].Translate(Body[0].forward * realspeed * Time.smoothDeltaTime, Space.World);
        Vector3 temp = Body[0].rotation.eulerAngles;
        temp.x = 0;temp.z = 0;
        Body[0].rotation = Quaternion.Euler(temp);
        
        for (int i=1;i<Body.Count;i++)
        {
            curbody = Body[i];
            prebody = Body[i - 1];
            float dist = Vector3.Distance(curbody.position, prebody.position);
            

            Vector3 npos = prebody.position;
            if (dist < mindist) continue;
            curbody.position = Vector3.Slerp(curbody.position, npos, Time.deltaTime * realspeed*2);
            curbody.rotation = Quaternion.Slerp(curbody.rotation, prebody.rotation, Time.deltaTime * realspeed*2);
            

        }
        
    }
    
    public void AddBody()
    {
        Transform newbody = (Instantiate(Bodyprefab, Body[Body.Count - 1].position, Body[Body.Count - 1].rotation) as GameObject).transform;
        newbody.SetParent(transform);
        Body.Add(newbody);
        CurrentScore.text = "Score: " + (Body.Count - inti).ToString();
    }
    public void DIE()
    {
  
        IfAlive = false;
        ScoreText.text = "your score was: " + (Body.Count - inti).ToString();
        CurrentScore.gameObject.SetActive(false);
        DeadScreen.SetActive(true);
    }
}
