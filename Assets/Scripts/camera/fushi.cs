using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fushi : MonoBehaviour
{
    public GameObject canvas;
    public GameObject fus;
    float wait = 0;
    bool isplay = false;
    public GameObject gaming;
    int cishu = 0;
    public GameObject player;
    public GameObject[] canvas_enemy;
    public bool ishei=false;
    public bool iswhite = false;
    // public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("UI").gameObject;
        canvas_enemy = GameObject.FindGameObjectsWithTag("UI");
        wait = 0;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isplay == true)
        {
            for(int i = 0; i < canvas_enemy.Length; i++)
            {
                canvas_enemy[i].SetActive(false);
            }
           
            wait += Time.deltaTime;
            
            if (wait >= 5.5)
            { 
                fus.SetActive(false);
               
                for (int i = 0; i < canvas_enemy.Length; i++)
                {
                    canvas_enemy[i].SetActive(true);
                }
                //if (!ishei)
                //{
                //    Camera.main.GetComponent<screenChange>().SetBlackBool = true;
                //    ishei = !ishei;
                //}
            }
            if (wait >= 8)
            {
                gaming.SetActive(true);
                canvas.SetActive(true);  
                //if (!iswhite)
                //{
                //    Camera.main.GetComponent<screenChange>().SetWhiteBool = true;
                //    iswhite = !iswhite;
                //}
               
            }
            
            //Event key = Event.current;
            //FunctionKeyCodeV1(key);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wait = 9;
            }
            
        }
     
    }
    //void OnGUI()

    //{
    //    
    //}


    private void FunctionKeyCodeV1(Event key)
    {
        if (key.isKey)//如果“事件”有效，并且“允许判断按下”。
        {
            switch (key.keyCode)
            {
                case KeyCode.DownArrow:
                    wait = 9;
                    break;

                default:
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        cishu++;
        if (other.tag == "Player" && cishu == 1 && player.GetComponent<beizhui>().iszhuizhu == false)
        {    
            fus.SetActive(true);
            isplay = true;
            gaming.SetActive (false);
            canvas.SetActive(false);
            
        //animator.Play
           

        } 
        if(other.tag == "Player" && cishu == 1)
        {  
            int i = PlayerPrefs.GetInt("guanqia");
            if (i <= 1)
            {
               PlayerPrefs.SetInt("guanqia", 1);
            }
        }
      
       
    }
}
