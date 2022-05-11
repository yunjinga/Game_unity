using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caidi : MonoBehaviour
{
    public bool isT = false;
    public GameObject texture;
    public GameObject duihua;
    int num = 0;
    //public GameObject baige;
    // public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        texture = GameObject.Find("texture3_1");
       // duihua = GameObject.Find("baige");
        texture.SetActive(false);
        duihua.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isT == true)
        {
            num++;
        }
        if (num == 1)
        {
            texture.SetActive(true);
        }
        if (num == 2)
        {
            texture.SetActive(false);
            duihua.SetActive(true);
        }
        if (num == 3)
        {
            texture.SetActive(false);
            duihua.SetActive(false);
            isT = false;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isT = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        num = 0;
        isT = false;
    }
}
