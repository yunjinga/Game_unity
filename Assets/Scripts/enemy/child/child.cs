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

    int now = 1;
    string[] s = new string[50];
    static string localPath = "Assets" + "\\" + "streamingAssetsPath" + "\\" + "child.xml";
    XmlDocument xml;
    XmlNodeList nodeList;
    int num = 1;
    public string level;//child几

    bool isAll = false;
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
        if (File.Exists(localPath))
        {
            xml = new XmlDocument();
            xml.Load(localPath);//加载xml文件
            nodeList = xml.SelectSingleNode("Data").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.Name == level)
                {
                    //Debug.Log(xe.GetAttribute("对话"));     
                    s[num++] = xe.GetAttribute("对话");
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            //Debug.Log(now+" "+num);   
            tishi.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)  && now < num)
            {
                duihua.SetActive(true);
                duihua_1.SetActive(true);
                biaoqing_1.SetActive(false);
                biaoqing_2.SetActive(true);
                cvam.Priority = 11;
                //gudin();
                text.text = s[now++];
                istalk = true;
                ator.SetBool("isTalk", true);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && istalk && now == num  && !isAll)
            {
                duihua_1.SetActive(false);
                cvam.Priority = 5;
                duihua.SetActive(false);
                istalk=false;
                biaoqing_2.SetActive(false);
                biaoqing_1.SetActive(true);
                ator.SetBool("isTalk", false);
                isAll = true;
            }
            //如果说完了则进行最后一句
            else if(Input.GetKeyDown(KeyCode.Space) && isAll &&!istalk)
            {
                duihua.SetActive(true);
                duihua_1.SetActive(true);
                biaoqing_1.SetActive(false);
                biaoqing_2.SetActive(true);
                cvam.Priority = 11;
                istalk = true;
                ator.SetBool("isTalk", true);
            }
            else if(Input.GetKeyDown(KeyCode.Space) && istalk && isAll)
            {
                duihua_1.SetActive(false);
                cvam.Priority = 5;
                duihua.SetActive(false);
                istalk = false;
                biaoqing_2.SetActive(false);
                biaoqing_1.SetActive(true);
                ator.SetBool("isTalk", false);
            }

        }
        else
        {
            tishi.SetActive(false);
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
