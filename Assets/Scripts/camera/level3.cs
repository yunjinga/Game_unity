using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3 : MonoBehaviour
{
    public CinemachineVirtualCamera cvam;
    public CinemachineVirtualCamera cvam1;
    public GameObject canvas;
    float wait = 0;
    bool isdown = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cvam = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
        cvam1 = GameObject.Find("CM vcam6").gameObject.GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        canvas = GameObject.Find("UI").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //int i = PlayerPrefs.GetInt("level");
        //if (i == 2)
        //{
        //    if (Input.GetKeyDown(KeyCode.U) && player.GetComponent<beizhui>().iszhuizhu == false)
        //    {
        //        wait++;
        //        Debug.Log(wait);
        //        if (wait % 2 == 1&&player.GetComponent<knapsack>().isUse_Whistle==true)
        //        {
        //            player.GetComponent<player>().enabled = false;
        //            cvam1.Priority = 11;
        //            isdown = true;
        //            canvas.SetActive(false);
        //            PlayerPrefs.SetInt("isMoveCamera", 1);
                   

        //        }

        //        if (wait % 2 == 0)
        //        {
        //            PlayerPrefs.SetInt("isMoveCamera", 0);
        //            isdown = false;
        //            player.GetComponent<player>().enabled = true;
        //            cvam1.Priority = 5;
        //            canvas.SetActive(true);
                   
        //        }
        //    }

            //if(isdown==true)wait += Time.deltaTime;
            //if (wait > 2)
            //    {
            //        cvam1.Priority = 5;
            //        isdown = false;
            //    }
       // }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("level", 2);
        if (other.tag == "Player")
        {
            int i = PlayerPrefs.GetInt("guanqia");
            if (i <= 2)
            {
                PlayerPrefs.SetInt("guanqia", 2);
            }

        }
    }
}
