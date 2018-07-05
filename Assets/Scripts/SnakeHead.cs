using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {
    public SnakeBehavior movement;
    public SpawnFood SF;
    public float col = 0;

    private void OnCollisionEnter(Collision collision)
    {
        col = 1;
        if (collision.gameObject.tag == "Food") 
        {
            movement.AddBody();
            Destroy(collision.gameObject);
            SF.LetSpawnFood();
        }
        else
        {
            if (collision.transform!=movement.Body[1].transform && movement.IfAlive)
            {
                if (Time.time - movement.TimeLastPlay > 5) movement.DIE();
            }
        }
    }
}
