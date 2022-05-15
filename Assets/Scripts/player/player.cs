using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Rigidbody rd;
    float rushTime = 5.0f;          //可冲刺时间
    float waitTime = 1.0f;          //等待时间
    public Slider slider;
    public float speed = 0f;        //速度
    public float breath = 1.0f;     //气息值
    public float speed_a,speed_b,speed_c;           //记录初始速度的值
    public float direct_y;          //旋转角度
    public double sin_1, cos_1;     //相对于世界坐标的cos和sin
    public double noneInputTime = 5.0f;
    public bool isInvisible;
    public bool isMove;
    public bool isOink1,isOink;
    public GameObject scope;
    public float oinkTime = 0f;
    public AudioSource audioSource;
    Animator ator;
    Vector3 direct;
    Vector3 vup=new Vector3(0,0.2f,0);
    public float xOffset;
    public float yOffset;
    public RectTransform recTransform;
    public bool isComplete = false;

    public AudioClip walkVoice;
    public AudioClip oinkVoice;
    public AudioClip bushVoice;
    public AudioClip boxVoice;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f;
        walkVoice= Resources.Load<AudioClip>("Music/playerWalk");
        oinkVoice= Resources.Load<AudioClip>("Music/oink");
        bushVoice = Resources.Load<AudioClip>("Music/bush");
        boxVoice = Resources.Load<AudioClip>("Music/box");

    }
    void Start()
    {
        
        rd = gameObject.GetComponent<Rigidbody>();
        
        isInvisible = true;

        slider.value = 1;
        slider.gameObject.SetActive(false);
        speed_a = speed;
        speed_b = speed * 2;
        speed_c = speed / 2;
        direct = transform.localEulerAngles;
        if (direct.y > 180)
        {
            direct.y = direct.y - 360;
        }
        direct_y = direct.y;
        //Debug.Log("direct_y=" + direct_y);
        sin_1 = Mathf.Sin(Mathf.Deg2Rad * direct_y);
        cos_1 = Mathf.Cos(Mathf.Deg2Rad * direct_y);
        //Debug.Log("sin_1=" + sin_1 + " cos_1=" + cos_1);
        ator = transform.GetComponent<Animator>();
        Vector3 v11 = new Vector3(-1, 0, 0);

        isMove = false;
        isOink1 = false;
        isOink = false;
    }
    void Update()
    {
        if (GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Camera>().enabled)
            RushTime();//冲刺
        noneInput();//判断是否有输入
        attack();//攻击
        Oink();//发出吼叫
        OinkTime();//记录间隔时间
        ifisInvisible();
        //Debug.Log(blood);
        //Debug.Log("speed "+speed+"speed_a "+speed_a);

    }
    void OinkTime()
    {
        if(isOink1 && oinkTime>=0)
        {
            oinkTime -= Time.deltaTime;
        }
        if(oinkTime<0)
        {
            isOink1 = false;
            isOink = false;
        }
        if(oinkTime>=0)
        {
            oinkTime -= Time.deltaTime;
        }
    }
    void ifisInvisible()
    {
        if(!isInvisible && audioSource.clip != bushVoice)
        {
            audioSource.clip = bushVoice;
            audioSource.Play();
        }
        else if(isInvisible && audioSource.clip == bushVoice)
        {
            audioSource.Stop();
        }
        if(audioSource.clip==walkVoice)
        {
            if(ator.GetBool("isRun"))
            {
                audioSource.pitch = 0.8f;
            }
            else
            {
                audioSource.pitch = 0.7f;
            }
            audioSource.loop = true;
            audioSource.volume = 0.6f;
        }
        else if(audioSource.clip != walkVoice)
        {
            audioSource.loop = false;

        }
       
    }
    void Oink() //吼叫
    {
        //Debug.Log("is1" + isOink1 + " isoink" + isOink);
        if(GameObject.FindWithTag("sphere"))
        {
            GameObject sc = GameObject.FindWithTag("sphere");
            oink.follow(sc, transform.position);
        }
        
        if (Input.GetKeyDown(KeyCode.K) && isOink1 == false)//如果按下第一下k则进行
        {
            Destroy(GameObject.FindWithTag("sphere"));
            //Debug.Log(1);
            oink.creat(scope,transform.position);
            oinkTime = 3f;
            isOink1 = true;
            
        }
        else if (Input.GetKeyDown(KeyCode.K) && isOink1 == true )
        {
            Destroy(GameObject.FindWithTag("sphere"));
            //Debug.Log(2);
            oinkTime = 1f;
            isOink = true;
            isOink1 = false;
            audioSource.clip = oinkVoice;
            audioSource.Play();
        }
    }
    void FixedUpdate()
    {
            Move();
        
    }
    void attack()
    {
        
            if (Input.GetKeyDown(KeyCode.J))
            {
            //wDebug.Log(1);
            ator.SetBool("isAttack", true);

            }
        AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
        // 判断动画是否播放完成
           if ((info.normalizedTime > 0.72f) && (info.IsName("attack")))
           {
             ator.SetBool("isAttack", false);
           }

        
       
    }
    private void noneInput()//判断是否有输入， 如果输入间隔时间大于5s则进行特殊动作
    {
        if(!Input.anyKeyDown && noneInputTime>0 && !Input.anyKey)
        {
            noneInputTime -= Time.deltaTime;
        }
        else if(Input.anyKeyDown || Input.anyKey)
        {
            noneInputTime = 5.0f;
            ator.SetBool("isCrouch", false);
        }
        else if(noneInputTime<=0)
        {
            ator.SetBool("isCrouch", true);
        }
    }
    private void Move()//移动函数
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Debug.Log(h);
        Vector3 sped = new Vector3(h, 0, v);
        #region 相机方向偏移
        Vector3 originSped = sped;
        if (CameraMgr.Instance.m_MainCamera != null)
        {
            GameObject mainCamera = CameraMgr.Instance.m_MainCamera;
            Vector3 PlayerToCamera = transform.position - mainCamera.transform.position;
            PlayerToCamera = new Vector3(PlayerToCamera.x, 0, PlayerToCamera.z);
            float angle = Vector3.Angle(new Vector3(-1, 0, 0), PlayerToCamera);
            float dir = (Vector3.Dot (Vector3.up, Vector3.Cross (new Vector3(-1, 0, 0), PlayerToCamera)) < 0 ? -1 : 1);
            angle = angle * dir;
            Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0,1,0));
            sped = q * sped;
        }
        else
        {
            //Debug.LogError("[CameraLog]Main Camera Has't init");
        }
        #endregion
        
        if(sped != Vector3.zero)
        {
            isMove = true;
            AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
            // 判断动画是否播放完成
            if (!info.IsName("walk"))
            {
         
                if (!audioSource.isPlaying && audioSource.clip != oinkVoice)
                {
                    audioSource.clip = walkVoice;

                    audioSource.Play();
                }
            }
           
            
        }
        else
        {
            isMove = false;
            if(audioSource.isPlaying && audioSource.clip == walkVoice)
            {
                audioSource.Stop();
            }
        }
        sped = sped.normalized;
        Vector3 sped_x = sped;
        //Debug.Log("sped.x * sin_1=" + sped.z * cos_1 + " sped.x * sin_1= " + sped.x * sin_1);
        sped.x = (float)(sped_x.x * cos_1 + sped_x.z * sin_1);
        sped.z = (float)(sped_x.z * cos_1 - sped_x.x * sin_1);
        //sped.x = (float)(sped_x.x * cos_1 - sped_x.z * sin_1);
        //sped.z = (float)(sped_x.z * cos_1 + sped_x.x * sin_1);
        //Debug.DrawLine(transform.position, transform.forward+transform.position, Color.black);
        //Vector3 v1 = new Vector3(1, 0, 0);
        if(checkBox.Check(transform.position+vup, sped, 1f, 0.5f,speed_c))
        {
            speed = speed_c;
            AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
            // 判断动画是否播放完成
            if (info.IsName("walk"))
            {
                if(audioSource.clip!= boxVoice)
                {
                    //Debug.Log(33333);
                    audioSource.clip = boxVoice;
                    audioSource.Play();
                }
                    
            }
                
        }
        //Debug.Log(speed);
        if (sped != Vector3.zero && speed == speed_b)
        {
            ator.SetBool("isRun", true);
            ator.SetBool("isJog", false);
            ator.SetBool("isWalk", false);
        }
        else if (sped != Vector3.zero && speed == speed_c)
        {
            ator.SetBool("isRun", false);
            ator.SetBool("isJog", false);
            ator.SetBool("isWalk", true);
        }
        else if (sped != Vector3.zero && speed == speed_a)
        {
            ator.SetBool("isRun", false);
            ator.SetBool("isJog", true);
            ator.SetBool("isWalk", false);
        }
        else if (sped == Vector3.zero)
        {
            ator.SetBool("isRun", false);
            ator.SetBool("isJog", false);
            ator.SetBool("isWalk", false);
        }
        transform.Translate(sped * Time.deltaTime * speed, Space.World);
        transform.LookAt(transform.position + sped);
    }
    private void RushTime()//冲刺条函数
    {
        slider.value = rushTime / 5;
        
        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(transform.position);
       
        recTransform.position = player2DPosition + new Vector2(xOffset, yOffset);
        if (Input.GetKey(KeyCode.LeftShift) && rushTime > 0)//冲刺计时
        {
            if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
            {
                recTransform.gameObject.SetActive(false);
            }
            else
            {
                recTransform.gameObject.SetActive(true);
            }
            slider.gameObject.SetActive(true);
            waitTime = 2;
            rushTime -= Time.deltaTime;
            speed = speed_b;
        }
        else
        {
            speed = speed_a;
            if (waitTime > 0 && !Input.GetKey(KeyCode.LeftShift) && rushTime < 5)
            {
                waitTime -= Time.deltaTime;
            }
            else if (rushTime < 5 && waitTime <= 0)
            {
                rushTime += Time.deltaTime;
                if (rushTime >= 5)
                {
                    slider.gameObject.SetActive(false);
                    waitTime = 2.0f;
                    rushTime = 5;
                }
            }
        }
    }
}
