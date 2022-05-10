using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            var yk = collision.transform.GetComponent<player_blood>();
            yk.blood -= 1;
        }
        
    }

}
