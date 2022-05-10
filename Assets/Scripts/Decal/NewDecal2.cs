using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDecal2 : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public Transform Decal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            ray = new Ray();
        }
        if(Physics.Raycast(ray, out hit))
        {
            Decal.position = hit.point;
            Vector3 pos = hit.collider.transform.position;
            Decal.LookAt(pos);
        }
    }
}
