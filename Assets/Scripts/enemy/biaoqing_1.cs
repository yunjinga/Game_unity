using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biaoqing_1 : MonoBehaviour
{
    public Transform Hand;
    Vector3 screenPos;
    public GameObject player;
    float wait=0;
    //public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        //      public Transform Hand;//敌人的idle表情
        //Vector3 screenPos;
        //transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance_enemy = Vector3.Distance(Hand.position, player.transform.position);
       // Vector3 distance = Hand.position - player.transform.position;
        if (Hand.gameObject.GetComponent<enemy>().waring == 0 && distance_enemy<7&&Hand.GetComponent<jingjie1>().isShow==true)
        {
            transform.gameObject.SetActive(true);
            wait+= Time.deltaTime;
            screenPos = Camera.main.WorldToScreenPoint(Hand.position);
            transform.position = screenPos + new Vector3(80, 200, 0);
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
