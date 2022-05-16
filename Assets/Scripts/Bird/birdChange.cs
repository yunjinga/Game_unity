using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdChange : MonoBehaviour
{
    // Start is called before the first frame update
    //public bool visible = true;
    //GameObject head;
    public GameObject arm_fly;
    public GameObject arm_static;
    private Animator animator;
    public bool firstFly = false;
    void Awake()
    {

    }
    void Start()
    {


        //获取两种翅膀的组件和动画组件

        animator = this.GetComponent<Animator>();
        arm_static.SetActive(true);
        arm_fly.SetActive(false);
        //head.SetActive(visible);
    }

    // Update is called once per frame
    void Update()
    {
        //head.SetActive(visible);

        //判断动画是否结束来设置两种翅膀的显示与隐藏
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(info.IsName("Fly"));
        if (animator.GetBool("isFly"))
        {
            Debug.Log("爷爷飞了");
            arm_static.SetActive(false);
            arm_fly.SetActive(true);
        }
    }
}
