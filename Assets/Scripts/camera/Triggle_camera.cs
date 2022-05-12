using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Triggle_camera : MonoBehaviour
{
    public CinemachineVirtualCamera cineVirtual;
    bool isTrigger = false;
    bool isfirst = false;
    public float speed = 2.0f;
    public GameObject ziji;
    int cishu = 0;
    public GameObject Anim_baige;
    public GameObject baige;
    // public static bool  isMove = true;
    int ancishu = 0;
   // public GameObject duihua_one;
    //public GameObject duihua_two;
    float wait = 0;
    bool next=false;
    public GameObject player;
    public Text text;
    public GameObject tishi;
    int num = 0;
    bool isstart = false;

   

    private void Start()
    {
        tishi.SetActive(false);
        player = GameObject.Find("Player");
        //duihua_one.SetActive(false);
        //duihua_two.SetActive(false);

        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTrigger == true)
        {
            num++;
        }
       
        var cineBody = (CinemachineFramingTransposer)cineVirtual.GetCinemachineComponent(CinemachineCore.Stage.Body); 
        //next_duihua();
        if (isTrigger == true&&num!=0)
        {
            //Debug.Log("1");
            tishi.SetActive(false);
            wait += Time.deltaTime;
            if (cineBody.m_CameraDistance >= 2.5)
            {
                cineBody.m_CameraDistance = cineBody.m_CameraDistance - speed * Time.deltaTime;
            }
            //else
            //{
            //    cineBody.m_CameraDistance = 2.4f;
            //}
            
        }
        if (isTrigger == false&&isstart==true )
        {
            if (cineBody.m_CameraDistance < 4f)
            {
                cineBody.m_CameraDistance = cineBody.m_CameraDistance + speed * Time.deltaTime;
            }
            
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu==false)
        {
            num = 0;
            cishu++;
            //Debug.Log(cishu);
             tishi.SetActive(true);
           
            isTrigger = true;
            Anim_baige.SetActive(true);
           
            //player.gameObject.GetComponent<player>().enabled = false;
        }

        int i = PlayerPrefs.GetInt("guanqia");
        if (i <= 0)
        {
            PlayerPrefs.SetInt("guanqia", 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && player.GetComponent<beizhui>().iszhuizhu == false)
        {
            isTrigger = false;
            if (cishu == 1) isfirst = true;
            else isfirst = false;
            tishi.SetActive(false);
            baige.GetComponent<Dove>().duihua.SetActive(false);
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