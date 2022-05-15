using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level6_final : MonoBehaviour
{
    public GameObject Anim;
    // Start is called before the first frame update
    public AudioSource source;
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        Anim.SetActive(false);
        source = GetComponent<AudioSource>();
        source.Stop();
        car.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Anim.SetActive(true);
        car.SetActive(true);
        source.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        Anim.SetActive(false);
        car.SetActive(false);
        car.GetComponent<CinemachineDollyCart>().m_Position = 0;
    }
}

