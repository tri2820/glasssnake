using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
    public GameObject FoodPrefab;
    public Vector3 center;
    public Vector3 size;

	// Use this for initialization
	void Start () {
        LetSpawnFood();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.T))
            LetSpawnFood();
	}
    public void LetSpawnFood()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
        Instantiate(FoodPrefab, pos, Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
