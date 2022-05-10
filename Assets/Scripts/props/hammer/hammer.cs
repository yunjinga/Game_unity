using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer : MonoBehaviour
{
    public Vector3 tp = new Vector3(43.67f, -4.768372e-07f, 11.85466f);
    private void Start()
    {
        //Debug.Log(transform.position);
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(collision.gameObject.GetComponent<knapsack>().isFull);
        if (collision.gameObject.GetComponent<knapsack>())
        {
            if (collision.gameObject.GetComponent<knapsack>().isFull == false)
            {
                if (collision.gameObject.GetComponent<knapsack>().game[0] == string.Empty)
                {
                    collision.gameObject.GetComponent<knapsack>().game[0] = "hammer";
                }
                else if (collision.gameObject.GetComponent<knapsack>().game[0] != string.Empty)
                {
                    collision.gameObject.GetComponent<knapsack>().game[1] = "hammer";

                }
                // Destroy(gameObject);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.GetComponent<walkto>() && transform.position != tp)//如果钳子没在原地会去捡起来
        {
            //Debug.Log(collision.gameObject.GetComponent<walkto>());
            collision.gameObject.GetComponent<walkto>().position = tp;
            collision.gameObject.GetComponent<walkto>().isWalkto = true;
            collision.gameObject.GetComponent<walkto>().isHammer = true;
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log(collision.gameObject.GetComponent<walkto>());
        }
    }
}