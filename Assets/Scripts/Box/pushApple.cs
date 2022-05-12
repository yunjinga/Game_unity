using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushApple : MonoBehaviour
{
    public float radius = 0.5F;
    public float power = 0.1F;
    bool first = false;
    int num = 0;
    Rigidbody[] rd;

    public AudioSource music;
    public AudioClip appleVoice;

    public void Awake()
    {
        findChild(transform.parent.gameObject);

        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        appleVoice = Resources.Load<AudioClip>("Music/apple");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.GetComponent<player>())
        {

            Animator ator = other.GetComponent<Animator>();
            AnimatorStateInfo info = ator.GetCurrentAnimatorStateInfo(0);
            // 判断动画是否播放完成
            if (info.IsName("attack"))
            {
                if (!first)
                {
                    findChild1(transform.parent.gameObject);
                    first = true;
                }
                addForce(other.transform.position + other.transform.forward);

            }
        }
    }
    void findChild(GameObject child)
    {
        for (int c = 0; c < child.transform.childCount; c++)
        {
            if (child.transform.GetChild(c).gameObject.GetComponent<Rigidbody>())
            {
                DestroyImmediate(child.transform.GetChild(c).gameObject.GetComponent<Rigidbody>());
            }
        }
    }
    void findChild1(GameObject child)
    {
        for (int c = 0; c < child.transform.childCount; c++)
        {
            if (child.transform.GetChild(c).gameObject.GetComponent<MeshCollider>() &&
                !child.transform.GetChild(c).gameObject.GetComponent<Rigidbody>())
            {
                child.transform.GetChild(c).gameObject.AddComponent<Rigidbody>();
                num++;
            }
        }
        int num1 = 0;
        rd = new Rigidbody[num];
        for (int c = 0; c < child.transform.childCount; c++)
        {
            if (child.transform.GetChild(c).gameObject.GetComponent<Rigidbody>())
            {
                rd[num1] = child.transform.GetChild(c).gameObject.GetComponent<Rigidbody>();
                //Debug.Log(num1+" "+rd[num1]);
                num1++;
            }
        }


    }
    void addForce(Vector3 position)
    {
        music.clip = appleVoice;
        music.Play();
        foreach (Rigidbody c in rd)
        {
            c.AddExplosionForce(power, position, radius, 3.0F);
        }
    }
}
