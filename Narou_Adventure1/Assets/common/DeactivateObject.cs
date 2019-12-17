using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObject : MonoBehaviour
{
    public bool lateStart = true;
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        if (lateStart)
        {
            Deactivate();
            Destroy(this);
        }
    }
}
