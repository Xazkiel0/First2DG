using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    int incomingEnemy = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            float nearEnemy = Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectsWithTag("Enemy")[incomingEnemy].transform.position);
            if (nearEnemy < 11f && nearEnemy > 3f)
            {

                print("OK Pas tuh");
            }
        }


    }
}
