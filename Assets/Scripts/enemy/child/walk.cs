using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class walk : MonoBehaviour
{
    Animator ator;
    NavMeshAgent agent;
    public ChildPoint NextPoint;
    // Start is called before the first frame update
    void Start()
    {
        ator = transform.gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
        if (ator.GetBool("isTalk"))
        {
            agent.speed = 0;
        }
        else if(!ator.GetBool("isTalk") && !info.IsName("run"))
        {
            agent.speed = 1;
        }
        else if (info.IsName("run"))
        {
            agent.speed = 1.5f;
        }
    }
}
