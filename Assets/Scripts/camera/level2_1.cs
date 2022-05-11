using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level2_1 : MonoBehaviour
{
    public GameObject biaoqing;
   
    // Start is called before the first frame update
    void Start()
    {
        biaoqing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.gameObject.GetComponent<camera6_1>().istalk2_1 == true)
        {
            biaoqing.SetActive(true);
            transform.gameObject.GetComponent<camera6_1>().istalk2_1 = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            biaoqing.SetActive(false);
        }
    }

}
