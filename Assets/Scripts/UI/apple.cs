using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : MonoBehaviour
{
    public GameObject texture_apple;
    bool isdown=false;
    int num = 0;
    private void Start()
    {
        texture_apple = GameObject.Find("texture2_1");
        texture_apple.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&isdown==true)
        {
            num++;
            if (num % 2 == 1)
            {
                texture_apple.SetActive(true);
            }
            else
            {
                texture_apple.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isdown = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isdown = false;
    }
}
