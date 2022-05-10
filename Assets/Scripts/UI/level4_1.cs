using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level4_1 : MonoBehaviour
{
    public GameObject texture;
    bool isdown=false;
   
    // Start is called before the first frame update
    void Start()
    {
        texture = GameObject.Find("texture4_1");
        texture.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isdown == true)
        {
            texture.SetActive(true);
           
        } 
        if (Input.GetKeyDown(KeyCode.Space)&&texture.active==true)
            {
            texture.SetActive(false);
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        isdown = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isdown = false;
        texture.SetActive(false);
    }
}
