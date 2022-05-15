using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_level : MonoBehaviour
{
    Animator ator;
    // Start is called before the first frame update
    void Start()
    {
        ator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ator)
        ator.Play("idle");
    }
}
