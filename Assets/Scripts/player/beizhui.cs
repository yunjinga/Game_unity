using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beizhui : MonoBehaviour
{
    public bool iszhuizhu=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy[] enemies = Object.FindObjectsOfType<enemy>();
        enemy1[] enemies1 = Object.FindObjectsOfType<enemy1>();
        enemy2[] enemies2 = Object.FindObjectsOfType<enemy2>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<enemy>().isCatching == true)
            {
                iszhuizhu = true;
            }
        }
        for (int i = 0; i < enemies1.Length; i++)
        {
            if (enemies1[i].GetComponent<enemy1>().isCatching == true)
            {
                iszhuizhu = true;
            }
        }
        for (int i = 0; i < enemies2.Length; i++)
        {
            if (enemies2[i].GetComponent<enemy2>().isCatching == true)
            {
                iszhuizhu = true;
            }
        }
    }
}
