using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level6_4 : MonoBehaviour
{
    Animator ator;

    public GameObject player;
    public GameObject duihua;
    public GameObject duihua_1;
    public GameObject daye2;
    public Text text;
    public Text text1;
    //public Text text2;
    public GameObject daye;
    //public RectTransform rectTransform;
    public bool istalk = false;
    public GameObject tishi;
    public CinemachineVirtualCamera cvam;
    public GameObject hand;
    float wait = 0;
    public int level;
    public GameObject duihua_3;
    //public GameObject duihua_2;
    public GameObject tr;
    float wait1 = 0;
    //bool istalk = false;
    // Start is called before the first frame update
    void Start()
    {
        //duihua_2.SetActive(false);
        duihua_3.SetActive(false);
        duihua_1.SetActive(false);
        tishi.SetActive(false);
        duihua.SetActive(false);
        //baige = GameObject.Find("baige");
       
        cvam = GameObject.Find("CM vcam22").gameObject.GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        ator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            tishi.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wait++;

            }
            switch (wait)
            {
                case 1:
                    {
                        wait1 +=Time.deltaTime;
                        if(wait1>1.5)duihua.SetActive(true);
                        



                        cvam.Priority = 11;
                        
                        //gudin();
                       text.text = "�������ʱ�䣬�Һ�ϣ���������ڵĴ��ӽ���ø�����һЩ��";
                        //istalk = true;
                        ator.SetBool("isTalk", true);
                        break;
                    }
                case 2:
                    {
                        duihua_1.SetActive(true);

                       
                        //gudin();
                       text1.text = "���������˶��ܹ�������µģ����Զ���һЩ���鲻Ҫ����ִ����";
                        //istalk = true;
                        ator.SetBool("isTalk", true);
                        break;
                    }
                case 3:
                    {
                        //duihua_1.SetActive(false);
                        //text.text = "����������µ�����";
                        
                        //gudin();
                        text.text = "Ҫ������������ʱ����Ը���һЩ�ͺ���";



                        break;
                    }
                case 4:
                    {
                        text1.text = "������ǻ���һЩ�ź���";
                        break;
                    }
                case 5:
                    {
                        text.text = "���У��㲻Ҫ�ܴ����ҵĹ��������Ժðɣ�";
                        break;
                    }
                case 6:
                    {
                        text1.text = "�����Ѱ����ո��գ����仨���긴�ꡱ�����������֮���ң�ʧ֮������һ����Ե�ͺ�";
                        break;
                    }
                case 7:
                    {
                        duihua.SetActive(false);
                        duihua_1.SetActive(false);
                        cvam.Priority = 5;
                        istalk = false;
                        //tishi.SetActive(false);
                        ator.SetBool("isTalk", false);
                        break;
                    }
                default:
                    {
                        Vector3 screenPos = Camera.main.WorldToScreenPoint(hand.transform.position);
                        Vector3 screenPos1 = Camera.main.WorldToScreenPoint(daye.transform.position);
                        Vector3 screenPos2 = Camera.main.WorldToScreenPoint(daye2.transform.position);
                        screenPos1.x += 180;
                        screenPos.x -= 170;
                        screenPos.y += 60;
                        //screenPos2.y += 100;
                        screenPos2.x -= 100;

                        if (wait > 4)
                        {
                            duihua.transform.position = screenPos;
                            duihua_1.transform.position = screenPos1;

                        }
                        if (wait % 2 == 1 && wait > 4 && level == 2)
                        {
                            duihua_3.transform.position = screenPos2;
                            duihua_3.SetActive(true);


                        }
                        if (wait % 2 == 0 && wait > 4 && level == 2)// && tr.GetComponent<level6_2>().level6 == true)
                        {
                            duihua_3.SetActive(false);
                            //tishi.SetActive(false);
                            istalk = false;
                        }
                        break;
                    }
            }
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            tishi.SetActive(false);
            duihua.SetActive(false);
            duihua_1.SetActive(false);
            duihua_3.SetActive(false);
        }
    }
}
