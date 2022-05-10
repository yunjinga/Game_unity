using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zhuizhu : MonoBehaviour
{
    public GameObject trigger_1;
    
    // Start is called before the first frame update
    void Start()
    {
        //trigger_1 = GameObject.Find("Start2").gameObject.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.gameObject.GetComponent<enemy>().isCatching==true|| transform.gameObject.GetComponent<enemy1>().isCatching == true|| transform.gameObject.GetComponent<enemy2>().isCatching == true)
        {
            trigger_1.SetActive(false);
        }
    }
}
