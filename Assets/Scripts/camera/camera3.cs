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
    int cishu = 0;//�봥���������Ĵ���
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

    int nums;//��J���Ĵ���
             // public Text text;
    public GameObject player;
    float wait_down = 0;//����j��¼ʱ��
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

                //text.text = "���෿����Ŀǰ�ж������ʩ�����ڴ����ǵ����ü�԰��";

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

                //text.text = "�������Ѭ�廨�⣬ȫ��������������ѡ��";
            }
            //if (nums == 3)
            //{
            //    text.text = "�������������������ĸߣ��ܶ�������Ĳ�����ҵӪ��״�����ǳ��úá�";
            //}
            //if (nums == 4)
            //{
            //    text.text = "����������������Щ�����Ĺ������Ѿ���ʼͶ��ʹ�ã������ܱ�����ͬʱҲ��Ҫ�������ǵ�������ʱ�������ҵ�С��һ���ﳵ����ɣ�";
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

