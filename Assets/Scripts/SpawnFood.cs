using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
    public Vector3 center;
    public Vector3 size;
    public GameObject FoodPrefab;
     public List<GameObject> listOfFood = new List<GameObject>();

	// Use this for initialization
	void Start () {
        Debug.Log("This SpawnFood script is attached to " + this);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.T))
            LetSpawnFood();
	}

    public void LetSpawnFood()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
        GameObject newFood = Instantiate(FoodPrefab, pos, Quaternion.identity);
        listOfFood.Add(newFood);
        Debug.Log("You spawned Food!");
    }

    public void LetDestroyFood()
    {
        foreach (var food in listOfFood) {
            Destroy(food);    
        }

        listOfFood.Clear();

        Debug.Log("You destroyed all the Food! Food left: " + listOfFood.Count);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }



}

