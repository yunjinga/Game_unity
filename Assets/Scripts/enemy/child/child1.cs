using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class child1 : MonoBehaviour
{
    Animator ator;
   
    public GameObject player;
    public GameObject duihua;
    
    public Text text;
    public Text text1;//�׸�ĶԻ�
    public GameObject baige;
    public GameObject wuya;
    public GameObject zhu;
    //public RectTransform rectTransform;
    public bool istalk = false;
    public GameObject tishi;
    public CinemachineVirtualCamera cvam;
    
    float wait = 0;
    //bool istalk = false;
    // Start is called before the first frame update
    public AudioSource source;
    //bool istalk = false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.Stop();
        tishi.SetActive(false);
        duihua.SetActive(false);
        //baige = GameObject.Find("baige");
        cvam = GameObject.Find("CM vcam16").gameObject.GetComponent<CinemachineVirtualCamera>();
        zhu.SetActive(false);
        wuya.SetActive(false);
        baige.SetActive(false);
        ator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {


            tishi.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) && !istalk)
            {
                wait++;
            }
            switch (wait)
            {
                case 1:
                    {
                        duihua.SetActive(true);
                        cvam.Priority = 11;
                        //gudin();
                        text.text = "����������";
                        //istalk = true;
                        ator.SetBool("isTalk", true);
                        break;
                    }
                case 2:
                    {
                        text.text = "�ҵ�������Ҫ��Ϊ��С�����һ����������Ϸ�����ˣ����¼����˲����Ҵ���Ϸ��";
                        //istalk = true;
                        ator.SetBool("isTalk", true);
                        source.Play();
                        break;
                    }
                case 3:
                    {

                        text1.text = "��Ҫ���⡰��ѻ�족�Ļ���";
                        //
                        duihua.SetActive(false);
                        wuya.SetActive(true);

                        zhu.SetActive(true);

                        break;
                    }
                case 4:
                    {
                        zhu.SetActive(false);
                        wuya.SetActive(false);
                        baige.SetActive(true);
                        break;
                    }
                case 5:
                    {
                        baige.SetActive(false);
                        cvam.Priority = 5;
                        istalk = false;
                        ator.SetBool("isTalk", false);
                        break;
                    }

            }
            //else if (Input.GetKeyDown(KeyCode.Space) && istalk)
            //{
            //    duihua_1.SetActive(false);
            //    wait = 0;
            //    cvam.Priority = 5;
            //    duihua.SetActive(false);
            //    istalk = false;

            //    ator.SetBool("isTalk", false);

            //}

        }
        else
        {
            tishi.SetActive(false);

        }
    }
}

