using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour
{
    public point NextPoint;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Dove>())
        {
            var yk = collision.transform.GetComponent<Fly>();
            yk.point = NextPoint;
        }
    }
   
}
