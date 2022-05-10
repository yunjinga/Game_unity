using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Check(Vector3 v)
    {
        Ray ray = new Ray(transform.position, transform.position+v);
        RaycastHit hitt = new RaycastHit();
        Physics.Raycast(ray, out hitt, 1.3f);
        if(hitt.transform!=null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
