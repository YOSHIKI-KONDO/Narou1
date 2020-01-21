using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncToggle : MonoBehaviour
{
    public void Synchronize(bool isOn, Toggle otherToggle)
    {
        if(otherToggle.isOn != isOn)
        {
            otherToggle.isOn = isOn;
        }
    }
}
