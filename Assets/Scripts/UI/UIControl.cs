using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public CinemachineVirtualCamera cvam;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        UI=transform.gameObject;
        cvam = GameObject.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cvam.Priority == 10)
        {
            UI.SetActive(true);
        }
        if(cvam.Priority == 4)
        {
            UI.SetActive(false);
        }
    }
}
