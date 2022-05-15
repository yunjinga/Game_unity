using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class child6_2 : MonoBehaviour
{
    Animator ator;

    public GameObject player;
    public GameObject duihua;
    public GameObject duihua_1;
    public Text text;
    public Text text1;
    public GameObject daye;
    //public RectTransform rectTransform;
    public bool istalk = false;
    public GameObject tishi;
    public CinemachineVirtualCamera cvam;
    public GameObject hand;
    float wait = 0;
    public int level;
    public GameObject duihua_3;
    public GameObject tr;
    //bool istalk = false;
    // Start is called before the first frame update
    void Start()
    {
        duihua_3.SetActive(false);
        duihua_1.SetActive(false);
        tishi.SetActive(false);
        duihua.SetActive(false);
        //baige = GameObject.Find("baige");
        if(level==1)cvam = GameObject.Find("CM vcam21").gameObject.GetComponent<CinemachineVirtualCamera>();
        if(level==2) cvam = GameObject.Find("CM vcam22").gameObject.GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        ator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            tishi.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) )
            {
                wait++;

            }
            switch (wait)
            {
                case 1:
                    {
                        duihua.SetActive(true);



                        cvam.Priority = 11;
                        if(level==1)text.text = "爷爷，爷爷，猪猪那么可爱为什么要吃猪猪啊";
                        //gudin();
                        if(level==2) text.text = "如果还有时间，我好希望把我所在的村子建造得更完整一些啊";
                        //istalk = true;
                        ator.SetBool("isTalk", true);
                        break;
                    }
                case 2:
                    {
                        duihua_1.SetActive(true);
                       
                        if (level == 1) text1.text = "因为物竞天择，适者生存阿巴阿巴阿.......";
                        //gudin();
                        if (level == 2) text1.text = "要是人生在世的时间可以更长一些就好了";
                        //istalk = true;
                        ator.SetBool("isTalk", true);
                        break;
                    }
                case 3:
                    {
                        duihua_1.SetActive(false);
                        text.text = "听起来好深奥的样子";
                        if (level == 1) text.text = "听起来好深奥的样子";
                        //gudin();
                        if (level == 2) text.text = "听起来好深奥的样子";



                        break;
                    }
                case 4:
                    {
                        duihua.SetActive(false);
                        cvam.Priority = 5;
                        istalk = false;
                        //tishi.SetActive(false);
                        ator.SetBool("isTalk", false);
                        break;
                    }
                default:
                    {
                        Vector3 screenPos = Camera.main.WorldToScreenPoint(hand.transform.position);
                        Vector3 screenPos1 = Camera.main.WorldToScreenPoint(daye.transform.position);
                        screenPos1.x += 100;
                        screenPos.x += 80;
                        screenPos.y += 80;
                        if (wait > 4)
                        {
                            duihua.transform.position = screenPos;
                            duihua_1.transform.position = screenPos1;

                        }
                        
                        if (wait % 3 == 2&&wait>4&&level==1)
                        {
                            duihua.SetActive(true);
                            text.text = "爷爷你看好可爱的小猪";
                            
                        }
                        if (wait % 3 == 0 && wait > 4&&level==1)
                        {
                            
                            duihua_1.SetActive(true);
                            text1.text = "是我年纪大了眼花了么阿巴阿巴阿.......";
                            duihua.SetActive(false);
                            
                            //tishi.SetActive(false);
                        }
                        if (wait % 3 == 1 && wait > 4&&level==1)
                        {
                            duihua_1.SetActive(false);
                            //tishi.SetActive(false);
                            istalk = false;
                        }
                        if (wait % 2 == 1&& wait > 4 && level == 2&&tr.GetComponent<level6_2>().level6==true)
                        {
                            duihua_3.SetActive(true);
                            

                        }
                        if (wait % 2 == 0 && wait > 4 && level == 2 && tr.GetComponent<level6_2>().level6 == true)
                        {
                            duihua_3.SetActive(false);
                            //tishi.SetActive(false);
                            istalk = false;
                        }
                            break;
                    }
            }
        }
        else if(Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            tishi.SetActive(false);
        }
    }
    
}
