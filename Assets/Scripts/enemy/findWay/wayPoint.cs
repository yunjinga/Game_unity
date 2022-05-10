using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPoint : MonoBehaviour
{
    public wayPoint NextPoint;
    public wayPoint NextNextPoint;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy2")
        {


            var yk = collision.transform.GetComponent<enemy1>();
            yk.NextPoint = NextPoint;
            wayPoint c = NextPoint;
            NextPoint = NextNextPoint;
            NextNextPoint = c;


        }
    }
}
