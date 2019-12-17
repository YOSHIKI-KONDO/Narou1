using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateToggle : MonoBehaviour
{
    Toggle toggle;
    public bool lateStart = true;
    public void Activate()
    {
        toggle.enabled = true;
    }

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void LateUpdate()
    {
        if (lateStart)
        {
            Activate();
            Destroy(this);
        }
    }
}
