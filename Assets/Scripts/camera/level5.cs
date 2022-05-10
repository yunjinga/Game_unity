using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level5 : MonoBehaviour
{
    // Start is called before the first frame update
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("level", 4);
        if (other.tag == "Player")
        {
            int i = PlayerPrefs.GetInt("guanqia");
            if (i <= 4)
            {
                PlayerPrefs.SetInt("guanqia", 4);
            }

        }
    }
}
