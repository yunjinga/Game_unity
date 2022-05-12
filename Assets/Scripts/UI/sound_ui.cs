using UnityEngine;
using System.Collections;

public class sound_ui : MonoBehaviour
{

	//public AudioClip clip;
	public AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.Stop();
    }
    public void click()
	{
		source.Play();
	}
}
