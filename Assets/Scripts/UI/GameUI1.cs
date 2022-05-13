using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI1 : MonoBehaviour
{
    
    bool isend=false;//对话是否播放完成
    //public GameObject Anim;
    //public GameObject Anim_fly;
    public GameObject Anim_start;//开始的动画
    //public GameObject duihua_niao_1;
    //public GameObject duihua_niao_2;
    //public GameObject duihua_niao_3;
    public CinemachineVirtualCamera cineVirtual;//相机
    public GameObject player;//玩家
    public GameObject button_stop;//暂停按钮
    public GameObject button_jixv;//主界面的继续按钮
    public GameObject button_qvxiao;//暂停界面的继续按钮
    public GameObject image_wupin;//第一个物品栏
    public GameObject wupin_two;//第二个物品栏
   
    public GameObject shibai;//失败界面
    public GameObject image_zantin;//暂停界面
    
   // public GameObject zhuangtai;
    //public GameObject duihua_three;
    private bool count;
   
    public static bool eat = false;//是否吃到物品
    public List<GameObject> m_Hps;
    public List<GameObject> wupin;
   // public List<GameObject> duihua;
    //public GameObject tishi;
    public GameObject shengli;//胜利界面
    public static GameUI1 _instance = new GameUI1();
    public static GameUI1 _Instance => _instance;
    private int cishu;//按下空格键的次数
    //public float speed = 40.0f;
    public GameObject gaming;
    float wait=0;
    public bool isplay=false;
    public GameObject[] canvas_enemy;
    public bool ishei = false;
    public bool iswhite = false;
    public Image black;
    float A = 255;
    public AudioSource source;
    public AudioClip clip;
    int num = 0;
    ///public AudioSource audio_jixv;
    void Start()
    {
        source.Stop();
        PlayerPrefs.SetInt("isMoveCamera", 0);
        PlayerPrefs.SetInt("level", 0);
        //Camera.main.GetComponent<screenChange>().SetBlackBool = true;
        //zhuangtai.SetActive(false);
        Time.timeScale = 1;
        canvas_enemy = GameObject.FindGameObjectsWithTag("UI");
        button_stop.GetComponent<Button>().onClick.AddListener(Onclick1);
        button_jixv.SetActive(false);
        button_jixv.GetComponent<Button>().onClick.AddListener(OnClick2);
        source = GetComponent<AudioSource>();
        //button_kaishi.GetComponent<Button>().onClick.AddListener(start1);
        //image_wupin.SetActive(false);
        //wupin_two.SetActive(false);
        //image_zantin.SetActive(false);
        shibai.SetActive(false);
        //image_kaishi.SetActive(true);
        //tishi.SetActive(false);
        shengli.SetActive(false);
        //duihua_niao_1.SetActive(false);
        //duihua_niao_2.SetActive(false);
        //duihua_niao_3.SetActive(false);
        //Anim.SetActive(false);
        //Anim_fly.SetActive(false);
        //float step = speed * Time.deltaTime;
        gaming.SetActive(true);

    }


    // Update is called once per frame
    void Update()
    {
        A -= Time.deltaTime * 150;
        black.GetComponent<Image>().color = new Color(0, 0, 0, A);
        if (A == 0)
        {
            black.enabled = false;
        }
        /*if (zhongdian.end)
        {
            shengli.SetActive(true);
            Time.timeScale = 0;
            zhongdian.end = false;
        }*/
        if (player.transform.gameObject.GetComponent<player_blood>().blood == 0)
        {
            num++;
            shibai.SetActive(true);
            image_zantin.SetActive(false);
            gaming.SetActive(false);
            if(num==1)source.PlayOneShot(clip);
            Time.timeScale = 0;
            for (int i = 0; i < canvas_enemy.Length; i++)
            {
                canvas_enemy[i].SetActive(false);
            }
        }
        if (isplay == true)
        {
            wait += Time.deltaTime;
            //Debug.Log(wait);
        }
        //if (!ishei)
        //{
        //    Camera.main.GetComponent<screenChange>().SetBlackBool = true;
        //    ishei = !ishei;
        //}
        //if (wait > 2)
        //{
        //    if (!iswhite)
        //    {
        //        Camera.main.GetComponent<screenChange>().SetWhiteBool = true;
        //        iswhite = !iswhite;
        //    }
        //}
        if (wait >= 4)
        {
            Anim_start.SetActive(false);
            gaming.SetActive(true);
        }
    }
    private void m1()
    {
        image_wupin.SetActive(false);
    }
    private void zhiyin()
    {
        //tishi.SetActive(false);
    }
    /*public void UpdateHPShow(int CurHp)
    {
        if (m_Hps.Count < CurHp)
        {
            Debug.LogError(message: "血量不正确");
            return;
        }
        ClearHpShow();
        for (int i = 0; i < CurHp; i++)
        {
            m_Hps[i].SetActive(true);
        }
        if (CurHp == 0)
        {
            shibai.SetActive(true);
            Time.timeScale = 0;
        }
    }*/
    /*public void UpdateDia(int CurWp)
    {
        if (duihua.Count < CurWp)
        {
            return;
        }
        ClearDia();
        duihua[CurWp - 1].SetActive(true);
    }*/
    public void UpdateWPShow(int CurWP)
    {
        if (wupin.Count < CurWP)
        {
            Debug.LogError(message: "数量不正确");
            return;
        }
        ClearWPShow();
        for(int i = 0; i < CurWP; i++)
        {
            wupin[i].SetActive(true);
        }
    }
    public void ClearWPShow()
    {
        if (wupin == null)
        {
            return;
        }
        foreach(var item in wupin)
        {
            item.SetActive(false);
        }
    }
    /*public void ClearDia()
    {
        if (duihua == null)
        {
            return;
        }
        foreach(var item in duihua)
        {
            item.SetActive(false);
        }
    }*/
    /*public void ClearHpShow()
    {
        if (m_Hps == null)
        {
            return;
        }
        foreach (var item in m_Hps)
        {
            item.SetActive(false);
        }
    }*/
    public void Onclick1()//暂停按钮
    {
        button_stop.SetActive(false);
        button_jixv.SetActive(true);
        Time.timeScale = 0;
        image_zantin.SetActive(true);
        //button_qvxiao.SetActive(true);
    }
    public void OnClick2()//继续图标按钮
    {
        button_stop.SetActive(true);
        button_jixv.SetActive(false);
        image_zantin.SetActive(false);
        Time.timeScale = 1;
        
    }
    public void OnClick4()//继续按钮
    {
        button_stop.SetActive(true);
        button_jixv.SetActive(false);
        image_zantin.SetActive(false);
        Time.timeScale = 1;

    }
    /* public void OnClick3()//图标上的继续键
     {
         image_zantin.SetActive(false);
     }*/
    public void restart()//重新开始
    {
        SceneManager.LoadScene(0);
        shibai.SetActive(false);
        Time.timeScale = 1;
        isplay = true;
    }
    public void restart_end()//结束时返回主菜单
    {
        SceneManager.LoadScene(0);
        shengli.SetActive(false);
        shibai.SetActive(false);
        Time.timeScale = 1;
    }
    //public void start1()//开始按键的功能
    //{
    //    image_kaishi.SetActive(false);
        
    //   /* if (tishi.activeInHierarchy == false)
    //    {
    //        tishi.SetActive(true);
    //         Invoke("zhiyin", 3.0f);
    //    }
    //    duihua_niao_1.SetActive(true);
    //    duihua_niao_2.SetActive(false);
    //    duihua_niao_3.SetActive(false);
    //    //Anim.SetActive(true);*/
      
    //    Anim_start.SetActive(true);
    //    Time.timeScale = 1;
    //    isplay =true;
    //    //Invoke("yincangshow", 4);
    //}
   
    public void Quit()
    {
       
           
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
           
#else
         Application.Quit();
#endif
     }
}

