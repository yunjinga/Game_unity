using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPoint : MonoBehaviour
{
    public ChildPoint NextPoint;
    public ChildPoint NextNextPoint;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<walk>())
        {
            var yk = collision.transform.GetComponent<walk>();
            yk.NextPoint = NextPoint;
            ChildPoint c = NextPoint;
            NextPoint = NextNextPoint;
            NextNextPoint = c;
        }
    }
}
