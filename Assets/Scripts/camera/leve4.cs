using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leve4 : MonoBehaviour
{
    public CinemachineVirtualCamera cvam;
    public CinemachineVirtualCamera cvam1;
    float wait = 0;
    bool isdown = false;
    public GameObject player;
    public GameObject canvas;
    public static bool isUse=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

            //if(isdown==true)wait += Time.deltaTime;
            //if (wait > 2)
            //    {
            //        cvam1.Priority = 5;
            //        isdown = false;
            //    }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("level", 3);
        if (other.tag == "Player")
        {
            int i = PlayerPrefs.GetInt("guanqia");
            if (i <= 3)
            {
                PlayerPrefs.SetInt("guanqia", 3);
            }

        }
    }
}
