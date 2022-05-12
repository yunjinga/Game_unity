using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBox : MonoBehaviour
{
   
    public static bool Check(Vector3 location, Vector3 direct, float distance, float dis,float speed)  //使用函数的位置，方向，距离
    {
        RaycastHit hit;
        Vector3 change;
        if (Physics.Raycast(location, direct, out hit, distance))
        {
            //Debug.Log("location=" + location + "direct=" + direct + "distance=" + distance + hit.transform.tag);
            if (hit.transform.tag == "box")//如果碰撞到的物体tag为box则让箱子朝着目标方向继续生成一个射线
            {
                RaycastHit boxhit;
                if (Physics.Raycast(hit.transform.position, direct, out boxhit, dis))//如果射线检测到任意物体，则停止位移
                {
                    if (boxhit.transform != null)
                    {
                        change = new Vector3(0, 0, 0);
                    }
                    else//如果未检测到物体则可以位移
                    {
                        change = direct;
                    }
                }
                else
                {
                    change = direct;
                }
                hit.transform.Translate(change * Time.deltaTime*speed, Space.World);
                
                return true;
                //hit.transform.position = hit.transform.position + change * Time.deltaTime;
            }
            else
            {
                change = direct;
                return false;
                
            }
        }
        else
        {
            change = direct;
            return false;
        }
    }
}
