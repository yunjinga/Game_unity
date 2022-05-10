using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class screenChange : MonoBehaviour
{
    public Material ma;
    float count;
    public bool SetWhiteBool = false;
    public bool SetBlackBool = false;
    public float ChangeTime = 0.3f;
    // Use this for initialization
    void Start()
    {
        if (ma == null)
        {
            ma = new Material(Shader.Find("Custom/screenChange"));
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (SetWhiteBool)
        {
            SetWhite();
        }
        else if (SetBlackBool)
        {
            SetBlack();
        }

    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //����Դ����Ŀ����Ⱦ��������Ҫ������ʵ��ͼ��Ч����  
        //Blit����dest���������Ⱦ�����ڲ���������source��Ϊ  
        //_MainTex���ԣ����һ���һ��ȫ�����顣  
        Graphics.Blit(source, destination, ma);
    }


    void SetWhite()
    {
        count = ma.GetFloat("_Float1") + ChangeTime * Time.deltaTime;
        ma.SetFloat("_Float1", count);
        if (ma.GetFloat("_Float1") >= 1)
        {
            SetWhiteBool = false;
        }
    }


    void SetBlack()
    {
        count = ma.GetFloat("_Float1") - ChangeTime * Time.deltaTime;
        ma.SetFloat("_Float1", count);
        if (ma.GetFloat("_Float1") <= 0)
        {
            SetBlackBool = false;
        }
    }
}
