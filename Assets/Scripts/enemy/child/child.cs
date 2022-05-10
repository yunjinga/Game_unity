using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System;
using Cinemachine;

public class child : MonoBehaviour
{
    Animator ator;
    public float xOffset = 0;
    public float yOffset = 0;
    public GameObject player;
    public GameObject duihua;
    public GameObject duihua_1;
    public Text text;
    //public RectTransform rectTransform;
    public bool istalk = false;
    public GameObject tishi;
    public CinemachineVirtualCamera cvam;
    public GameObject biaoqing_1;
    public GameObject biaoqing_2;
    float wait = 0;
    //bool istalk = false;
    // Start is called before the first frame update
    void Start()
    {
        tishi.SetActive(false);
        duihua.SetActive(false);
        duihua_1.SetActive(false);
        cvam = GameObject.Find("CM vcam15").gameObject.GetComponent<CinemachineVirtualCamera>();
        biaoqing_2.SetActive(false);
        biaoqing_1.SetActive(true);
        ator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
           
            
            tishi.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) && !istalk)
            {
                Debug.Log(wait);
               
                    duihua.SetActive(true);
                    wait = 2.1f;
               
               
                duihua_1.SetActive(true);
                biaoqing_1.SetActive(false);
                biaoqing_2.SetActive(true);
                cvam.Priority = 11;
                //gudin();
                text.text = "妈妈说你现在虽然是小猪，但有一天会变成大肥猪";
                istalk = true;
                ator.SetBool("isTalk", true);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && istalk)
            {
                duihua_1.SetActive(false);
                wait = 0;
                cvam.Priority = 5;
                duihua.SetActive(false);
                istalk=false;
                biaoqing_2.SetActive(false);
                biaoqing_1.SetActive(true);
                ator.SetBool("isTalk", false);
                
            }

        }
        else
        {tishi.SetActive(false);

        }
    }
    //void gudin()
    //{
    //    Vector2 player2DPosition = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z));
    //    rectTransform.position = player2DPosition + new Vector2(xOffset, yOffset);
    //    if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
    //    {
    //        rectTransform.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        if (rectTransform.gameObject.activeInHierarchy == false)
    //        {
    //            rectTransform.gameObject.SetActive(true);
    //        }
    //    }
    //}
}
