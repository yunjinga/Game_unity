using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera_3 : MonoBehaviour
{
    public GameObject canvas;
    public CinemachineVirtualCamera cvam;
    public CinemachineVirtualCamera cvam_1;
    public CinemachineVirtualCamera cvam_2;
    public CinemachineVirtualCamera cvam_3;
    public CinemachineVirtualCamera cvam_4;
    public CinemachineVirtualCamera cvam_5;
    float wait_down = 0;
    float wait = 0;
    bool isplay1 = false;
    public GameObject duihua;
    public GameObject tishi;
    int nums;//按J键的次数
    //public Text text;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("UI").gameObject;
        cvam = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_2 = GameObject.Find("CM vcam3").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_3 = GameObject.Find("CM vcam4").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_1 = GameObject.Find("CM vcam5").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_4 = GameObject.Find("CM vcam11").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam_5 = GameObject.Find("CM vcam12").gameObject.GetComponent<CinemachineVirtualCamera>();
        tishi = GameObject.Find("tishi3_1").gameObject;
        tishi.SetActive(false);
        ////camera.enabled = true;
        //camera2.enabled = false;
        duihua.SetActive(false);

        player = GameObject.Find("Player");
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

                cvam_1.Priority = 5;
                canvas.SetActive(true);
                duihua.SetActive(false);
                wait_down = 0;
                tishi.SetActive(false);


            }


            //wait += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        nums = 0;
        wait = 0;
        
        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu == false)
        {
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
