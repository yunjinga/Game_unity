using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class child6_1 : MonoBehaviour
{
    Animator ator;

    public GameObject player;
    public GameObject duihua;

    public Text text;


    //public RectTransform rectTransform;
    public bool istalk = false;
    public GameObject tishi;
    public CinemachineVirtualCamera cvam;

    float wait = 0;
    float wait1 = 0;
    //bool istalk = false;
    // Start is called before the first frame update
    void Start()
    {
        tishi.SetActive(false);
        duihua.SetActive(false);
        //baige = GameObject.Find("baige");
        cvam = GameObject.Find("CM vcam18").gameObject.GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        ator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            tishi.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) && !istalk)
            {
                wait++;

            }
            switch (wait%4)
            {
                case 1:
                    {
                        wait1 += Time.deltaTime;
                        if(wait1>1.5)  duihua.SetActive(true);
                      



                        cvam.Priority = 11;
                        //gudin();
                        text.text = "��Ȼ�����ڻ�С�����ҳ�����һ�����῿�����¸�";
                        //istalk = true;
                        ator.SetBool("isTalk", true);
                        break;
                    }
                case 2:
                    {
                        text.text = "��������Щ��Ϊ��С���ӵ����Ӷ��������ҵĳ�����";
                        //istalk = true;
                        ator.SetBool("isTalk", true);
                        break;
                    }
                case 3:
                    {

                        text.text = "�Ǻǣ�û������ĳ�����";

                        


                        break;
                    }
                case 0:
                    {duihua.SetActive(false);
                        cvam.Priority = 5;
                        istalk = false;
                        ator.SetBool("isTalk", false);
                        break;
                    }
            }
        }
        else {
            tishi.SetActive(false);
            duihua.SetActive(false);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    istalk = true;
    //    tishi.SetActive(true);
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    istalk = false;
    //}
}
