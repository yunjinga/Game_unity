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


        //��ȡ���ֳ�������Ͷ������

        animator = this.GetComponent<Animator>();
        arm_static.SetActive(true);
        arm_fly.SetActive(false);
        //head.SetActive(visible);
    }

    // Update is called once per frame
    void Update()
    {
        //head.SetActive(visible);

        //�ж϶����Ƿ�������������ֳ�����ʾ������
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(info.IsName("Fly"));
        if (animator.GetBool("isFly"))
        {
            Debug.Log("үү����");
            arm_static.SetActive(false);
            arm_fly.SetActive(true);
        }
    }
}
