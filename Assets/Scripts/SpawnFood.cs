using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
    public Vector3 center;
    public Vector3 size;
    public GameObject FoodPrefab;

	// Use this for initialization
	void Start () {
        LetSpawnFood();
        Debug.Log("This SpawnFood script is attached to " + this);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.T))
            LetSpawnFood();
	}

    public void LetSpawnFood()
    {
        Debug.Log("You spawned Food!");
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
        Instantiate(FoodPrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    // public class SpawnFood_Controlller{
    //     void foo(){
    //         Debug.Log("Called Foo");
    //     }
    // }

    public static void bar(){
        Debug.Log("Called Bar");
        
        // LetSpawnFood();
    }


}

