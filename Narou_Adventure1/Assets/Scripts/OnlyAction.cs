using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class OnlyAction : BASE {
    public Button button;
    public bool isOn;
    public Action SelectedAction; //選択された時に呼ばれる関数

    // Use this for initialization
    public void AwakeOnlyAction (Button button) {
		StartBASE();
        this.button = button;
        button.onClick.AddListener(SwitchProgress);
        main.progressCtrl.list.Add(this);
    }

    public void SwitchProgress()
    {
        main.progressCtrl.SwitchProgress(this);
    }
}
