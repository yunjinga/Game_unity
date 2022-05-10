using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class daojishi : MonoBehaviour
{
    public Slider s1;
    public GameObject player;
    float x = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        float i = player.GetComponent<knapsack>().timeUse[0];
        float j = player.GetComponent<knapsack>().timeUse[1];
        
        
            s1.value = 0;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float i = player.GetComponent<knapsack>().timeUse[0];
        float j= player.GetComponent<knapsack>().timeUse[1];
        if((player.GetComponent<knapsack>().game[0] != string.Empty|| player.GetComponent<knapsack>().game[1] != string.Empty)&& (i==10||j==10))
        { 
             x -= Time.deltaTime * 0.1f;
             s1.value = x;
        //    if (player.GetComponent<knapsack>().game[0] == "whistle"|| player.GetComponent<knapsack>().game[1] == "whistle" )
        //{
           
        //    player.GetComponent<knapsack>().isUse_Whistle = true;
        //}
        }
        
        
        //if (s1.value == 0)
        //{
        //    player.GetComponent<knapsack>().isUse_Whistle = false;
        //}
    }
}
