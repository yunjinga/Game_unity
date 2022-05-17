using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1 : MonoBehaviour
{
    public CinemachineVirtualCamera cineVirtual;
    bool isdown = false;
    public GameObject dove;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (isdown)
        {
            if (dove.gameObject.GetComponent<Dove>().isAll == false&& dove.gameObject.GetComponent<Dove>().isdown==true)
            {
                cineVirtual.Priority = 11;

            }
            else
            {
                cineVirtual.Priority = 5;
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isdown = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isdown = false;
    }
}
