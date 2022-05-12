using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class enemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float xOffset;
    public float yOffset;
    public GameObject biaoqing_1;
    public GameObject biaoqing_2;
    //xinjia
    float x_offset = -30;
    float y_offset = 300;
   
    //public RectTransform rectTransform;
    //public RectTransform rectTransform_one;
    //public RectTransform rectTransform_two;
    public Transform goal;
    public float viewRadius = 3f, viewRadius_normal = 3f, viewRadius_waring = 5f;// ������Ұ��Զ�ľ���;����ľ���
    public float viewRugle = 45, viewRugle_normal = 45, viewRugle_waring = 60;//���߽Ƕ�
    public float viewAngleStep = 30f, normal_step = 30f, waring_step = 45f; //���������;�������������
    public wayPoint NextPoint;//Ѱ·��
    public float PursuitTime = 0f; //׷��ʱ��
    public Vector3 begin;//��ʼʱ��λ��

    public float waring = 0f;//����ֵ
    public float maxWaring = 40f, midleWaring = 20f;//��󾯽�ֵ ���м侯��ֵ

    public bool isCatching, isOink = false;//�Ƿ���׷player
    public bool isWalkToOink = false, isLook = false, isWalkToBegin = false;
    public float LookTime = 0f;
    public float waringTime = 0f;//Ѱ��ʱ��
    public float stayTime = 3f;//����������λ��ʱ��
    private Vector3 beginrotation;//��ʼ��תֵ
    private Mesh mesh;//mesh
    public float distance_normal = 5, distance_waring = 7, distance;//����ǰ������Ϣֵ�ķ�Χ
    private Vector3[] vertices;//mesh��vertices
    public Vector3 v1;
    int[] tringles;//mesh��tringles
    Vector2[] _uvs;//mesh��uv
    Quaternion targetPoint;//ת��
    Animator ator;  //�������
    MeshFilter mf;
    MeshRenderer mr;
    Shader shader;
    public BoxCollider bx;//�Ȳ���ײ��
    public Vector3 oinkPostion;
    NavMeshAgent agent;
    bool iszhuizhu = true;
    //lxy
    public Material material;

    //�¼ӵ��ĸ�����
    //public Slider jingjie;
    //public Slider jingjiehong;
   
    bool isview;//�Ƿ�����Ұ��Χ��
    float wait_biaoqing = 0;//������ʾʱ�䣻
    public AudioSource music;
    public AudioClip walkVoice;
    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        music.spatialBlend = 1;
        walkVoice = Resources.Load<AudioClip>("Music/peopleWalk");
    }
    void Start()
    {
        biaoqing_1.SetActive(true);
        biaoqing_2.SetActive(false);
        //rectTransform_two.gameObject.SetActive(false);
        mesh = new Mesh();
        mf = transform.gameObject.AddComponent<MeshFilter>();
        mr = transform.gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "mesh";

        agent = GetComponent<NavMeshAgent>();

        begin = transform.position;
        beginrotation = transform.localEulerAngles;

        targetPoint = Quaternion.Euler(beginrotation);
        ator = transform.GetComponent<Animator>();
        isCatching = false;

        bx.enabled = false;
        oinkPostion = Vector3.zero;

        v1 = Vector3.zero;
        //�¼ӵ�
        //jingjie.gameObject.SetActive(false);
        //jingjiehong.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (isCatching == true)//�������׷��ͻ�����
        {
            wait_biaoqing += Time.deltaTime;
            if (wait_biaoqing < 3)
            {
                biaoqing_1.SetActive(false);
                biaoqing_2.SetActive(true);
            }
            else
            {
                biaoqing_1.SetActive(true);
                biaoqing_2.SetActive(false);
            }

        }
        IsWaring();
        DrawFieldOfView();
        Rotate();
        Attack();
        staytime();
        setSpeed();
        if (!ator.GetBool("isAttack") && !ator.GetBool("isRun"))
        {
            walktoOink();
            //rectTransform_one.gameObject.SetActive(false);
            //rectTransform_two.gameObject.SetActive(false);
            //nogo();
        }
        if (!isWalkToOink && !isLook && !ator.GetBool("isAttack") && !ator.GetBool("isRun"))
        {
            walkToNextPoint(NextPoint);
        }
        //if (GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Camera>().enabled)
        //    jingjiequan();
        //Debug.Log(transform.InverseTransformPoint(transform.position)+" "+transform.position);
        isWalk();
    }
    void isWalk()
    {
        AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("run"))
        {
            if (!music.isPlaying)
            {
                music.clip = walkVoice;
                music.Play();
                music.pitch = 1f;
                music.loop = true;
            }
        }
        else if (info.IsName("walk"))
        {
            if (!music.isPlaying)
            {
                music.clip = walkVoice;
                music.Play();
                music.pitch = 0.7f;
                music.loop = true;
            }
        }
        else
        {
            music.Stop();
        }
    }
    //void jingjiequan()//����ֵ��slider
    //{
    //    jingjie.value = waring / midleWaring;
    //    jingjiehong.value = waring / maxWaring;
    //    jingjiehong.gameObject.SetActive(false);
    //    Vector2 player2DPosition = Camera.main.WorldToScreenPoint(transform.position);
    //    if (waring <= midleWaring && waring > 0)
    //    {
    //        jingjie.gameObject.transform.position = player2DPosition + new Vector2(x_offset, y_offset);
    //        if (isview == false)//player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
    //        {
    //            //warn.gameObject.SetActive(false);
    //            jingjie.gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            //warn.gameObject.SetActive(true);
    //            jingjie.gameObject.SetActive(true);
    //        }
    //    }
    //    if (waring > midleWaring)
    //    {
    //        // warn.gameObject.SetActive(false);
    //        jingjie.gameObject.SetActive(false);
    //        jingjiehong.gameObject.transform.position = player2DPosition + new Vector2(x_offset, y_offset);

    //        if (isview == false)//player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
    //        {
    //            jingjiehong.gameObject.SetActive(false);
    //            //warn_hong.gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            jingjiehong.gameObject.SetActive(true);
    //            //warn_hong.gameObject.SetActive(true);
    //        }
    //    }
    //}
    private void OnBecameInvisible()
    {
        isview = false;
    }
    private void OnBecameVisible()
    {
        isview = true;
    }
    //void nogo()//����δ׷��ʱ��ʾ�Ի�
    //{
    //    if (GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Camera>().enabled)
    //    {
    //        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z));
    //        rectTransform.position = player2DPosition + new Vector2(xOffset, yOffset);
    //        if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
    //        {
    //            rectTransform.gameObject.SetActive(false);
    //        }
    //        else
    //        {

    //            if (rectTransform.gameObject.activeInHierarchy == false)
    //            {
    //                rectTransform.gameObject.SetActive(true);
    //            }
    //        }
    //    }  
    //}
    void staytime()//����Ƿ��Ѿ������������ص�
    {
        if (Vector3.Distance(transform.position, oinkPostion) < 0.1f && isOink)
        {
            if (stayTime > 0)
            {
                stayTime -= Time.deltaTime;
            }
            else if (stayTime <= 0)
            {
                stayTime = 0;
                isOink = false;
                PursuitTime = 0f;
            }
        }

    }
    void IsWaring()//�ж��Ƿ���뾯��״̬
    {
        if (ator.GetBool("isLook") || ator.GetBool("isRun"))
        {
            viewRugle = viewRugle_waring;//�л��ɾ���Ƕ�
            viewAngleStep = waring_step;//��������
            viewRadius = viewRadius_waring;//�������߾���
            distance = distance_waring;
        }
        else if (!ator.GetBool("isLook") && !ator.GetBool("isRun"))
        {
            viewRugle = viewRugle_normal;//�л��������Ƕ�
            viewAngleStep = normal_step;//������������
            viewRadius = viewRadius_normal;//�������߾���
            distance = distance_normal;
        }
    }
    void ontriggerOpen()//�����¼�
    {
        bx.enabled = true;
    }
    void ontriggerClose()//�����¼�
    {
        bx.enabled = false;
    }
    void setSpeed1()
    {
        ator.speed = 3f;
        Debug.Log("setspeed");
    }
    void setSpeed2()
    {
        ator.speed = 1f;
    }
    void Attack()//��������
    {
        float distance_attack = Vector3.Distance(goal.position, transform.position);
        attackSpeed.changeSpeed(ator);
        //Debug.Log(distance_attack);
        if (distance_attack <= 0.8f && PursuitTime >= 0)
        {
            Vector3 dir = goal.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.3f);
            ator.SetBool("isAttack", true);
        }
        AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
        // �ж϶����Ƿ񲥷����
        if ((info.normalizedTime > 0.92f) && (info.IsName("push")) && distance_attack > 1)
        {
            ator.SetBool("isAttack", false);
        }
        else if (info.IsName("push"))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goal.transform.localPosition, 2 * Time.deltaTime);
        }
    }
    void Rotate()//��������ʼ�ص���ָ�ԭ�����泯����
    {
        Vector3 v = transform.localEulerAngles - beginrotation;
        if (Mathf.Abs(transform.position.x - begin.x) <= 0.1f && Mathf.Abs(transform.position.y - begin.y) <= 0.1f && Mathf.Abs(transform.position.z - begin.z) <= 0.1f)  //��õ�����ΪһλС������ʵ�ʿ���Ϊ�ü�λС�����ԼӸ���Χ
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetPoint, 3 * Time.deltaTime);//ƽ��ת��
            ator.SetBool("isWalk", false);
        }
    }
    void setSpeed()//���ݶ������õ����ٶȣ�����Ǳ������ٶ�
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = ator.GetCurrentAnimatorStateInfo(0);
        if (animatorInfo.IsName("attack"))
        {
            agent.speed = 0.8f;
        }
        else if (animatorInfo.IsName("run"))
        {
            agent.speed = 1.5f;
        }
        else if (animatorInfo.IsName("walk"))
        {
            agent.speed = 1f;
        }
        else
        {
            agent.speed = 0f;
        }
    }
    void DrawFieldOfView()
    {

        //Debug.Log(transform.position);
        _uvs = new Vector2[(int)(viewAngleStep + 1)];
        vertices = new Vector3[(int)viewAngleStep + 1];
        tringles = new int[((int)viewAngleStep - 1) * 3];
        //vertices[0] = transform.InverseTransformPoint(transform.position);
        vertices[0] = Vector3.zero;
        _uvs[0] = new Vector2(1f, 1f);
        // ���������������ߵ������������ǰ�����Ƕ���-rugle
        Vector3 forward_left = Quaternion.Euler(0, -viewRugle, 0) * transform.forward * viewRadius;
        // ���δ���ÿһ������
        //Debug.Log(transform.forward);
        for (int i = 0; i < viewAngleStep; i++)
        {
            // ÿ�����߶���forward_left�Ļ�����ƫתһ�㣬���һ������ƫת2*rugle�ȵ��������Ҳ�
            Vector3 v = Quaternion.Euler(0, (viewRugle * 2 / viewAngleStep) * i, 0) * forward_left;
            float currAngle = Mathf.Deg2Rad * v.y;
            float x = Mathf.Cos(currAngle);
            float y = Mathf.Sin(currAngle);
            _uvs[i] = new Vector2(x * 0.5f + 0.5f, y * 0.5f + 0.5f);
            // ��������
            Ray ray = new Ray(transform.position, v);
            RaycastHit hitt = new RaycastHit();
            // ����ֻ�����ֲ���ײ��ע�����ֺ�����ӵ�layerһ�£����������
            int mask = LayerMask.GetMask("Obstacle", "Enemy", "OcclusionTran");
            Physics.Raycast(ray, out hitt, viewRadius, mask);

            // Playerλ�ü�v�����������յ�pos
            Vector3 pos = transform.position + v;
            if (hitt.transform != null)
            {
                // �����ײ��ʲô�����������յ�ͱ�Ϊ��ײ�ĵ���
                pos = hitt.point;
            }
            vertices[i + 1] = transform.InverseTransformPoint(pos);

            // �����λ�õ�pos���߶Σ�ֻ���ڱ༭���￴��
            Debug.DrawLine(transform.position, pos, Color.red);

            // ��������ײ�����ˣ���һ������
            if (hitt.transform != null && hitt.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //if (GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Camera>().enabled)
                //{
                //    rectTransform_two.gameObject.SetActive(false);
                //    rectTransform.gameObject.SetActive(false);
                //    Vector2 player2DPosition = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z));
                //    rectTransform_one.position = player2DPosition + new Vector2(xOffset, yOffset);
                //    if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
                //    {
                //        rectTransform_one.gameObject.SetActive(false);
                //    }
                //    else
                //    {
                //        if (rectTransform_one.gameObject.activeInHierarchy == false)
                //        {
                //            rectTransform_one.gameObject.SetActive(true);
                //            //Invoke("mm", 5.0f);
                //        }
                //    }
                //}
                //OnEnemySpotted(hitt.transform.gameObject);
                if (goal.transform.GetComponent<player>().isInvisible)
                {
                    if (waring < midleWaring)
                    {
                        waring += 4 * Time.deltaTime;
                    }
                    else if (waring >= midleWaring && waring < maxWaring)
                    {
                        waring += 8 * Time.deltaTime;
                    }
                    waringTime = 2;
                }
            }
            else if (hitt.transform != null && hitt.transform.GetComponent<hammer>())
            {
                if (Vector3.Distance(hitt.transform.position, hitt.transform.GetComponent<hammer>().tp) > 0.2f)
                {
                    //Debug.Log(Vector3.Distance(hitt.transform.position, hitt.transform.GetComponent<hammer>().tp));
                    transform.gameObject.GetComponent<walkto>().isWalkto = true;
                    transform.gameObject.GetComponent<walkto>().position = hitt.transform.position;
                }
            }
        }


        makeMesh();
        float distance_enemy = Vector3.Distance(goal.position, transform.position);
        //Debug.Log(vertices[30]);
        if (PursuitTime > 0)//���waitTime����0�����׷��
        {
            PursuitTime -= Time.deltaTime;
            runToPlayer();
        }
        else if (PursuitTime <= 0 && distance_enemy > 0.1)
        {
            walkToNextPoint(NextPoint);
            //rectTransform.gameObject.SetActive(false);
            //if (GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Camera>().enabled)
            //{
            //    Vector2 player2DPosition = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z));
            //    rectTransform_two.position = player2DPosition + new Vector2(xOffset, yOffset);
            //    if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
            //    {
            //        rectTransform_two.gameObject.SetActive(false);
            //    }
            //    else
            //    {
            //        if (rectTransform_two.gameObject.activeInHierarchy == false)
            //        {
            //            rectTransform_two.gameObject.SetActive(true);
            //            Invoke("wait_duihua", 2.0f);
            //        }
            //    }
            //}
        }
        getBreathe();
    }
    //void wait_duihua()
    //{
    //    rectTransform_two.gameObject.SetActive(false);
    //}
    void runToPlayer()//Ŀ������Ϊ���
    {
        agent.destination = goal.position;
        ator.SetBool("isRun", true);
        isCatching = true;
    }
    /*void walkToBegin()//��Ŀ������Ϊ�ʼ�����
    {
        agent.destination = begin;
        ator.SetBool("isRun", false);
        ator.SetBool("isWalk", true);
        //GameUI1._Instance.UpdateDia(3);
        //showDia(3);

    }*/
    void makeMesh()//����mesh
    {
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
        material = new Material(Shader.Find("Custom/changeColor"));
        mr.material = material;
        //mr.material.shader = shader;
        //mr.material.color = Color.red;
    }

    void getBreathe()
    {
        float distance_player = Vector3.Distance(goal.position, transform.position);
        if (distance_player <= distance)
        {
            //Debug.Log(distance);
            waring = maxWaring;
            waringTime = 2;
        }
        if (waring >= midleWaring && !isWalkToOink)
        {
            ator.SetBool("isLook", true);
        }
        else if (waring <= 0)
        {
            ator.SetBool("isLook", false);
        }
        if (waring >= maxWaring)
        {
            PursuitTime = 2f;
            ator.SetBool("isRun", true);
            ator.SetBool("isWalk", false);
            ator.SetBool("isLook", false);
        }
        if (waring > 0 && !goal.transform.GetComponent<player>().isMove)
        {
            if (waringTime > 0)
                waringTime -= Time.deltaTime;
            else if (waringTime <= 0 && waring >= 0)
            {
                waring -= 2 * Time.deltaTime;
                waringTime = 0;
            }
        }
    }
    void walktoOink()
    {
        //Debug.Log(Vector3.Distance(v1, transform.position));
        if (goal.transform.GetComponent<player>().isOink && Vector3.Distance(goal.position, transform.position) < 10f && !isWalkToOink)
        {
            v1 = goal.position;
            isWalkToOink = true;
        }
        if (isWalkToOink && !isLook)
        {
            agent.destination = v1;
            //Debug.Log(v1);
            ator.SetBool("isWalk", true);
            ator.SetBool("isLook", false);
        }
        if (Vector3.Distance(v1, transform.position) < 0.1f && isWalkToOink)
        {
            isLook = true;
            LookTime = 6f;
            isWalkToOink = false;
        }
        if (LookTime >= 0)
        {
            ator.SetBool("isWalk", false);
            ator.SetBool("isLook", true);
            LookTime -= Time.deltaTime;
        }
        if (LookTime <= 0 && isLook == true)
        {
            isLook = false;
            isWalkToBegin = true;
            ator.SetBool("isWalk", true);
            ator.SetBool("isLook", false);
            //Debug.Log(isLook + " " + isWalkToBegin);
        }
        if (isWalkToBegin && !isLook && !isWalkToOink)
        {
            Debug.Log(1);
            walkToNextPoint(NextPoint);
            if (Vector3.Distance(NextPoint.transform.position, transform.position) < 0.1f)
            {
                ator.SetBool("isWalk", false);
                ator.SetBool("isLook", false);
            }
        }
    }
    void walkToNextPoint(wayPoint NextPoint)
    {
        if(!transform.GetComponent<walkto>().isWalkto)
        {
            agent.destination = NextPoint.transform.position;
            ator.SetBool("isWalk", true);
        }
        
    }
}
