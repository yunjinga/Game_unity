using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jvqing3_2 : MonoBehaviour
{
    
    bool isTrigger = false;
    bool isfirst = false;
    public float speed = 2.0f;
    public GameObject NPC1;
    public GameObject NPC2;
    int cishu = 0;
    bool isstart = false;
    // public static bool  isMove = true;
    int ancishu = 0;
    public GameObject duihua_one;
    public GameObject duihua_two;
    float wait = 0;
    bool next = false;
    public GameObject player;
    public GameObject tishi;
    int num = 0;

    private void Start()
    {

        tishi.SetActive(false);
        player = GameObject.Find("Player");
        duihua_one.SetActive(false);
        duihua_two.SetActive(false);
     

    }
    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(NPC1.transform.position);
        Vector3 screenPos1 = Camera.main.WorldToScreenPoint(NPC2.transform.position);
        if (Input.GetKeyDown(KeyCode.Space) && isTrigger == true)
        {
            num++;
        }
        switch (num)
        {
            case 1:
                {
                    duihua_one.SetActive(true);
                    duihua_two.SetActive(true);
                    duihua_one.transform.position = screenPos + new Vector3(40, 400, 0);
                    duihua_two.transform.position = screenPos1 + new Vector3(40, 400, 0);
                    break;
                }
            case 2:
                {
                    duihua_one.SetActive(false);
                    duihua_two.SetActive(false);
                    break;
                }

        }
        
        //next_duihua();
        if (isTrigger == true && num != 0)
        {
            tishi.SetActive(false);
        }
       

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu == false)
        {
            isstart = true;
            cishu++;
            Debug.Log(cishu);
            tishi.SetActive(true);
            isTrigger = true;
            num = 0;


            //player.gameObject.GetComponent<player>().enabled = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu == false)
        {
            
            isTrigger = false;
            tishi.SetActive(false);
            //if (num != 0)
            //{
            //    isstart = true;
            //}
            //else
            //{
            //    isstart = false;
            //}
        }
    }
}
