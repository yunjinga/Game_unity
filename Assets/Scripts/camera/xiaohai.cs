using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xiaohai : MonoBehaviour
{
    public CinemachineVirtualCamera cineVirtual;
    bool isTrigger = false;
    bool isfirst = false;
    public float speed = 2.0f;
    public GameObject NPC;
    int cishu = 0;
    bool isstart = false;
    public GameObject duihua_one;
    public GameObject biaoqing_1;
    public GameObject biaoqing_2;
    float wait = 0;
    bool next = false;
    public GameObject player;
    public GameObject tishi;
    int num = 0;
    private void Start()
    {
        tishi.SetActive(false);
        player = GameObject.Find("Player");
        duihua_one.SetActive(false);
        //duihua_two.SetActive(false);
        cineVirtual = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
        biaoqing_2.SetActive(false);
        biaoqing_1.SetActive(true);

    }
    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(NPC.transform.position);

        if (Input.GetKeyDown(KeyCode.Space) && isTrigger == true)
        {
            num++;
        }
        switch (num)
        {
            case 1:
                {
                    duihua_one.SetActive(true);
                    duihua_one.transform.position = screenPos + new Vector3(40, 400, 0);
                    biaoqing_1.SetActive(false);
                    biaoqing_2.SetActive(true);
                    break;
                }
            
            case 2:
                {
                    duihua_one.SetActive(false);
                    biaoqing_2.SetActive(false);
                    biaoqing_1.SetActive(true);
                    isTrigger = false;
                    break;
                }

        }
        var cineBody = (CinemachineFramingTransposer)cineVirtual.GetCinemachineComponent(CinemachineCore.Stage.Body);
        //next_duihua();
        if (isTrigger == true && num != 0)
        {
            tishi.SetActive(false);
            wait += Time.deltaTime;
            if (cineBody.m_CameraDistance >= 2.5)
            {
                Debug.Log("no");
                cineBody.m_CameraDistance = cineBody.m_CameraDistance - speed * Time.deltaTime;
            }


        }
        if (isTrigger == false && isstart == true)
        {
            if (cineBody.m_CameraDistance < 4f)
            {
                cineBody.m_CameraDistance = cineBody.m_CameraDistance + speed * Time.deltaTime;
            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu == false)
        {
            isstart = true;
            cishu++;
            //Debug.Log(cishu);
            tishi.SetActive(true);
            isTrigger = true;


            //player.gameObject.GetComponent<player>().enabled = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu == false)
        {
            num = 0;
            isTrigger = false;
            tishi.SetActive(false);
            if (num != 0)
            {
                isstart = true;
            }
            else
            {
                isstart = false;
            }
        }
    }

}
