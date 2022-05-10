using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class level6 : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("level", 5);
        if (other.tag == "Player")
        {
            int i = PlayerPrefs.GetInt("guanqia");
            if (i <= 5)
            {
                PlayerPrefs.SetInt("guanqia", 5);
            }

        }
    }
}
