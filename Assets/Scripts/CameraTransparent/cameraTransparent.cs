//����İ�͸���ű�

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class cameraTransparent : MonoBehaviour
{
    // Start is called before the first frame update
    public class TransparentParam
    {
        public Material[] materials = null;
        public Material[] sharedMats = null;
        public float currentFadeTime = 0;
        public bool isTransparent = true;
    }

    public Transform targetObject = null; //Ŀ������
    public float height = 3.0f; //Ŀ�����Y����ƫ��
    public float destTransparent = 0.5f; //�ڵ���͸�����հ�͸ǿ��;
    public float fadeInTime = 1.0f; //��ʼ�ڵ���͸ʱ����ʱ��
    private int transparentLayer; //��Ҫ�ڵ���͸�Ĳ㼶
    private Dictionary<Renderer, TransparentParam> transparentDic = new Dictionary<Renderer, TransparentParam>();
    private List<Renderer> clearList = new List<Renderer>();

    void Start()
    {
        transparentLayer = 1 << LayerMask.NameToLayer("OcclusionTran");
        //targetObject = transform.Find("Cube (1)");
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject == null)
            return;
        UpdateTransparentObject();
        UpdateRayCastHit();
        RemoveUnuseTransparent();
    }

    public void UpdateTransparentObject()
    {
        var var = transparentDic.GetEnumerator();
        while (var.MoveNext())
        {
            TransparentParam param = var.Current.Value;
            param.isTransparent = false;
            foreach (var mat in param.materials)
            {
                Color col = mat.GetColor("_Color");
                param.currentFadeTime += Time.deltaTime;
                float t = param.currentFadeTime / fadeInTime;
                col.a = Mathf.Lerp(1, destTransparent, t);
                mat.SetColor("_Color", col);
            }
        }
    }

    public void UpdateRayCastHit()
    {
        RaycastHit[] rayHits = null;
        //���߷���Ϊ�����������ָ��Ŀ��λ��
        Vector3 targetPos = targetObject.position + new Vector3(0, height, 0);
        Vector3 viewDir = (targetPos - transform.position).normalized;
        Vector3 oriPos = transform.position;
        float distance = Vector3.Distance(oriPos, targetPos);
        Ray ray = new Ray(oriPos, viewDir);
        rayHits = Physics.RaycastAll(ray, distance, transparentLayer);
        //ֱ����Scene��һ���ߣ�����۲�����
        Debug.DrawLine(oriPos, targetPos, Color.red);
        foreach (var hit in rayHits)
        {
            Renderer[] renderers = hit.collider.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                AddTransparent(r);
            }
        }
    }

    public void RemoveUnuseTransparent()
    {
        clearList.Clear();
        var var = transparentDic.GetEnumerator();
        while (var.MoveNext())
        {
            if (var.Current.Value.isTransparent == false)
            {
                //��������ʵ���������٣����Ա�unloadunuseasset���ٻ��г������١�
                var.Current.Key.materials = var.Current.Value.sharedMats;
                clearList.Add(var.Current.Key);
            }
        }
        foreach (var v in clearList)
            transparentDic.Remove(v);
    }

    void AddTransparent(Renderer renderer)
    {
        TransparentParam param = null;
        transparentDic.TryGetValue(renderer, out param);
        if (param == null)
        {
            param = new TransparentParam();
            transparentDic.Add(renderer, param);
            //�˴�˳���ܷ�������material���������ʵ����
            param.sharedMats = renderer.sharedMaterials;
            param.materials = renderer.materials;
            foreach (var v in param.materials)
            {
                v.shader = Shader.Find("Custom/Transparent");
            }
        }
        param.isTransparent = true;
    }
}
