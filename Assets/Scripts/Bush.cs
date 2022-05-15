using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    // Start is called before the first frame update
    
    

    private void Awake()
    {
       
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<player>())
        {
            var yk = other.transform.GetComponent<player>();
            yk.isInvisible = false;
            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<player>())
        {
            var yk = other.transform.GetComponent<player>();
            yk.isInvisible = true;
        }
    }
}
