using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenario2 : MonoBehaviour
{
    GameObject L1;
    GameObject L2;
    GameObject L3;
    GameObject L4;
    GameObject L5;
    GameObject L6;
    // Start is called before the first frame update
    void Start()
    {
        L1 = GameObject.Find("1_1");
        L2 = GameObject.Find("1_2");
        L3 = GameObject.Find("1_3");
        L4 = GameObject.Find("1_4");
        L5 = GameObject.Find("1_5");
        L6 = GameObject.Find("1_6");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<player>())
        {
            L1.SetActive(true);
            L2.SetActive(true);
            L3.SetActive(true);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
        }

    }
}
