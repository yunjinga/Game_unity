using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST : MonoBehaviour
{
    public GameObject start;
    bool isdown = false;
    float wait = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isdown == true)
        {
            start.SetActive(false);
            wait += Time.deltaTime;
            Camera.main.GetComponent<screenChange>().SetBlackBool = true;
            if (wait > 5)
            {
                Application.LoadLevel(1);
            }
        }
    }
    public void start1()//开始按键的功能
    {
        isdown = true;
        
       
    }
    public void Quit()
    {


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
         Application.Quit();
#endif
    }
}
