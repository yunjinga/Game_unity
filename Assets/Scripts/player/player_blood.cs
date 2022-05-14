using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_blood : MonoBehaviour
{
    public int blood = 1;
    public GameObject camera;
    AudioSource ad;
    AudioSource ad1;
    // Start is called before the first frame update
    void Start()
    {
        ad = camera.transform.GetComponent<AudioSource>();
        ad1 = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blood == 0)
        {
            ad.Stop();
            ad1.Stop();
        }
    }
}