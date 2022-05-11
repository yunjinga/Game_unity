using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera6_1 : MonoBehaviour
{
   // public GameObject canvas;
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
    public CinemachineVirtualCamera cvam_6;
    public CinemachineVirtualCamera cvam_7;
    public CinemachineVirtualCamera cvam_8;//level2红色告示
    public int level;
    public List<CinemachineVirtualCamera> cvam1;
    public GameObject player;
    float wait_down;
    public GameObject biaoqing_1;
    public GameObject biaoqing_2;
    public bool istalk2_1=false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //canvas = GameObject.Find("UI").gameObject;
       // cvam = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_1 = GameObject.Find("CM vcam3").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_2 = GameObject.Find("CM vcam4").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_3 = GameObject.Find("CM vcam5").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_4 = GameObject.Find("CM vcam11").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_5 = GameObject.Find("CM vcam12").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_6 = GameObject.Find("CM vcam13").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_7 = GameObject.Find("CM vcam14").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_8 = GameObject.Find("CM vcam17").gameObject.GetComponent<CinemachineVirtualCamera>();
        //cvam1.Add(cvam);
        cvam1.Add(cvam_1);
        cvam1.Add(cvam_2);
        cvam1.Add(cvam_3);
        cvam1.Add(cvam_4);
        cvam1.Add(cvam_5);
        cvam1.Add(cvam_6);
        cvam1.Add(cvam_7);
        cvam1.Add(cvam_8);
        //tishi = GameObject.Find("tishi2").gameObject;
        tishi.SetActive(false);
        ////camera.enabled = true;
        //camera2.enabled = false;
        duihua.SetActive(false);
        duihua_1.SetActive(false);
        biaoqing_2.SetActive(false);
        biaoqing_1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isplay1 == true)
        {
            wait += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
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
                    if (level == 2)
                    {
                        biaoqing_1.SetActive(false);
                        biaoqing_2.SetActive(true);
                    }
                    if (level == 1)
                    {
                        istalk2_1 = true;
                    }
                    if (level == 8)
                    {
                        duihua.SetActive(true);
                        duihua_1.SetActive(false);
                    }
                    else
                    {duihua.SetActive(true);
                    //text.text = "金豚房产：目前有多地正在施工，期待我们的美好家园！";
                    duihua_1.SetActive(true);

                    }
                    
                    wait_down = 3;
                }
                for(int i = 0; i < cvam1.Count; i++)
                {
                    
                    if (i != level - 1)
                    {
                        cvam1[i].Priority = 5;
                    }
                    if (i == level - 1)
                    {
                        cvam1[i].Priority = 11;
                    }
                }
                //cvam_1.Priority = 11;
                //cvam_2.Priority = 5;
                //cvam_4.Priority = 5;
                //cvam_3.Priority = 5;
                //cvam_5.Priority = 5;
                //canvas.SetActive(false);

                //text.text = "金豚房产：目前有多地正在施工，期待我们的美好家园！";

            }
            if (nums == 2)
            {
                if (level == 2)
                    {
                        biaoqing_2.SetActive(false);
                        biaoqing_1.SetActive(true);
                    }
                wait_down = 0;
                cvam1[level-1].Priority = 5;
                //canvas.SetActive(true);
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
        nums = 0;
    }
}
