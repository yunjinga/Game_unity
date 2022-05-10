using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    //private MeshRenderer mat;
    //private Vector4 v41;
    //private float v = 0;//颜色值这里随便默认，主要看shader里的
    //private float v1 = 0;
    //private int flag = 0;
    //private float temp_clip = 0;
    //public Vector4 Scolor = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
    //public static Material material;
    /*GameObject enemy;
    GameObject enemy1;
    GameObject enemy2;
    GameObject enemy3;
    GameObject enemy4;
    GameObject enemy5;
    GameObject secondMesh;
    GameObject secondMesh1;
    GameObject secondMesh2;
    GameObject secondMesh3;
    GameObject secondMesh4;
    GameObject secondMesh5;*/
    enemy[] enemies;
    enemy1[] enemies1;
    enemy2[] enemies2;
    void Awake()
    {
        //动态赋予材质
        //material = new Material(Shader.Find("Custom/changeColor"));
        //material.color = Color.green;
        //GetComponent<Renderer>().material = material;

    }
    void Start()
    {
        enemies = Object.FindObjectsOfType<enemy>();
        enemies1 = Object.FindObjectsOfType<enemy1>();
        enemies2 = Object.FindObjectsOfType<enemy2>();

        //v = material.GetFloat("_Clip");
        //v1 = material.GetFloat("_Clip2");
        //1 left到right 2 up到down 3 forward到back
        //material.SetFloat("_Mode", 3.0f);//颜色变化方向
        /*enemy = GameObject.Find("1_1/Enemy1/NPC01_Apose (1) Variant");
        secondMesh = GameObject.Find("1_1/Enemy1/NPC01_Apose (1) Variant/secondMesh");

        enemy1 = GameObject.Find("1_2/Enemy2/NPC01_Apose (1) Variant");
        secondMesh1 = GameObject.Find("1_2/Enemy2/NPC01_Apose (1) Variant/secondMesh");

        enemy2 = GameObject.Find("1_2/Enemy2/NPC01_Apose (2) Variant");
        secondMesh2 = GameObject.Find("1_2/Enemy2/NPC01_Apose (2) Variant/secondMesh");

        enemy3 = GameObject.Find("1_3/Enemy3/NPC01_Apose (2) Variant");
        secondMesh3 = GameObject.Find("1_3/Enemy3/NPC01_Apose (2) Variant/secondMesh");

        enemy4 = GameObject.Find("1_3/Enemy3/NPC01_Apose (3) Variant");
        secondMesh4 = GameObject.Find("1_3/Enemy3/NPC01_Apose (3) Variant/secondMesh");

        enemy5 = GameObject.Find("NPC_02_find Variant");
        secondMesh5 = GameObject.Find("NPC_02_find Variant/secondMesh");*/
    }
    void Update()
    {

        //v = v - 0.001f;
        //material.SetFloat("_Clip", v);//颜色变化比例

        threeColorChange();
        //print(v);

    }
    /*
    version 1.0
    void threeColorChange()
    {
        temp_clip = material.GetFloat("_Clip");
        if (temp_clip <= 3.0f)
        {
            v = v + 0.01f;
            material.SetFloat("_Clip", v);
        }
        else if (temp_clip >3.0f) //如果绿变黄已经完成
        {
            material.SetFloat("_Clip", 10.0f); //控制变量_Clip
            v1 = v1 + 0.01f;
            material.SetFloat("_Clip2", v1);
        }
    }
    */
    void threeColorChange()
    {
        foreach (var c in enemies)
        {

            if (c.waring >= c.midleWaring && c.waring <= c.maxWaring)
            {
                //Debug.Log("enemy " + c.name);
                c.material.SetFloat("_Clip", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip", 3.0f);
            }
            else if (c.waring > c.maxWaring)
            {

                c.material.SetFloat("_Clip", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip", 3.0f);
                c.material.SetFloat("_Clip2", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip2", 3.0f);


            }
        }
        foreach (var c in enemies1)
        {
            if (c.waring >= c.midleWaring && c.waring <= c.maxWaring)
            {
                //Debug.Log("enemy1 " + c.name);
                c.material.SetFloat("_Clip", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip", 3.0f);
            }
            else if (c.waring > c.maxWaring)
            {

                c.material.SetFloat("_Clip", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip", 3.0f);
                c.material.SetFloat("_Clip2", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip2", 3.0f);


            }
        }
        foreach (var c in enemies2)
        {
            if (c.waring >= c.midleWaring && c.waring <= c.maxWaring)
            {
                //Debug.Log("enemy2 " + c.name);
                c.material.SetFloat("_Clip", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip", 3.0f);
            }
            else if (c.waring > c.maxWaring)
            {

                c.material.SetFloat("_Clip", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip", 3.0f);
                c.material.SetFloat("_Clip2", 3.0f);
                c.GetComponentInChildren<secondMesh>().material.SetFloat("_Clip2", 3.0f);


            }
        }
        // enemy[] enemies = Object.FindObjectsOfType<enemy>();
        /*if (enemies.waring>=enemy.midleWaring && enemy.waring<enemy.maxWaring)
        {
            material.SetFloat("_Clip", 3.0f);
        }
        else if(enemy.waring>= enemy.maxWaring)
        {
            material.SetFloat("_Clip2", 3.0f);
        }*/

        /*if (enemy.GetComponent<enemy>().waring>=enemy.GetComponent<enemy>().midleWaring && enemy.GetComponent<enemy>().waring <=enemy.GetComponent<enemy>().maxWaring)
        {
            enemy.GetComponent<enemy>().material.SetFloat("_Clip", 3.0f);
            secondMesh.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
        }
        else if(enemy.GetComponent<enemy>().waring >= enemy.GetComponent<enemy>().maxWaring)
        {
            enemy.GetComponent<enemy>().material.SetFloat("_Clip", 3.0f);
            secondMesh.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
            enemy.GetComponent<enemy>().material.SetFloat("_Clip2", 3.0f);
            secondMesh.GetComponent<secondMesh>().material.SetFloat("_Clip2", 3.0f);
        }

        if (enemy1.GetComponent<enemy>().waring >= enemy1.GetComponent<enemy>().midleWaring && enemy1.GetComponent<enemy>().waring <= enemy1.GetComponent<enemy>().maxWaring)
        {
            enemy1.GetComponent<enemy>().material.SetFloat("_Clip", 3.0f);
            secondMesh1.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
        }
        else if (enemy1.GetComponent<enemy>().waring >= enemy1.GetComponent<enemy>().maxWaring)
        {
            enemy1.GetComponent<enemy>().material.SetFloat("_Clip", 3.0f);
            secondMesh1.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
            enemy1.GetComponent<enemy>().material.SetFloat("_Clip2", 3.0f);
            secondMesh1.GetComponent<secondMesh>().material.SetFloat("_Clip2", 3.0f);
        }

        if (enemy2.GetComponent<enemy1>().waring >= enemy2.GetComponent<enemy1>().midleWaring && enemy2.GetComponent<enemy1>().waring <= enemy2.GetComponent<enemy1>().maxWaring)
        {
            enemy2.GetComponent<enemy1>().material.SetFloat("_Clip", 3.0f);
            secondMesh2.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
        }
        else if (enemy2.GetComponent<enemy1>().waring >= enemy2.GetComponent<enemy1>().maxWaring)
        {
            enemy2.GetComponent<enemy1>().material.SetFloat("_Clip", 3.0f);
            secondMesh2.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
            enemy2.GetComponent<enemy1>().material.SetFloat("_Clip2", 3.0f);
            secondMesh2.GetComponent<secondMesh>().material.SetFloat("_Clip2", 3.0f);
        }

        if (enemy3.GetComponent<enemy1>().waring >= enemy2.GetComponent<enemy1>().midleWaring && enemy3.GetComponent<enemy1>().waring <= enemy3.GetComponent<enemy1>().maxWaring)
        {
            enemy3.GetComponent<enemy1>().material.SetFloat("_Clip", 3.0f);
            secondMesh3.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
        }
        else if (enemy3.GetComponent<enemy1>().waring >= enemy3.GetComponent<enemy1>().maxWaring)
        {
            enemy3.GetComponent<enemy1>().material.SetFloat("_Clip", 3.0f);
            secondMesh3.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
            enemy3.GetComponent<enemy1>().material.SetFloat("_Clip2", 3.0f);
            secondMesh3.GetComponent<secondMesh>().material.SetFloat("_Clip2", 3.0f);
        }

        if (enemy4.GetComponent<enemy2>().waring >= enemy4.GetComponent<enemy2>().midleWaring && enemy4.GetComponent<enemy2>().waring <= enemy4.GetComponent<enemy2>().maxWaring)
        {
            enemy4.GetComponent<enemy2>().material.SetFloat("_Clip", 3.0f);
            secondMesh4.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
        }
        else if (enemy4.GetComponent<enemy2>().waring >= enemy4.GetComponent<enemy2>().maxWaring)
        {
            enemy4.GetComponent<enemy2>().material.SetFloat("_Clip", 3.0f);
            secondMesh4.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
            enemy4.GetComponent<enemy2>().material.SetFloat("_Clip2", 3.0f);
            secondMesh4.GetComponent<secondMesh>().material.SetFloat("_Clip2", 3.0f);
        }

        if (enemy5.GetComponent<enemy>().waring >= enemy5.GetComponent<enemy>().midleWaring && enemy5.GetComponent<enemy>().waring <= enemy5.GetComponent<enemy>().maxWaring)
        {
            enemy5.GetComponent<enemy>().material.SetFloat("_Clip", 3.0f);
            secondMesh5.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
        }
        else if (enemy5.GetComponent<enemy>().waring >= enemy5.GetComponent<enemy>().maxWaring)
        {
            enemy5.GetComponent<enemy>().material.SetFloat("_Clip", 3.0f);
            secondMesh5.GetComponent<secondMesh>().material.SetFloat("_Clip", 3.0f);
            enemy5.GetComponent<enemy>().material.SetFloat("_Clip2", 3.0f);
            secondMesh5.GetComponent<secondMesh>().material.SetFloat("_Clip2", 3.0f);
        }
        */
    }
}
