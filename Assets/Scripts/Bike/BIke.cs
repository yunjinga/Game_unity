using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIke : MonoBehaviour
{
    public bool isRight = false;
    public bool isLeft = false;
    public bool isRotating = false;
    Vector3 r,l;
    bool isUse = false;
    // Start is called before the first frame update
    void Start()
    {
        r = transform.localEulerAngles;
        r.x += 40.724f;
        l= transform.localEulerAngles;
        l.x += -40.724f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeft)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(l), 3 * Time.deltaTime);
            isRotating = true;
        }
        else if(isRight)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(r), 3 * Time.deltaTime);
            isRotating = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<player>())
        {
            if(other.gameObject.GetComponent<Animator>())
            {
                Animator ator = other.gameObject.GetComponent<Animator>();
                AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
                // 判断动画是否播放完成
                if (info.IsName("attack"))
                {
                    Debug.Log(Vector3.Cross(other.transform.forward, transform.position).y) ;
                    if (Vector3.Cross(other.transform.forward, transform.position).y > 0)//为正右方
                    {
                        isRight = true;
                    }
                    else
                    {
                        isLeft = true;
                    }
                }
            }
        }
        else if(other.tag=="Bike")
        {
            if(other.gameObject.GetComponent<BIke>().isRotating && !isRotating)
            {
                isLeft = other.gameObject.GetComponent<BIke>().isLeft;
                isRight = other.gameObject.GetComponent<BIke>().isRight;
            }
           
        }
    }
}
