using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position : MonoBehaviour
{
    public GameObject start1;
    public GameObject start2;
    public GameObject start3;
    public GameObject start4;
    public GameObject start5;
    public GameObject start6;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("l", 0);
        start1 = GameObject.Find("Start");
        start2 = GameObject.Find("Start2");
        start3 = GameObject.Find("Start3");
        start4 = GameObject.Find("start4");
        start5 = GameObject.Find("start5");
        start6 = GameObject.Find("start6");
        int i = PlayerPrefs.GetInt("l");
        switch (i)
        {
            case 0:
                {
                    transform.position = start1.transform.position;
                    break;
                }
            case 1:
                {
                    transform.position = start2.transform.position;
                    break;
                }
            case 2:
                {
                    transform.position = start3.transform.position;
                    break;
                }
            case 3:
                {
                    transform.position = start4.transform.position;
                    break;
                }
            case 4:
                {
                    transform.position = start5.transform.position;
                    break;
                }
            case 5:
                {
                    transform.position = start6.transform.position;
                    break;
                }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (PlayerPrefs.GetInt("l") < 5)
            {
                PlayerPrefs.SetInt("l", PlayerPrefs.GetInt("l") + 1);
                int i = PlayerPrefs.GetInt("l");
                switch (i)
                {
                    case 0:
                        {
                            transform.position = start1.transform.position;
                            break;
                        }
                    case 1:
                        {
                            transform.position = start2.transform.position;
                            break;
                        }
                    case 2:
                        {
                            transform.position = start3.transform.position;
                            break;
                        }
                    case 3:
                        {
                            transform.position = start4.transform.position;
                            break;
                        }
                    case 4:
                        {
                            transform.position = start5.transform.position;
                            break;
                        }
                    case 5:
                        {
                            transform.position = start6.transform.position;
                            break;
                        }
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            PlayerPrefs.SetInt("l", 0);
            int i = PlayerPrefs.GetInt("l");
            switch (i)
            {
                case 0:
                    {
                        transform.position = start1.transform.position;
                        break;
                    }
                case 1:
                    {
                        transform.position = start2.transform.position;
                        break;
                    }
                case 2:
                    {
                        transform.position = start3.transform.position;
                        break;
                    }
                case 3:
                    {
                        transform.position = start4.transform.position;
                        break;
                    }
                case 4:
                    {
                        transform.position = start5.transform.position;
                        break;
                    }
                case 5:
                    {
                        transform.position = start6.transform.position;
                        break;
                    }
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (PlayerPrefs.GetInt("l") > 0)
            {
                PlayerPrefs.SetInt("l", PlayerPrefs.GetInt("l") - 1);
                int i = PlayerPrefs.GetInt("l");
                switch (i)
                {
                    case 0:
                        {
                            transform.position = start1.transform.position;
                            break;
                        }
                    case 1:
                        {
                            transform.position = start2.transform.position;
                            break;
                        }
                    case 2:
                        {
                            transform.position = start3.transform.position;
                            break;
                        }
                    case 3:
                        {
                            transform.position = start4.transform.position;
                            break;
                        }
                    case 4:
                        {
                            transform.position = start5.transform.position;
                            break;
                        }
                    case 5:
                        {
                            transform.position = start6.transform.position;
                            break;
                        }
                }
            }

        }
    }

}
