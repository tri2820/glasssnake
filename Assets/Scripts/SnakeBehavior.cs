using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeBehavior : MonoBehaviour {

    public List<Transform> Body = new List<Transform>();

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


    void Start () {
        StartGame(initLength);
    }

    void StartGame(int length)
    {   
        initLength = length;
        Debug.Log("You called StartGame! initLength = " + initLength);

        SpawnFood foodController = platform.GetComponent<SpawnFood>();   
        foodController.LetDestroyFood();
        foodController.LetSpawnFood();

        deadScreen.SetActive(false);
        timeLastPlay = Time.time;
        
        ifAlive = true;


        setBodyLength(initLength);
        // The first element in Body is "Head", which can be set using unity-editor
        Body[0].position = beginPosition;
        Body[0].rotation = beginRotation;

        currentScore.gameObject.SetActive(true);
        UpdateScore();
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
            UpdateScore();
        }

        if (collision.gameObject.tag == "Obstacle") Die();
    }
    

    // Update is called once per frame
    void Update () {
        if (ifAlive) Move();
        if (Input.GetKey(KeyCode.Q)) AddBody();
        if (Input.GetKey(KeyCode.R)) StartGame(initLength);
        if (Input.GetKey(KeyCode.P)) Die();
    }


    public void Move(){
        float realspeed = speed;
        transform.rotation = beginRotation;
        transform.position = beginPosition;
        if (Input.GetKey(KeyCode.UpArrow))
            realspeed = speed * 2;
        if (Input.GetKey(KeyCode.DownArrow))
            realspeed = speed / 2;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Body[0].rotation = Body[0].rotation * Quaternion.Euler(0, rotateSpeed, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Body[0].rotation = Body[0].rotation * Quaternion.Euler(0, -rotateSpeed, 0);
        }
        Vector3 temp = Body[0].rotation.eulerAngles;
        temp.x = 0; temp.z = 0;
        Body[0].rotation = Quaternion.Euler(temp);
        Body[0].Translate(Body[0].forward * realspeed * Time.smoothDeltaTime, Space.World);

        
        for (int i=1;i<Body.Count;i++)
        {
            Transform curbody = Body[i];
            Transform prebody = Body[i - 1];
            float dist = Vector3.Distance(curbody.position, prebody.position);
            float ripdist = Vector3.Distance(curbody.position, Body[0].position);
            if (Time.time - timeLastPlay > 5)
                if (i > 1)
                    if (ripdist < 1) Die();
            Vector3 npos = prebody.position;
            if (dist < minDistance) continue;
            curbody.position = Vector3.Slerp(curbody.position, npos, Time.smoothDeltaTime * realspeed*1.5f);
            curbody.rotation = Quaternion.Slerp(curbody.rotation, prebody.rotation, Time.deltaTime * realspeed);
        }
        
    }
    
    public void setBodyLength(int n)
    {
        while (Body.Count < n) AddBody();
        while (Body.Count > n) RemoveBody();
        Debug.Log("Reset body length to " + Body.Count);
    }

    public void AddBody(){
        Transform newBlock = (Instantiate(Bodyprefab, Body[Body.Count - 1].position, Body[Body.Count - 1].rotation) as GameObject).transform;
        newBlock.SetParent(transform);
        Body.Add(newBlock);
    }

    public void RemoveBody(){
        Transform lastBlock = Body[Body.Count -1];
        Destroy(lastBlock.gameObject);
        Body.Remove(lastBlock);
    }

    public void UpdateScore(){
        currentScore.text = "Score: " + (Body.Count - initLength).ToString();
    }

    public void Die()
    {
        ifAlive = false;
        currentScore.gameObject.SetActive(false);
        deadScreen.SetActive(true);
        scoreText.text = "your score was: " + (Body.Count - initLength).ToString();
    }
}
