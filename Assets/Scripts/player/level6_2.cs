using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level6_2 : MonoBehaviour
{
    public bool level6 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        level6 = true;
    }
    private void OnTriggerExit(Collider other)
    {
        level6 = false;
    }
}
