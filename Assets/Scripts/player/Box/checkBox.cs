using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBox : MonoBehaviour
{
   
    public static bool Check(Vector3 location, Vector3 direct, float distance, float dis,float speed)  //ʹ�ú�����λ�ã����򣬾���
    {
        RaycastHit hit;
        Vector3 change;
        if (Physics.Raycast(location, direct, out hit, distance))
        {
            //Debug.Log("location=" + location + "direct=" + direct + "distance=" + distance + hit.transform.tag);
            if (hit.transform.tag == "box")//�����ײ��������tagΪbox�������ӳ���Ŀ�귽���������һ������
            {
                RaycastHit boxhit;
                if (Physics.Raycast(hit.transform.position, direct, out boxhit, dis))//������߼�⵽�������壬��ֹͣλ��
                {
                    if (boxhit.transform != null)
                    {
                        change = new Vector3(0, 0, 0);
                    }
                    else//���δ��⵽���������λ��
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
