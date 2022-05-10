using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cunzhishu : MonoBehaviour
{
    Animator ator;
    enemy[] enemies;
    enemy1[] enemies1;
    enemy2[] enemies2;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindObjectsOfType<enemy>();
        enemies1 = GameObject.FindObjectsOfType<enemy1>();
        enemies2 = GameObject.FindObjectsOfType<enemy2>();
        ator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0); 
        
        if ((info.IsName("push")))
        {
            //Debug.Log(1111);
            for (int i = 0; i < enemies.Length; i++)
            {
                //enemies[i].waring = enemy.midleWaring;
                enemies[i].waring = 20;
            }
            for (int i = 0; i < enemies1.Length; i++)
            {
                //enemies1[i].waring = enemy.midleWaring;
                enemies1[i].waring = 20;
            }
            for (int i = 0; i < enemies2.Length; i++)
            {
                //enemies2[i].waring = enemy.midleWaring;
                enemies2[i].waring = 20;
            }
        }
    }
}
