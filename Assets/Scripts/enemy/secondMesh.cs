using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondMesh : MonoBehaviour
{
    private Mesh mesh;//mesh
    private Vector3[] vertices;
    int[] tringles;//mesh的tringles
    Vector2[] _uvs;//mesh的uv
    MeshFilter mf;
    MeshRenderer mr;
    public float viewRadius = 3f;
    public float viewRugle = 45;
    public float viewAngleStep = 30f;

    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mf = transform.gameObject.AddComponent<MeshFilter>();
        mr = transform.gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = mesh;
        //Debug.Log(transform.gameObject.name);
        mesh.name = "mesh";
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.GetComponent<enemy>())
        {
            viewAngleStep = transform.parent.gameObject.GetComponent<enemy>().viewAngleStep;
            viewRugle = transform.parent.gameObject.GetComponent<enemy>().viewRugle;
            viewRadius = transform.parent.gameObject.GetComponent<enemy>().viewRadius;
        }
        else if (transform.parent.gameObject.GetComponent<enemy1>())
        {
            viewAngleStep = transform.parent.gameObject.GetComponent<enemy1>().viewAngleStep;
            viewRugle = transform.parent.gameObject.GetComponent<enemy1>().viewRugle;
            viewRadius = transform.parent.gameObject.GetComponent<enemy1>().viewRadius;
        }
        else if (transform.parent.gameObject.GetComponent<enemy2>())
        {
            viewAngleStep = transform.parent.gameObject.GetComponent<enemy2>().viewAngleStep;
            viewRugle = transform.parent.gameObject.GetComponent<enemy2>().viewRugle;
            viewRadius = transform.parent.gameObject.GetComponent<enemy2>().viewRadius;
        }
        //Debug.Log(transform.position);
        _uvs = new Vector2[(int)(viewAngleStep + 1)];
        vertices = new Vector3[(int)viewAngleStep + 1];
        tringles = new int[((int)viewAngleStep - 1) * 3];
        //vertices[0] = transform.InverseTransformPoint(transform.position);
        vertices[0] = Vector3.zero;
        _uvs[0] = new Vector2(1f, 1f);
        // 获得最左边那条射线的向量，相对正前方，角度是-rugle
        Vector3 forward_left = Quaternion.Euler(0, -viewRugle, 0) * transform.forward * viewRadius;
        // 依次处理每一条射线
        //Debug.Log(transform.forward);
        for (int i = 0; i < viewAngleStep; i++)
        {
            // 每条射线都在forward_left的基础上偏转一点，最后一个正好偏转2*rugle度到视线最右侧
            Vector3 v = Quaternion.Euler(0, (viewRugle * 2 / viewAngleStep) * i, 0) * forward_left;
            float currAngle = Mathf.Deg2Rad * v.y;
            float x = Mathf.Cos(currAngle);
            float y = Mathf.Sin(currAngle);
            _uvs[i] = new Vector2(x * 0.5f + 0.5f, y * 0.5f + 0.5f);
            // 创建射线
            Ray ray = new Ray(transform.position, v);
            RaycastHit hitt = new RaycastHit();
            // 射线只与两种层碰撞，注意名字和你添加的layer一致，其他层忽略
            int mask = LayerMask.GetMask("Obstacle", "Enemy", "OcclusionTran");
            Physics.Raycast(ray, out hitt, viewRadius, mask);

            // Player位置加v，就是射线终点pos
            Vector3 pos = transform.position + v;
            vertices[i + 1] = transform.InverseTransformPoint(pos);
        }
        mesh.Clear();
        for (int i = 0; i < (int)viewAngleStep - 1; i++)
        {
            tringles[3 * i] = 0;
            tringles[3 * i + 1] = i + 1;
            tringles[3 * i + 2] = i + 2;
        }
        /*for (int i = 0; i < viewAngleStep - 1; i++)
        {
            Debug.Log("tringles[i * 3]=" + tringles[i * 3] + " tringles[i * 3 + 1]=" + tringles[i * 3 + 1] + " tringles[i * 3 + 2]=" + tringles[i * 3 + 2]);
        }*/
        mesh.vertices = vertices;
        mesh.triangles = tringles;
        mesh.uv = _uvs;
        mesh.RecalculateNormals();
        mf.mesh = mesh;
        material = new Material(Shader.Find("Custom/changeColorFirst"));
        mr.material = material;
        //mr.material.shader = shader;
        //mr.material.color = Color.red;
    }
}
