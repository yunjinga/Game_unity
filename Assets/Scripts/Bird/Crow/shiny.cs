using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiny : MonoBehaviour
{
    public GameObject bird;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            bird.transform.gameObject.GetComponent<Crow>().isComplete = true;
            Destroy(this.gameObject);
        }


    }
}
