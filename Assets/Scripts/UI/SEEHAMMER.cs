using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEEHAMMER : MonoBehaviour
{
    public GameObject image;
    public GameObject Hammer;
    float wait = 0;
    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector3.Distance(transform.position, player.transform.position);
        //if (distance < 5)
        //{
        //    wait += Time.deltaTime;
        //    image.SetActive(true);
        //}
        //if (wait > 3)
        //{
        //    image.SetActive(false);
        //}
    }
}
