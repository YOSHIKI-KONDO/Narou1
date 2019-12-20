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
    public string actionName;
    public float sliderValue;
    bool addCtrl;

    public BoolSync Watched;

    // Use this for initialization
    public void AwakeOnlyAction(Button button, string ActionName, BoolSync Watched, bool addCtrl = true) {
		StartBASE();
        this.button = button;
        this.actionName = ActionName;
        this.Watched = Watched;
        button.onClick.AddListener(SwitchProgress);
        this.addCtrl = addCtrl;
        if (addCtrl)
        {
            main.progressCtrl.list.Add(this);
        }
    }

    public void SwitchProgress()
    {
        Watched?.Invoke(true); //watchedが設定されていれば、それをtrueにする
        if(addCtrl)
            main.progressCtrl.SwitchProgress(this);
    }
}
