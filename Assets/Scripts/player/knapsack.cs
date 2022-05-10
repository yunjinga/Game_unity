using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System;
using Cinemachine;

public class knapsack : MonoBehaviour
{
    public CinemachineVirtualCamera cvam;
    public CinemachineVirtualCamera cvam1;//第三关
    public CinemachineVirtualCamera cvam2;//第四关
    public CinemachineVirtualCamera cvam3;//第五关
    public CinemachineVirtualCamera cvam4;//第六关
    public Slider slider1;
    public Slider slider2;
    public GameObject canvas;
    public bool isFull = false;
    public string[] game = new string[2];
    public bool[] isUse = new bool[2];
    public float[] timeUse = new float[2];
    public GameObject hammer1;
    public GameObject hammer2;
    public GameObject apple1;
    public GameObject apple2;
    public GameObject koushao1;
    public GameObject koushao2;
    public GameObject zhuangtai_1;
    bool isget = false;
    float wait;
    public GameObject zhuangtai;
    Vector3 position;
    public bool isUse_Whistle=false;
    public List<CinemachineVirtualCamera> camera;
   
    
    private void Awake()
    {
        //Debug.Log(GameObject.FindWithTag("hammer1"));
        hammer1 = GameObject.FindWithTag("hammer1").gameObject;
        hammer2 = GameObject.FindWithTag("hammer2").gameObject;
        apple1 = GameObject.FindWithTag("apple1").gameObject;
        apple2 = GameObject.FindWithTag("apple2").gameObject;
        koushao1 = GameObject.FindWithTag("koushao1").gameObject;
        koushao2 = GameObject.FindWithTag("koushao2").gameObject;
        //zhuangtai_1 = GameObject.Find("xihuan").gameObject;
        //zhuangtai = GameObject.Find("zhuangtai").gameObject;
        hammer1.SetActive(false);
        hammer2.SetActive(false);
        apple1.SetActive(false);
        apple2.SetActive(false);
        koushao1.SetActive(false);
        koushao2.SetActive(false);
        zhuangtai.SetActive(false);
        cvam = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam1 = GameObject.Find("CM vcam6").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam2 = GameObject.Find("CM vcam8").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam3 = GameObject.Find("CM vcam9").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam4 = GameObject.Find("CM vcam10").gameObject.GetComponent<CinemachineVirtualCamera>();
        canvas = GameObject.Find("UI").gameObject;
        camera.Add(cvam1);
        camera.Add(cvam2);
        camera.Add(cvam3);
        camera.Add(cvam4);
    }
    // Start is called before the first frame update
    void Start()
    {
        game[0] = string.Empty;
        game[1] = string.Empty;
        isUse[0] = false;
        isUse[1] = false;
        timeUse[0] = 0f;
        timeUse[1] = 0f;
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (game[0] != string.Empty && game[1] != string.Empty)
        {
            isFull = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {  int i = PlayerPrefs.GetInt("level");
            PlayerPrefs.SetInt("isMoveCamera", 0);
            transform.GetComponent<player>().enabled = true;
                    camera[i-2].Priority = 5;
                    canvas.SetActive(true);
            timeUse[0] = 10f;

        }
        if (Input.GetKeyDown(KeyCode.U) && game[0] != string.Empty )
        {
            int i = PlayerPrefs.GetInt("level");
            
            if (game[0] == "apple")
            {
                game[0] = string.Empty;
            }
            else if (game[0] == "whistle" &&slider1.value==0) 
            {
                //isUse[0] = true;
                
                if (transform.GetComponent<beizhui>().iszhuizhu == false)
                {
                    
                    isUse_Whistle = true;
                    transform.GetComponent<player>().enabled = false;
                    camera[i-2].Priority = 11;
                    canvas.SetActive(false);
                    PlayerPrefs.SetInt("isMoveCamera", 1);
                    
                }
            }
            else if (game[0] == "hammer")
            {
                //Debug.Log(1111);
                
                position = transform.forward * 2 + transform.position;
                GameObject.Instantiate(Resources.Load("polySurface186", typeof(GameObject)), position, new Quaternion(0, 0, 0, 0));
                game[0] = string.Empty;
            }

        }
        else if (Input.GetKeyDown(KeyCode.I) && game[1] != string.Empty && !isUse[1])
        {
            if (game[1] == "apple")
            {
                game[1] = string.Empty;
            }
            else if (game[1] == "whistle")
            {
                isUse[1] = true;
                timeUse[1] = 10f;
            }
            else if (game[1] == "hammer")
            {
                GameObject h_1 = GameObject.Instantiate(Resources.Load("polySurface186", typeof(GameObject)), position, new Quaternion(0, 0, 0, 0)) as GameObject;
                game[1] = string.Empty;
            }

        }
        //if (isUse[0] && timeUse[0] > 0)
        //{
        //    timeUse[0] -= Time.deltaTime;
        //}
        //if (isUse[1] && timeUse[1] > 0)
        //{
        //    timeUse[1] -= Time.deltaTime;
        //}
        //if (timeUse[0] <= 0)
        //{
        //    isUse[0] = false;
        //    timeUse[0] = 10f;
        //}
        //if (timeUse[1] <= 0)
        //{
        //    isUse[1] = false;
        //    timeUse[1] = 10f;
        //}
        if (game[0] != string.Empty)
        {
            if(game[0]=="hammer")
            {
                hammer1.SetActive(true);
            }
            else if(game[0]=="apple")
            {
                apple1.SetActive(true);
            }
            else if(game[0]== "whistle")
            {
                koushao1.SetActive(true);
            }
            //Debug.Log(game[0]);
            
            isget = true;
            zhuangtai.SetActive(true);
            zhuangtai_1.SetActive(true);
        }
        else if(game[0]==string.Empty)
        {
            hammer1.SetActive(false);
            apple1.SetActive(false);
            koushao1.SetActive(false);
        }
        if (game[1] != string.Empty)
        {
            if (game[1] == "hammer")
            {
                hammer2.SetActive(true);
            }
            else if (game[1] == "apple")
            {
                apple2.SetActive(true);
            }
            else if (game[1] == "whistle")
            {
                koushao2.SetActive(true);
            }
            Debug.Log(game[1]);
            hammer2.SetActive(true);
            isget = true;
            zhuangtai.SetActive(true);
            zhuangtai_1.SetActive(true);
        }
        else if (game[1] == string.Empty)
        {
            hammer2.SetActive(false);
            apple2.SetActive(false);
            koushao2.SetActive(false);
        }
    }
}
