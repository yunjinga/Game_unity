using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level6_final : MonoBehaviour
{
    public GameObject Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Anim.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Anim.SetActive(false);
    }
}
