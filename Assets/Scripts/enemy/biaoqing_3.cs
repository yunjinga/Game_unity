using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biaoqing_3 : MonoBehaviour
{
    public Transform Hand;
    Vector3 screenPos;
    public GameObject player;
    float wait = 0;
    //public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        //      public Transform Hand;//敌人的idle表情
        //Vector3 screenPos;

    }

    // Update is called once per frame
    void Update()
    {
        float distance_enemy = Vector3.Distance(Hand.position, player.transform.position);
        if (Hand.gameObject.GetComponent<enemy2>().waring == 0 && distance_enemy < 6) //&& Hand.GetComponent<jingjie1>().isShow == true)
        {
            transform.gameObject.SetActive(true);
            screenPos = Camera.main.WorldToScreenPoint(Hand.position);
            transform.position = screenPos + new Vector3(0, 250, 0);
            wait += Time.deltaTime;
        }
        else
        {
            transform.gameObject.SetActive(false);
        }
        if (wait > 3)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
