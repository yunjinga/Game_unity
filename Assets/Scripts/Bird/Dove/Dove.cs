/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System;


public class Dove : MonoBehaviour
{
    public string level;
    float xOffset = 0;
    float yOffset = 100;
    public GameObject player;
    public GameObject duihua;
    string[] s = new string[50];
    int now = 1;
    static string localPath = "Assets" + "\\" + "streamingAssetsPath" + "\\" + "Dove.xml";
    XmlDocument xml;
    XmlNodeList nodeList;
    public Text text;
    int num = 1;
    public RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {

        if (File.Exists(localPath))
        {
            xml = new XmlDocument();
            xml.Load(localPath);//加载xml文件
            nodeList = xml.SelectSingleNode("Data").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.Name == level)
                {
                    Debug.Log(xe.GetAttribute("对话"));
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
            if (Input.GetKeyDown(KeyCode.Space) && now < num)
            {
                duihua.SetActive(true);
                gudin();
                //text的值等于s[num++]
                text.text = s[num++];
            }
            else
            {
                duihua.SetActive(true);
            }
        }
        
    }
    void gudin()
    {
        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z));
        rectTransform.position = player2DPosition + new Vector2(xOffset, yOffset);
        if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
        {
            rectTransform.gameObject.SetActive(false);
        }
        else
        {
            if (rectTransform.gameObject.activeInHierarchy == false)
            {
                rectTransform.gameObject.SetActive(true);
            }
        }
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System;
using Cinemachine;

public class Dove : MonoBehaviour
{
     float xOffset =0;
     float yOffset = -40;
    public CinemachineVirtualCamera cvam;
    public string level;
    public GameObject player;
    public GameObject duihua;
    string[] s = new string[50];
    int now = 1;
    static string localPath;
    XmlDocument xml;
    XmlNodeList nodeList;
    public Text text;
    int num = 1;
    public GameObject texture_1_1;
    public GameObject texture_1_2;
    public RectTransform rectTransform;
    int num_texture = 0;
    bool isAll = false;

    public GameObject tishi;

    // Start is called before the first frame update
    private void Awake()
    {
        //texture_1_1=GameObject.Find("")
        transform.GetComponent<Fly>().enabled = false;
    }
    void Start()
    {
        cvam = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
        //texture_1_1 = GameObject.Find("texture1_1");
        //texture_1_2 = GameObject.Find("texture1_2");
        if (texture_1_1)
        {
            texture_1_1.SetActive(false);
        }
        if(texture_1_2)
        {
            texture_1_2.SetActive(false);
        }
        localPath = Application.dataPath + "/StreamingAssets/Dove.xml";
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
        duihua.SetActive(false);
        tishi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(now);
        
        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            //Debug.Log(now);
            if (now == 1)
            {
                tishi.SetActive(true);
                
            }
            else if(now!=1)
            {
                tishi.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Space) && now < num - 1 )
            {
                duihua.SetActive(true);
                gudin();
                
                //text的值等于s[num++]
                Debug.Log(s[now]);
                text.text = s[now++];
            }
            else if (Input.GetKeyDown(KeyCode.Space) && now == num - 1  && !isAll)
            {
                //Debug.Log(111111);
                num_texture++;

                if (level == "L1")
                {
                    if (num_texture == 1)
                    {
                        texture_1_1.SetActive(true);
                        duihua.SetActive(false);

                    }

                    if (num_texture == 2)
                    {
                        texture_1_1.SetActive(false);
                        texture_1_2.SetActive(true);
                        duihua.SetActive(false);
                    }
                    if (num_texture == 3)
                    {
                        texture_1_1.SetActive(false);
                        texture_1_2.SetActive(false);
                        duihua.SetActive(true);
                        text.text = s[now];
                    }
                    if (num_texture == 4)
                    {
                        duihua.SetActive(false);
                        isAll = true;
                    }
                }
                else if(level != "L1")
                {
                    if (level == "L5")
                    {
                        if (num_texture == 1)
                        {
                           
                                cvam.m_Lens.FieldOfView = 30;
                            texture_1_1.SetActive(true);
                            duihua.SetActive(false);
                        }
                        if (num_texture == 2)
                        {
                            texture_1_1.SetActive(false);
                            texture_1_2.SetActive(false);
                            duihua.SetActive(true);
                            text.text = s[now]; cvam.m_Lens.FieldOfView = 50;
                        }
                        if (num_texture == 3)
                        {
                            duihua.SetActive(false);
                            isAll = true;
                           
                        }
                    }
                    else
                    {
                        duihua.SetActive(true);
                        text.text = s[now];
                    }
                    
                }
         
                
            }
            if(isAll)
            {
                transform.GetComponent<Fly>().enabled = true;
            }
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
            duihua.SetActive(false);
            tishi.SetActive(false);
        }
    }
    void gudin()
    {
        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z));
        rectTransform.position = player2DPosition + new Vector2(xOffset, yOffset);
        if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
        {
            rectTransform.gameObject.SetActive(false);
        }
        else
        {
            if (rectTransform.gameObject.activeInHierarchy == false)
            {
                rectTransform.gameObject.SetActive(true);
            }
        }
    }
}

