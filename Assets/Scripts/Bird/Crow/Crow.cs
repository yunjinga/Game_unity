using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System;

public class Crow : MonoBehaviour
{
    public RectTransform recTransform;
    public float x;
    public float y;
    public int now = 0;
    static string localPath ;
    XmlDocument xml;
    XmlNodeList nodeList;
    public GameObject player;
    public GameObject speak;
    bool isInspeak = false;
    float TimeFly = 1.5f;
    public bool isComplete=false;
    ArrayList arraylist = new ArrayList();
    int n = 0;
    int max = 0;
    // Start is called before the first frame update
    void Start() //读取存储对话信息的表单
    {


        localPath = Application.dataPath + "/StreamingAssets/1234.xml";
        if (File.Exists(localPath))
        {
            xml = new XmlDocument();
            xml.Load(localPath);//加载xml文件
            nodeList = xml.SelectSingleNode("Data").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if(xe.GetAttribute("Bool")=="1")
                {
                    arraylist.Add(Convert.ToInt32(xe.GetAttribute("Id")));
                    max++;
                }
            }
        }
        speak.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(transform.position);

        recTransform.position = player2DPosition + new Vector2(x, y);
        if(Input.GetKeyDown(KeyCode.E) && isInspeak)
        {
            if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
            {
                recTransform.gameObject.SetActive(false);
            }
            else
            {
                recTransform.gameObject.SetActive(true);
            }
            speak.SetActive(true);
            Debug.Log(now);
            if(now < Convert.ToInt32(arraylist[n]))
            {
                now++;
            }
            else if(now == Convert.ToInt32(arraylist[n]))
            {
                if(isComplete)
                {
                    Debug.Log("任务完成");
                    now++;
                }
            }
            Text text = speak.transform.Find("Text").GetComponent<Text>();
            foreach (XmlElement xe in nodeList)
            {
                if(xe.GetAttribute("Id") == now.ToString())
                {
                    text.text = xe.GetAttribute("任务");
                    speak.SetActive(true);
                }
            }
        }
        if(now== Convert.ToInt32(arraylist[n]) + 1 && TimeFly>0)
        {
            TimeFly = TimeFly - Time.deltaTime;
        }
        else if(TimeFly<=0 && now== Convert.ToInt32(arraylist[n]) + 1)
        {
            player.gameObject.GetComponent<player_blood>().blood = 2;
            isComplete = false;
            speak.SetActive(false);
            transform.GetComponent<Fly>().enabled = true;
            if(n < max - 1)
            {
                n++;
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            speak.SetActive(false);
        }
        

    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            isInspeak = true;
        }
        else
        {
            isInspeak = false;
        }

    }
}
