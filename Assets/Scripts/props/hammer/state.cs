using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class state : MonoBehaviour
{
    public GameObject zhuangtai;
    float wait=0;
    // Start is called before the first frame update
    void Start()
    {
       // zhuangtai = GameObject.Find("zhuangtai").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (zhuangtai.activeInHierarchy == true)
        {
            wait += Time.deltaTime;
            if (wait >= 2)
            {
                zhuangtai.SetActive(false);
            }
        }
    }
}
