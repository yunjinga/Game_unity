using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class daye6_2 : MonoBehaviour
{
    public GameObject player;
    public GameObject duihua;

    public Text text;


    //public RectTransform rectTransform;
    public bool istalk = false;
    public GameObject tishi;
    public CinemachineVirtualCamera cvam;
    int num = 0;
    float wait = 0;
    //bool istalk = false;
    // Start is called before the first frame update
    void Start()
    {
        tishi.SetActive(false);
        duihua.SetActive(false);
        //baige = GameObject.Find("baige");
        cvam = GameObject.Find("CM vcam19").gameObject.GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        //ator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Vector3.Distance(transform.position, player.transform.position) < 10f)
        //{
            //
            if(istalk)tishi.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) && istalk)
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
                        text.text = "游戏到这里就接近结束了，感谢你玩到这里";
                        //istalk = true;
                        //ator.SetBool("isTalk", true);
                        break;
                    }
                case 2:
                    {
                        text.text = "制作人说这个项目还有很多的问题，但他真的做不动了";
                        //istalk = true;
                        //ator.SetBool("isTalk", true);
                        break;
                    }
                case 3:
                    {

                    text.text = "可能没法很好回应你的期待，所以代他向你说声抱歉";
                        break;
                    }
                case 4:
                    {
                        duihua.SetActive(false);
                        cvam.Priority = 5;
                        istalk = false;
                        tishi.SetActive(false);
                        //ator.SetBool("isTalk", false);
                        num++;
                        break;
                    }
                default:
                    {
                        if (wait % 2 == 1)
                        {duihua.SetActive(true);
                            text.text = "我刚刚对着一只猪在自言自语些什么";

                        }
                        if (wait % 2 == 0)
                        {
                            duihua.SetActive(false);
                            tishi.SetActive(false);
                        }
                        break;
                    }
            }
        }
    //    else
    //    {
    //        tishi.SetActive(false);
    //    }

    //}
    private void OnTriggerEnter(Collider other)
    {
        istalk = true;
        tishi.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        istalk = false;
    }
}
