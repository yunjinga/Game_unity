using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trriger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        int i = PlayerPrefs.GetInt("guanqia");
        PlayerPrefs.SetInt("guanqia", 0);
        Debug.Log(1);
    }
}
