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
    public GameObject wupin;
    public GameObject biaoqing;
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
        wupin.SetActive(false);
        biaoqing.SetActive(false);
        source.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        Anim.SetActive(false);
        car.SetActive(false);
        wupin.SetActive(true);
        biaoqing.SetActive(true);
        car.GetComponent<CinemachineDollyCart>().m_Position = 0;
    }
}

