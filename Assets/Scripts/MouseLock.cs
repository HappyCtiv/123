using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{

    void Awake() 
    {
        mouseLock();
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            mouseUnlock();
        }
        else
        {
        mouseLock();
        }
    }

    void mouseLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void mouseUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
