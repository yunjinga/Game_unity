using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jingjie3 : MonoBehaviour
{
    public Slider jingjie_1;
    public Slider jingjie_2;
    public GameObject follow;
    Vector3 screenPos;
    bool isShow = false;
    // Start is called before the first frame update
    void Start()
    {
        jingjie_1.gameObject.SetActive(false);
        jingjie_2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        jingjie_1.value = transform.GetComponent<enemy2>().waring / 20;
        jingjie_2.value = (transform.GetComponent<enemy2>().waring - 20) / 20;
        screenPos = Camera.main.WorldToScreenPoint(follow.transform.position);
        if (transform.GetComponent<enemy2>().waring > 0 && transform.GetComponent<enemy2>().waring < 20&&isShow==true)
        {
            jingjie_1.gameObject.SetActive(true);
            jingjie_1.transform.position = screenPos;
        }
        else
        {
            jingjie_1.gameObject.SetActive(false);
            jingjie_2.gameObject.SetActive(false);
        }
        if (transform.GetComponent<enemy2>().waring > 20&&isShow==true&& transform.GetComponent<enemy2>().waring < 40)
        {
            jingjie_1.gameObject.SetActive(false);
            jingjie_2.gameObject.SetActive(true);
            jingjie_2.transform.position = screenPos;
        }
        else
        {
            jingjie_1.gameObject.SetActive(false);
            jingjie_2.gameObject.SetActive(false);
        }
        //if (transform.GetComponent<enemy2>().waring == 0)
        //{
        //    jingjie_1.gameObject.SetActive(false);
        //    jingjie_2.gameObject.SetActive(false);
        //    isShow = false;
        //}
    }
    private void OnBecameInvisible()
    {
        jingjie_1.gameObject.SetActive(false);
        jingjie_2.gameObject.SetActive(false);
        isShow = false;
    }
    private void OnBecameVisible()
    {
        isShow = true;
    }
}
