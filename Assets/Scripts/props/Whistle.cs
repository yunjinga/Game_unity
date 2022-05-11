using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<knapsack>())
        {
            if (collision.gameObject.GetComponent<knapsack>().isFull == false)
            {
                if (collision.gameObject.GetComponent<knapsack>().game[0] == string.Empty)
                {
                    
                    collision.gameObject.GetComponent<knapsack>().game[0] = "whistle";
                    
                }
                else if (collision.gameObject.GetComponent<knapsack>().game[0] != string.Empty)
                {
                    collision.gameObject.GetComponent<knapsack>().game[1] = "whistle";
                   

                }
                // Destroy(gameObject);
                Destroy(gameObject);
            }
        }
    }
   
}
