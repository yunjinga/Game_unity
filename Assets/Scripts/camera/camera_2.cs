using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera_2 : MonoBehaviour
{
    //level2告示栏
    public GameObject canvas;
    public CinemachineVirtualCamera cvam;
    public CinemachineVirtualCamera cvam_1;
    int cishu = 0;//与触发器触发的次数
    float wait = 0;
    bool isplay1 = false;
    public GameObject duihua;
    public GameObject duihua_1;
    public GameObject tishi;
    int nums;//按J键的次数
             // public Text text;
    public CinemachineVirtualCamera cvam_2;
    public CinemachineVirtualCamera cvam_3;
    public CinemachineVirtualCamera cvam_4;
    public CinemachineVirtualCamera cvam_5;
    public GameObject player;
    float wait_down;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        canvas = GameObject.Find("UI").gameObject;
        cvam = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_1 = GameObject.Find("CM vcam3").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_2 = GameObject.Find("CM vcam4").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_3 = GameObject.Find("CM vcam5").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_4 = GameObject.Find("CM vcam11").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_5 = GameObject.Find("CM vcam12").gameObject.GetComponent<CinemachineVirtualCamera>();
        tishi = GameObject.Find("tishi2").gameObject;
        tishi.SetActive(false);
        ////camera.enabled = true;
        //camera2.enabled = false;
        duihua.SetActive(false);
        duihua_1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isplay1 == true)
        {
            wait += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.J))
            {
                nums++;
            }
            if (nums == 0)
            {
                if (wait >= 5)
                {
                    tishi.SetActive(false);
                }
            }
            if (nums == 1)
            {
                wait_down += Time.deltaTime;
                if (wait_down > 2)
                {
                    duihua.SetActive(true);
                    //text.text = "金豚房产：目前有多地正在施工，期待我们的美好家园！";
                    duihua_1.SetActive(true);
                    wait_down = 3;
                }
                cvam_1.Priority = 11;
                cvam_2.Priority = 5;
                cvam_3.Priority = 5;
                cvam_4.Priority = 5;
                cvam_5.Priority = 5;
                canvas.SetActive(false);

                //text.text = "金豚房产：目前有多地正在施工，期待我们的美好家园！";

            }
            if (nums == 2)
            {
                wait_down = 0;
                cvam_1.Priority = 5;
                canvas.SetActive(true);
                duihua.SetActive(false);
                duihua_1.SetActive(false);
                tishi.SetActive(false);
            }
            //if (nums == 3)
            //{
            //    text.text = "金豚工作室：为了纪念每一只带来财富的，我们将会为整个村子建造更多的猪猪雕像。";
            //}

            //wait += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        nums = 0;
        wait = 0;
        tishi.SetActive(true);
        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu == false)
        {
            //camera2.enabled = true;
            //camera.enabled = false;
            isplay1 = true;
        }
        else
        {
            isplay1 = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        isplay1 = false;
        tishi.SetActive(false);
    }
}

