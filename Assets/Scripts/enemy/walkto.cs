using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class walkto : MonoBehaviour
{
    Animator ator;
    NavMeshAgent agent;
    public Vector3 position;
    public bool isWalkto = false;
    public bool isHammer = false;
    public Object game = null;
    public GameObject hammer;//敌人头上锤子图标
    //public GameObject hammer_1;//锤子图标
    public Transform Hand;//敌人
    //public Transform follow;//锤子
    private void Start()
    {
        game = Resources.Load("polySurface186", typeof(GameObject));
        ator = transform.gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        position = transform.position;
    }
    public void Update()
    {
        if(transform.GetComponent<enemy>())
        {
            if (Vector3.Distance(transform.position, position) > 0.1f && isWalkto && transform.GetComponent<enemy>().waring == 0)
            {

                hammer.SetActive(true);
                //hammer_1.SetActive(true);
                walk();
            }
            else if (Vector3.Distance(transform.position, position) < 0.1f && isWalkto)
            {
                //hammer.SetActive(false);
                ator.SetBool("isWalk", false);
                isWalkto = false;
                //isWalkto = false;
                if (isHammer)
                {
                    //hammer_1.SetActive(false);
                    GameObject polySurface186 = GameObject.Instantiate(game, position, Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;
                    polySurface186.GetComponent<hammer>().tp = position;
                    isHammer = false;
                    hammer.SetActive(false);
                }

            }
        }
        else if(transform.GetComponent<enemy1>())
        {
            if (Vector3.Distance(transform.position, position) > 0.1f && isWalkto && transform.GetComponent<enemy1>().waring == 0)
            {

                hammer.SetActive(true);
                //hammer_1.SetActive(true);
                walk();
            }
            else if (Vector3.Distance(transform.position, position) < 0.1f && isWalkto)
            {
                //hammer.SetActive(false);
                ator.SetBool("isWalk", false);
                isWalkto = false;
                //isWalkto = false;
                if (isHammer)
                {
                    //hammer_1.SetActive(false);
                    GameObject polySurface186 = GameObject.Instantiate(game, position, Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;
                    polySurface186.GetComponent<hammer>().tp = position;
                    isHammer = false;
                    hammer.SetActive(false);
                }

            }
        }
        else if(transform.GetComponent<enemy2>())
        {
            if (Vector3.Distance(transform.position, position) > 0.1f && isWalkto && transform.GetComponent<enemy2>().waring == 0)
            {

                hammer.SetActive(true);
                //hammer_1.SetActive(true);
                walk();
            }
            else if (Vector3.Distance(transform.position, position) < 0.1f && isWalkto)
            {
                //hammer.SetActive(false);
                ator.SetBool("isWalk", false);
                isWalkto = false;
                //isWalkto = false;
                if (isHammer)
                {
                    //hammer_1.SetActive(false);
                    GameObject polySurface186 = GameObject.Instantiate(game, position, Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;
                    polySurface186.GetComponent<hammer>().tp = position;
                    isHammer = false;
                    hammer.SetActive(false);
                }

            }
        }
        
    }
    public void walk()
    {
        ator.SetBool("isWalk", true);
       
        agent.destination = position;
        
    }
    private void OnBecameInvisible()
    {
        hammer.SetActive(false);
        //hammer_1.SetActive(false);
    }
    private void OnBecameVisible()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(Hand.transform.position);
        hammer.transform.position = screenPos;
        //Vector3 screenPos1 = Camera.main.WorldToScreenPoint(follow.transform.position);
        //hammer_1.transform.position = screenPos1;
    }
}
