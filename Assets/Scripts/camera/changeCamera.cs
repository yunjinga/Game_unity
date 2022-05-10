using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCamera : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    // Start is called before the first frame update
    void Start()
    {
        camera1 = transform.gameObject.GetComponent<Camera>();
        camera2 = GameObject.Find("Camera1").gameObject.GetComponent<Camera>();
        camera1.enabled = true;
        camera2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !camera2.enabled)
        {
            camera2.enabled = true;
            camera1.enabled = false;
            GameObject.Find("Player").gameObject.GetComponent<player>().enabled=false;
        }
        else if (Input.GetKeyDown(KeyCode.U) && camera2.enabled)
        {
            camera2.enabled = false;
            camera1.enabled = true;
            GameObject.Find("Player").gameObject.GetComponent<player>().enabled = true;
        }
    }
}
