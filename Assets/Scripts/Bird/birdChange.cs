using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdChange : MonoBehaviour
{
    // Start is called before the first frame update
    public bool visible = true;
    //GameObject head;
    GameObject arm_fly;
    GameObject arm_static;
    private Animator animator;
    void Awake()
    {
        //head = GameObject.Find("1_1/Enemy1/NPC01_Apose (1) Variant/NPC_01_head");
    }
    void Start()
    {
        //head = GameObject.Find("1_1/Enemy1/NPC01_Apose (1) Variant/NPC_01_head");

        //获取两种翅膀的组件和动画组件
        arm_fly = GameObject.Find("");
        arm_static = GameObject.Find("");
        animator = this.GetComponent<Animator>();
        //head.SetActive(visible);
    }

    // Update is called once per frame
    void Update()
    {
        //head.SetActive(visible);

        //判断动画是否结束来设置两种翅膀的显示与隐藏
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1.0f)
        {
            arm_static.SetActive(true);
            arm_fly.SetActive(false);
        }
        else
        {
            arm_static.SetActive(false);
            arm_fly.SetActive(true);
        }
    }
}
