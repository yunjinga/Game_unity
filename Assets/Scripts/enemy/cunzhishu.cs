using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cunzhishu : MonoBehaviour
{
    Animator ator;
    GameObject parent;
    List<GameObject> l1 = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ator = transform.GetComponent<Animator>();
        parent = transform.parent.gameObject;
        foreach (Transform item in parent.GetComponentsInChildren<Transform>())
        {
            if (!item.GetComponent<cunzhishu>())
            {
                l1.Add(item.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);

        if ((info.IsName("push")))
        {
            foreach (GameObject item in l1)
            {
                if(item.GetComponent<enemy>())
                {
                    item.GetComponent<enemy>().waring = 20;
                }
                else if (item.GetComponent<enemy1>())
                {
                    item.GetComponent<enemy1>().waring = 20;
                }
                else if (item.GetComponent<enemy2>())
                {
                    item.GetComponent<enemy2>().waring = 20;
                }
            }
        }
    }
}
