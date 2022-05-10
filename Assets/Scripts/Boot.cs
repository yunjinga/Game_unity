using System;
using UnityEngine;

public class Boot : MonoBehaviour
{
    private void Start()
    {
        CameraMgr.Instance.Bind();
    }
}