using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera3 : MonoBehaviour
{
    //public GameObject canvas;
    public CinemachineVirtualCamera cvam;
    public CinemachineVirtualCamera cvam_1;
    int cishu = 0;//与触发器触发的次数
    float wait = 0;
    bool isplay1 = false;
    public GameObject duihua;
    public GameObject duihua_1;
    public GameObject tishi;
    public CinemachineVirtualCamera cvam_2;
    public CinemachineVirtualCamera cvam_3;
    public CinemachineVirtualCamera cvam_4;
    public CinemachineVirtualCamera cvam_5;
    public GameObject biaoqing_1;
    public GameObject biaoqing_2;

    int nums;//按J键的次数
             // public Text text;
    public GameObject player;
    float wait_down = 0;//按下j记录时间
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //canvas = GameObject.Find("UI").gameObject;
        cvam = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_2 = GameObject.Find("CM vcam3").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_3 = GameObject.Find("CM vcam5").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_1 = GameObject.Find("CM vcam4").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_4 = GameObject.Find("CM vcam11").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_5 = GameObject.Find("CM vcam12").gameObject.GetComponent<CinemachineVirtualCamera>();
        //tishi = GameObject.Find("tishi3").gameObject;
        tishi.SetActive(false);
        ////camera.enabled = true;
        //camera2.enabled = false;
        duihua.SetActive(false);
        duihua_1.SetActive(false);
        biaoqing_1.SetActive(false);
        biaoqing_2.SetActive(true);
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
                    biaoqing_1.SetActive(true);
                    biaoqing_2.SetActive(false);
                    duihua.SetActive(true);
                    duihua_1.SetActive(true);
                    wait_down = 3;
                }
                cvam_1.Priority = 11;
                cvam_2.Priority = 5;
                cvam_3.Priority = 5;
                cvam_4.Priority = 5;
                cvam_5.Priority = 5;
               // canvas.SetActive(false);

                //text.text = "金豚房产：目前有多地正在施工，期待我们的美好家园！";

            }
            if (nums == 2)
            {
                cvam_1.Priority = 5;
                //canvas.SetActive(true);
                duihua.SetActive(false);
                duihua_1.SetActive(false);
                tishi.SetActive(false);
                biaoqing_2.SetActive(true);
                biaoqing_1.SetActive(false);

                //text.text = "金豚村烟熏五花肉，全世界人民的猪肉好选择！";
            }
            //if (nums == 3)
            //{
            //    text.text = "金豚村土猪肉最近销量颇高，很多卖猪肉的餐饮行业营收状况都非常得好。";
            //}
            //if (nums == 4)
            //{
            //    text.text = "金豚政府：近日有些地区的共享单车已经开始投入使用，在享受便利的同时也不要忘了我们的猪猪，有时间带着你家的小猪一起骑车兜风吧！";
            //}
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu == false)
        {
            nums = 0;
            wait = 0;
            wait_down = 0;
            tishi.SetActive(true);
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

