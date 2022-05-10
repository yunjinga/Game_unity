using System;
using UnityEngine;

public class CameraMgr
{
    private static CameraMgr m_Instance;

    public static CameraMgr Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new CameraMgr();
            return m_Instance;
        }
    }

    public GameObject m_MainCamera;
    public void Bind()
    {
        m_MainCamera = GameObject.Find("CM vcam1").gameObject;
        if (m_MainCamera == null)
            Debug.LogError("[CameraLog]Can't find Main Camera");
    }
}