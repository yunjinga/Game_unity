using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public point point;
    public point TheLastPoint;
    public float speed=3f;
    Animator ator;
    // Start is called before the first frame update
    void Start()
    {
        ator = transform.GetComponent<Animator>();
        transform.GetComponent<Fly>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, point.transform.position, step);
        Vector3 p= point.transform.position - transform.position;
        p.y = 0;
        transform.forward = p; 
        if(point == TheLastPoint && Vector3.Distance(transform.position,TheLastPoint.transform.position)<0.8f)
        {
            ator.SetBool("isFly", false);
            ator.SetBool("isLand", true);
            AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
            // 判断动画是否播放完成
            if ((info.normalizedTime > 0.99f) && (info.IsName("Land")))
            {
                ator.SetBool("isLand", false);
                ator.SetBool("isIdle", true);
            }
        }
        else
        {
            ator.SetBool("isFly", true);
        }
    }
}
