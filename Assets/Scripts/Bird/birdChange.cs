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
        //head = GameObject.Find("1_1/Enemy1/NPC01_Apose (1) Variant/NPC_01_head");
    }
    void Start()
    {
        //head = GameObject.Find("1_1/Enemy1/NPC01_Apose (1) Variant/NPC_01_head");

        //获取两种翅膀的组件和动画组件
        animator = this.GetComponent<Animator>();
        arm_static.SetActive(false);
        arm_fly.SetActive(true);
        //head.SetActive(visible);
    }

    // Update is called once per frame
    void Update()
    {
        //head.SetActive(visible);

        //判断动画是否结束来设置两种翅膀的显示与隐藏
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(info.IsName("Fly"));
        if (info.normalizedTime > 0.99f && info.IsName("Fly"))
        {

            //if(firstFly==false)
            //firstFly = !firstFly;
            arm_static.SetActive(true);
            arm_fly.SetActive(false);
        }
        else if (info.normalizedTime <= 0.99f && info.IsName("Fly"))
        {
            arm_static.SetActive(false);
            arm_fly.SetActive(true);
        }
    }
}
