using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeBehavior : MonoBehaviour {

    public List<Transform> body = new List<Transform>();
    public Vector3 Headposition;
    // Bodyprefab is set to Sphere prefab, which can be set in unity-editor
    public GameObject Bodyprefab;

    // Moving parameters
    public float speed = 1;
    public Vector3 beginPosition = new Vector3(0, 0, 0);
    public Quaternion beginRotation = Quaternion.identity;
    public float rotateSpeed = 1;

    // Init snake parameters
    public int initLength = 10;
    public bool ifColided = false;
    public float minDistance = 0.5f;

    // Init game parameters, which can be set using unity-editor
    // deadScreen can call StartGame with initLength = 3
    public GameObject deadScreen;
    // Score to display in deadscreen
    public Text scoreText;
    public float timeLastPlay;
    public bool ifAlive;
    public Text currentScore;
    public GameObject platform;
    bool flagDie = false;
    public SpawnFood foodController;
    void Start () {
        foodController = platform.GetComponent<SpawnFood>();
        StartGame(initLength);
    }

    public void StartGame(int length)
    {   
        initLength = length;
        Debug.Log("You called StartGame! initLength = " + initLength);
        flagDie = false;


        foodController.LetDestroyFood();
        foodController.LetSpawnFood();

        deadScreen.SetActive(false);
        timeLastPlay = Time.time;
        
        ifAlive = true;
        setBodyLength(1); // Destroy all the body before start newgame;
        transform.rotation = beginRotation;
        transform.position = beginPosition;
        body[0].position = new Vector3(0,0,0);
        body[0].rotation = Quaternion.identity;
        Headposition = body[0].gameObject.transform.position;
        setBodyLength(initLength);
        // The first element in body is "Head", which can be set using unity-editor
       
        currentScore.gameObject.SetActive(true);
    }

    // I changed the "Layer" of the "Head" in "SNAKE" to SNAKE (using unity editor)
    // So that if a collison happens in the Head, it will pass that signal to the nearest Rigidbody, in this case it is the "SNAKE"'s Rigidbody 
    // I also turned off the Sphere Collider in Sphere Prefab (using unity-editor)
    // So that if the tail touches the food it DO NOT destroy the food
    void OnCollisionEnter(Collision collision)
    {
        ifColided = true;
        // Create an instance, which links to SpawnFood.cs script
        // Because SpawnFood.cs script is attached to "Camera", we find "Camera" and get the component
        SpawnFood foodController = platform.GetComponent<SpawnFood>();   

        Debug.Log("You collided with " + collision.gameObject);
        
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            AddBody();
            foodController.LetSpawnFood();
        }

        if (collision.gameObject.tag == "Obstacle") ifAlive=false;
    }
    

    // Update is called once per frame
    void Update () {
        transform.rotation = beginRotation;
        transform.position = beginPosition;
        if (ifAlive) Move();
        else Die();
        if (Input.GetKey(KeyCode.Q)) AddBody();
        if (Input.GetKey(KeyCode.R)) StartGame(initLength);
        if (Input.GetKey(KeyCode.P)) Die();
        UpdateScore();
        
    }


    public void Move(){
        float realspeed = speed;
        if (Input.GetKey(KeyCode.UpArrow))
            realspeed = speed * 2;
        if (Input.GetKey(KeyCode.DownArrow))
            realspeed = speed / 2;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body[0].rotation = body[0].rotation * Quaternion.Euler(0, rotateSpeed, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body[0].rotation = body[0].rotation * Quaternion.Euler(0, -rotateSpeed, 0);
        }
        Vector3 temp = body[0].rotation.eulerAngles;
        temp.x = 0; temp.z = 0;
        body[0].rotation = Quaternion.Euler(temp);
        body[0].Translate(body[0].forward * realspeed * Time.smoothDeltaTime, Space.World);

        
        for (int i=1;i<body.Count;i++)
        {
            Transform curbody = body[i];
            Transform prebody = body[i - 1];
            float dist = Vector3.Distance(curbody.position, prebody.position);
            float ripdist = Vector3.Distance(curbody.position, body[0].position);
            if (Time.time - timeLastPlay > 5)
                if (i > 1)
                    if (ripdist < 1) ifAlive=false;
            Vector3 npos = prebody.position;
            if (dist < minDistance) continue;
            curbody.position = Vector3.Slerp(curbody.position, npos, Time.smoothDeltaTime * realspeed*1.5f);
            curbody.rotation = Quaternion.Slerp(curbody.rotation, prebody.rotation, Time.deltaTime * realspeed);
        }
        
    }
    
    public void setBodyLength(int n)
    {
        while (body.Count < n) AddBody();
        while (body.Count > n) RemoveBody();
        Debug.Log("Reset body length to " + body.Count);
    }

    public void AddBody(){
        Transform newBlock = (Instantiate(Bodyprefab, body[body.Count - 1].position, body[body.Count - 1].rotation) as GameObject).transform;
        newBlock.SetParent(transform);
        body.Add(newBlock);
    }

    public void RemoveBody(){
        Transform lastBlock = body[body.Count -1];
        Destroy(lastBlock.gameObject);
        body.Remove(lastBlock);
    }

    public void UpdateScore(){
        Debug.Log("Update score!");
        currentScore.text = "Score: " + (body.Count - initLength).ToString();
    }

    public void Die()
    {
        ifAlive = false;
        currentScore.gameObject.SetActive(false);
        deadScreen.SetActive(true);
        scoreText.text = "your score was: " + (body.Count - initLength).ToString();
    }
}
