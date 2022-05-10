using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cundang : MonoBehaviour
{
    public GameObject cundang_1;
    public GameObject cundang_2;
    public GameObject cundang_3;
    //public GameObject cundang_4;
    public List<GameObject> cundagdian;
    private Vector3 offset;
    public GameObject player;
    private Transform playerTrans;
    public CinemachineVirtualCamera cineVirtual;
    public GameObject kshi;
    public GameObject gaming;
    bool isplay=false;
    float wait;
    public GameObject Anim_start;//开始的动画
    // Start is called before the first frame update
    void Start()
    {
        cundagdian.Add(cundang_1);
        cundagdian.Add(cundang_2);
        cundagdian.Add(cundang_3);
        //cundagdian.Add(cundang_4);
        playerTrans = player.transform;
        offset = transform.position - playerTrans.position;
        //offset = new Vector3(0, offset.y, offset.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isplay == true)
        {
            wait += Time.deltaTime;
        }
        if (wait >= 6)
        {
            Anim_start.SetActive(false);
            gaming.SetActive(true);
        }
    }
    public void cun()
    {
        int i = PlayerPrefs.GetInt("guanqia");
        player.transform.position = cundagdian[i].transform.position;
        cineVirtual.transform.position = player.transform.position + offset;
        kshi.SetActive(false);
        Time.timeScale = 1;
        cineVirtual.m_Lens.FieldOfView = 50;
        isplay = true;
        Anim_start.SetActive(true);
;    }
}
