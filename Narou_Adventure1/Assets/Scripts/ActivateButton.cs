using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateButton : MonoBehaviour
{
    Button button;
    public bool lateStart = true;
    public bool destroyThis = true;
    public float delay;
    public void Activate()
    {
        button.enabled = true;
        if (destroyThis)
            Destroy(this);
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void LateUpdate()
    {
        if (lateStart)
        {
            //Activate();
            Invoke("Activate", delay);
        }
    }
}
