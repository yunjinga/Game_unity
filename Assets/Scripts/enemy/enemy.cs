using System;
using System.Collections;
using System.Collections.Generic;
using FIMSpace.FLook;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    
    // Start is called before the first frame update
    public float xOffset;
    public float yOffset;
    public GameObject biaoqing_1;
    public GameObject biaoqing_2;
    //新加的x,y
    float x_offset=0;
    float y_offset=300;
    //public RectTransform rectTransform;
    //public RectTransform rectTransform_one;
    //public RectTransform rectTransform_two;
    public Transform goal;
    public float viewRadius = 3f, viewRadius_normal=3f, viewRadius_waring=5f;// 代表视野最远的距离和警戒后的距离
    public float viewRugle = 45, viewRugle_normal = 45, viewRugle_waring = 60;//射线角度
    public float viewAngleStep = 30f, normal_step = 30f, waring_step=45f; //射线数量和警戒后的射线数量
    public float PursuitTime = 0f; //追击时间
    public Vector3 begin;//开始时的位置

    //public float waring = 0f;//警戒值
    //lxy
    public float waring = 0f;
    //lxy

    //public float maxWaring = 40f, midleWaring = 20f;//最大警戒值 和中间警戒值
    //lxy
    public float maxWaring = 40f, midleWaring = 20f;
    //lxy

    public bool isCatching,isOink=false;//是否在追player
    public bool isWalkToOink = false,isLook=false,isWalkToBegin=false;
    public float LookTime = 0f;
    public float waringTime = 0f;//寻敌时间
    public float stayTime = 3f;//待在声音点位置时间
    private Vector3 beginrotation;//初始旋转值
    private Mesh mesh;//mesh
    public float distance_normal=0.2f,distance_waring=0.4f,distance;//警戒前后获得气息值的范围
    private Vector3[] vertices;//mesh的vertices
    public Vector3 v1;
    int[] tringles;//mesh的tringles
    Vector2[] _uvs;//mesh的uv
    Quaternion targetPoint;//转向
    Animator ator;  //动画组件
    MeshFilter mf;
    MeshRenderer mr;
    Shader shader;
    private FLookAnimator m_LookAnimator;
    public BoxCollider bx;
    public Vector3 oinkPostion;
    NavMeshAgent agent;
    bool iszhuizhu=true;
    //新加的四个变量
    //public Slider jingjie;
    //public Slider jingjiehong;
    //public RectTransform warn;
    //public RectTransform warn_hong;
    bool isview;//是否在视野范围内
    float wait_biaoqing = 0;//表情显示时间；
    public GameObject Trigger;
    public GameObject Trigger_1;
    #region 环绕注视
    private float m_CurTimeCount;
    private float m_doubleLoopTime;
    private Transform m_LookAtTarget;
    
    [Header("环绕注视", order = 1)]
    public float m_MinAngle;
    
    [Tooltip("注视最大角度")]
    public float m_MaxAngle;
    
    [Range(1f, 30f)]
    [Tooltip("[注视]注视环绕一周的时间秒")]
    public float m_LoopTime;  //循环一周时间
    
    [Range(0f, 5f)]
    [Tooltip("[注视]注视高度，相对人物坐标")]
    public float m_Hight;
    
    [Range(0f, 5f)]
    [Tooltip("[注视]注视点距离人物距离")]
    public float m_Distance;
    

    #endregion
    
    
    //lxy
    public Material material;

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
        //warn.gameObject.SetActive(false);
        //jingjie.gameObject.SetActive(false);
        //jingjiehong.gameObject.SetActive(false);
        //warn_hong.gameObject.SetActive(false);
        m_CurTimeCount = 0;
        mesh = new Mesh();
        mf = transform.gameObject.AddComponent<MeshFilter>();
        mr = transform.gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "mesh";
        //LookAtInit();
        
        agent = GetComponent<NavMeshAgent>();

        begin = transform.position;
        beginrotation = transform.localEulerAngles;

        targetPoint = Quaternion.Euler(beginrotation);
        ator = transform.GetComponent<Animator>();
        isCatching = false;

        bx.enabled = false;
        oinkPostion = Vector3.zero;
        
        v1 = Vector3.zero;
        //新加的初始显示绿到黄
        //if (waring <= midleWaring)
        //{
        //    jingjie.gameObject.SetActive(true);
        //    jingjiehong.gameObject.SetActive(false);
        //}
        Trigger = GameObject.Find("Start2").gameObject;
        Trigger_1 = GameObject.Find("Start3").gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.destination);
        IsWaring();
        DrawFieldOfView();
        Rotate();
        Attack();
        staytime();
        setSpeed();
        if(!ator.GetBool("isAttack") && !ator.GetBool("isRun"))
        {
            walktoOink();
            //rectTransform_one.gameObject.SetActive(false);
            //rectTransform_two.gameObject.SetActive(false);
            //nogo();
        }
        //新加的
        //if (GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Camera>().enabled)
        //    jingjiequan();
        if (isCatching == true)//如果正在追逐就换表情
        {
            Trigger.SetActive(false);
            Trigger_1.SetActive(false);
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
        //Debug.Log(transform.InverseTransformPoint(transform.position)+" "+transform.position);
        isWalk();
    }
    void isWalk()
    {
        AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("run"))
        {
            if (!music.isPlaying)
            {
                music.clip = walkVoice;
                music.Play();
                music.pitch = 1f;
                music.loop = true;
            }
        }
        else if(info.IsName("walk"))
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
    //新加的警戒值的slider
    //void jingjiequan()//警戒值的slider
    //{
    //    jingjie.value = waring / midleWaring;
    //    if (jingjie.value == 0)
    //    {
    //        jingjie.gameObject.SetActive(false);
    //        jingjiehong.gameObject.SetActive(false);
    //    }
    //    if (jingjiehong.value == 0)
    //    {
    //        jingjiehong.gameObject.SetActive(false);
    //    }
    //        jingjiehong.value = waring / maxWaring;
    //    jingjiehong.gameObject.SetActive(false);
    //    Vector2 player2DPosition = Camera.main.WorldToScreenPoint(transform.position);
    //    if (waring <= midleWaring&&waring>0)
    //    {
    //        jingjie.gameObject.transform.position = player2DPosition + new Vector2(x_offset, y_offset);
    //        if (isview==false)//player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
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
    //       // warn.gameObject.SetActive(false);
    //        jingjie.gameObject.SetActive(false);
    //        jingjiehong.gameObject.transform.position = player2DPosition + new Vector2(x_offset, y_offset);
            
    //        if (isview==false)//player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
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


    private void LateUpdate()
    {
        //LooKTargetAutogiRation();
    }

    
    void staytime()//检测是否已经到达了声音地点
    {
        if(Vector3.Distance(transform.position,oinkPostion)<0.1f && isOink)
        {
            if (stayTime > 0)
            {
                stayTime -= Time.deltaTime;
            }
            else if(stayTime<=0)
            {
                stayTime = 0;
                isOink = false;
                PursuitTime = 0f;
            }
        }
        
    }
    void IsWaring()//判断是否进入警戒状态
    {
        if(ator.GetBool("isLook")||ator.GetBool("isRun"))
        {
            viewRugle = viewRugle_waring;//切换成警戒角度
            viewAngleStep = waring_step;//射线数量
            viewRadius = viewRadius_waring;//警戒射线距离
            distance = distance_waring;
        }
        else if(!ator.GetBool("isLook")&&!ator.GetBool("isRun"))
        {
            viewRugle = viewRugle_normal;//切换成正常角度
            viewAngleStep = normal_step;//正常射线数量
            viewRadius = viewRadius_normal;//正常射线距离
            distance = distance_normal;
        }
    }
    void ontriggerOpen()//动画事件
    {
        bx.enabled = true;
    }
    void ontriggerClose()//动画事件
    {
        bx.enabled = false;
    }
   
    void Attack()//攻击函数
    {
        float distance_attack = Vector3.Distance(goal.position, transform.position);
        attackSpeed.changeSpeed(ator);
        //Debug.Log(distance_attack);
        if (distance_attack<=0.8f && PursuitTime>=0)
        {
            Vector3 dir = goal.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.3f);
            ator.SetBool("isAttack", true);
        }
        AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
        // 判断动画是否播放完成
        if ((info.normalizedTime > 0.92f) && (info.IsName("push"))&& distance_attack>1)
        {
            ator.SetBool("isAttack", false);
        }
        else if(info.IsName("push") && !transform.GetComponent<cunzhishu>())
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goal.transform.localPosition, 2 * Time.deltaTime);
        }
    }
    void Rotate() //如果到达初始地点则恢复原本的面朝方向
    {
        if (Vector3.Distance(transform.position,begin)<0.1f && Vector3.Distance(transform.localEulerAngles, beginrotation) >0.1f && !transform.GetComponent<walkto>().isWalkto)  //获得的数据为一位小数，但实际可能为好几位小数所以加个范围
        {
            //Debug.Log(1111);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetPoint, 3 * Time.deltaTime);//平滑转向
            ator.SetBool("isWalk", false);
        }
    }
    void setSpeed()//根据动画设置导航速度，如果是奔跑则速度
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo=ator.GetCurrentAnimatorStateInfo(0);
        if (animatorInfo.IsName("attack"))
        {
            agent.speed = 0.8f;
        }
        else if(animatorInfo.IsName("run"))
        {
            agent.speed = 1.5f;
        }
        else if(animatorInfo.IsName("walk"))
        {
            agent.speed = 1f;
        }else
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
        // 获得最左边那条射线的向量，相对正前方，角度是-rugle
        Vector3 forward_left = Quaternion.Euler(0, -viewRugle, 0) * transform.forward * viewRadius;
        // 依次处理每一条射线
        //Debug.Log(transform.forward);
        for (int i = 0; i < viewAngleStep; i++)
        {
            // 每条射线都在forward_left的基础上偏转一点，最后一个正好偏转2*rugle度到视线最右侧
            Vector3 v = Quaternion.Euler(0, (viewRugle*2 / viewAngleStep) * i, 0) * forward_left;
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
            if (hitt.transform != null)
            {
                // 如果碰撞到什么东西，射线终点就变为碰撞的点了
                pos = hitt.point;
            }
            vertices[i + 1] = transform.InverseTransformPoint(pos);
            
            // 从玩家位置到pos画线段，只会在编辑器里看到
            Debug.DrawLine(transform.position, pos, Color.red);

            // 如果真的碰撞到敌人，进一步处理
            if (hitt.transform != null && hitt.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //rectTransform.gameObject.SetActive(false);
                //rectTransform_two.gameObject.SetActive(false);
                //if (GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Camera>().enabled)
                //{
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
        
        //void wait_duihua()
        //{
        //    rectTransform_two.gameObject.SetActive(false);
        //}
        makeMesh();
        float distance_enemy = Vector3.Distance(goal.position, transform.position);
        //Debug.Log(vertices[30]);
        if (PursuitTime > 0)//如果waitTime大于0则进行追击
        {
            PursuitTime -= Time.deltaTime;
            runToPlayer();
        }
        else if(PursuitTime<=0 && distance_enemy>0.1)
        {
            walkToBegin();
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
    void runToPlayer()//目标设置为玩家
    {
        agent.destination = goal.position;
        ator.SetBool("isRun", true);
        isCatching = true;
    }
    void walkToBegin()//将目标设置为最开始的起点
    {
        if(Vector3.Distance(transform.position,begin) > 0.3f && !transform.GetComponent<walkto>().isWalkto)
        {
            
            agent.destination = begin;
            ator.SetBool("isRun", false);
            ator.SetBool("isWalk", true);
        }
        
        //GameUI1._Instance.UpdateDia(3);
        //showDia(3);
       
    }
    void makeMesh()//生成mesh
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
        mr.material = material ;
        //mr.material.shader = shader;
        //mr.material.color = Color.red;
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
        if(goal.transform.GetComponent<player>().isOink && Vector3.Distance(goal.position, transform.position)<10f && !isWalkToOink)
        {
            v1 = goal.position;
            isWalkToOink = true;
        }
        if(isWalkToOink && !isLook)
        {
            agent.destination = v1;
            //Debug.Log(v1);
            ator.SetBool("isWalk", true);
            ator.SetBool("isLook", false);
        }
        if(Vector3.Distance(v1,transform.position)<0.1f && isWalkToOink)
        {
            isLook = true;
            LookTime = 6f;
            isWalkToOink = false;
        }
        if (LookTime >= 0)
        {
            ator.SetBool("isWalk",false);
            ator.SetBool("isLook", true);
            LookTime -= Time.deltaTime;
        }
        if(LookTime<=0 && isLook==true)
        {
            isLook = false;
            isWalkToBegin = true;
            ator.SetBool("isWalk", true);
            ator.SetBool("isLook", false);
            //Debug.Log(isLook + " " + isWalkToBegin);
        }
        if(isWalkToBegin && !isLook && !isWalkToOink)
        {
            walkToBegin();
            if(Vector3.Distance(begin,transform.position)<0.1f)
            {
                ator.SetBool("isWalk", false);
                ator.SetBool("isLook", false);
            }
        }
    }

    /*
    #region 环绕注视

    private void LookAtInit()
    {
        m_LookAnimator = transform.GetComponent<FLookAnimator>();
        if (m_LookAnimator == null)
        {
            Debug.LogError("[LookAtLog]can't find lookAt Component");
            m_LookAnimator = gameObject.AddComponent<FLookAnimator>();
        }

        Transform lookAtTarget = transform.Find("LookAtPosition");
        m_doubleLoopTime = 2 * m_LoopTime;
        SetLookAtTarget(lookAtTarget);
    }

    private void SetLookAtTarget(Transform Target)
    {
        if (m_LookAnimator == null)
            return;
        m_LookAnimator.ObjectToFollow = Target;
        m_LookAtTarget = Target;
    }

    private void RestoreLookAtTarget()
    {
        Transform lookAtTarget = transform.Find("LookAtPosition");
        SetLookAtTarget(lookAtTarget);
    }

    private void LooKTargetAutogiRation()
    {
        m_CurTimeCount += Time.deltaTime;
        if (m_CurTimeCount > m_doubleLoopTime)
            m_CurTimeCount -= m_doubleLoopTime;
        float timeUsed = m_CurTimeCount > m_LoopTime ? m_doubleLoopTime - m_CurTimeCount : m_CurTimeCount;
        float timeProportion = timeUsed / m_LoopTime;
        float curAngle = Mathf.Lerp(m_MinAngle, m_MaxAngle, timeProportion);
        Quaternion q = Quaternion.AngleAxis(curAngle, Vector3.up);
        Vector3 targetForward = transform.forward;
        targetForward = q * targetForward.normalized;
        Vector3 curPosition = transform.position + new Vector3(0, m_Hight, 0) + targetForward * m_Distance;
        //m_LookAtTarget.position = curPosition;
        //Debug.Log($"当前坐标{curPosition}");
    }

    public void SetLookAtActive(bool IfOpen)
    {
        m_LookAnimator.enabled = IfOpen;
    }
    
    

    #endregion
    void drawWireArc()
    {

    }
    */
}
