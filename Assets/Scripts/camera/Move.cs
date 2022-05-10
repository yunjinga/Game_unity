using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;
    Vector3 initialposition;
    void Start()
    {
        initialposition = transform.position;
        //Debug.Log(initialposition.z);
    }

    // Update is called once per frame
    void Update()
    {
        int i = PlayerPrefs.GetInt("isMoveCamera");
        if (i == 1)
        {
            if (Input.GetKey(KeyCode.W) && transform.position.y > initialposition.y -3)
            {
                transform.Translate(0, 0, speed);
                


            }
            else if (Input.GetKey(KeyCode.S) && transform.position.y < initialposition.y + 3)
            {
                transform.Translate(0, 0, -speed);

            }
            else if (Input.GetKey(KeyCode.A) && transform.position.z > initialposition.z - 10)
            {
                transform.Translate(-speed, 0, 0);

            }
            else if (Input.GetKey(KeyCode.D) && transform.position.z < initialposition.z + 10)
            {
                transform.Translate(speed, 0, 0);

            }

        }

    }
}

