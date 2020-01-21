using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// トグルを同期する関数
/// 1. 同期元のtoggleにこのComponentをAdd
/// 2. otherToggleに同期先のToggleをアタッチ
/// 3. インスペクターから同期元のValueChangedにSynchronizeを設定。
/// </summary>
public class SyncToggle : MonoBehaviour
{
    public Toggle otherToggle;
    public void Synchronize(bool isOn)
    {
        if(otherToggle.isOn != isOn)
        {
            otherToggle.isOn = isOn;
        }
    }
}
