using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level4_1 : MonoBehaviour
{
    public GameObject texture;
    bool isdown=false;
    public GameObject tishi;
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        tishi.SetActive(false);
        texture = GameObject.Find("texture4_1");
        texture.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isdown == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                num++;
            }
            if (num%2==1)
            {
                texture.SetActive(true);
            }
            
            
        } 
        if (num%2==0)
            {
            texture.SetActive(false);
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        isdown = true;
        tishi.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        isdown = false;
        texture.SetActive(false);
    }
}
