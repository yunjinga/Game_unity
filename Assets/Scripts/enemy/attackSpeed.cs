using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackSpeed : MonoBehaviour
{
    public static void changeSpeed(Animator ator)
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = ator.GetCurrentAnimatorStateInfo(0);
        if (animatorInfo.IsName("attack"))
        {
            ator.speed = 1.5f;
        }
        else if(!animatorInfo.IsName("attack"))
        {
            ator.speed = 1f;
        }
    }
}
