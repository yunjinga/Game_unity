using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level5_1 : MonoBehaviour
{
    public GameObject baige;
    public Text text;
    public GameObject texture;
    public GameObject tishi;
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {texture = GameObject.Find("texture5_1");
        baige.SetActive(false);
        texture.SetActive(false);
        tishi.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    num++;
        //}
        //switch (num)
        //{
        //    case 1:
        //        {
        //            baige.SetActive(true);
        //            text.text = "这里的人越来越多了";
        //            texture.SetActive(true);
        //            tishi.SetActive(true);
        //            break;
        //        }
        //    case 2:
        //        {
        //            baige.SetActive(false);
        //            texture.SetActive(false);
        //            tishi.SetActive(false);
        //            break;
        //        }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        baige.SetActive(true);
        text.text = "这里的人越来越多了";
        texture.SetActive(true);
        tishi.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        num = 0;
        baige.SetActive(false);
        texture.SetActive(false);
        tishi.SetActive(false);
    }
}
