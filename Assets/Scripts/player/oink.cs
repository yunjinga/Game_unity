using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oink : MonoBehaviour
{
    GameObject scope;
    public static void creat(GameObject scope,Vector3 postion)
    {
        scope = GameObject.Instantiate(scope);
        scope.transform.position = postion+new Vector3(0,0.1f,0);
        Destroy(scope,3f);
    }
    public static void follow(GameObject scope,Vector3 postion)
    {
        scope.transform.position = postion + new Vector3(0, 0.1f, 0);
    }
   
}
