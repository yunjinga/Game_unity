using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{
    public bool isThis = false;
    public GameObject Last;
    public AudioSource music;
    public AudioClip bgmVoice;
    float waitTime = 0f;
    public string s;
    // Start is called before the first frame update
    void Start()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        bgmVoice = Resources.Load<AudioClip>("Music/" + s);
    }

    // Update is called once per frame
    void Update()
    {
        music.volume = 0.7f;
        if (isThis)
        {
            if (!music.isPlaying)
            {
                if (waitTime > 0)
                {
                    waitTime -= Time.deltaTime;
                }
                else if (waitTime <= 0)
                {
                    music.clip = bgmVoice;
                    music.Play();

                }
            }
            else if (music.isPlaying)
            {
                waitTime = 10f;
            }
        }
        else if (!isThis)
        {
            music.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Last != null)
        {
            if (Last.GetComponent<start>())
            {
                Last.GetComponent<start>().isThis = false;
            }
        }
        isThis = true;
    }
}
