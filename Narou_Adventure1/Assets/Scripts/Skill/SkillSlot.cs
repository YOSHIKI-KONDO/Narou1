﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class SkillSlot : BASE {
    public bool CanClick;
    public SkillKind kind;

    public Slider slider;
    public Button button;
    public Text text;
    public PopUp popUp;
    public GameObject obstaclePanel;

    public void CopyValue(SkillSlot slot)
    {
        slider.value = slot.slider.value;
        text.text = slot.text.text;
        kind = slot.kind;
    }

	// Use this for initialization
	void Awake () {
		StartBASE();

        popUp = main.skillPopUp.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        if(CanClick == false) { setActive(obstaclePanel); }
    }

    private void FixedUpdate()
    {
        ApplyPopUp();
    }

    void ApplyPopUp()
    {
        if (kind == SkillKind.nothing)
        {
            setFalse(popUp.gameObject);
            return; //*
        }
        if (popUp.gameObject.activeSelf)
        {
            main.battleCtrl.skills[(int)kind].ApplyPopUp();
            //色変更
            if (main.battleCtrl.skills[(int)kind].combo != null)
            {
                popUp.texts[9].color = main.battleCtrl.skills[(int)kind].popUp.texts[9].color;
                popUp.texts[7].color = main.battleCtrl.skills[(int)kind].popUp.texts[7].color;
                popUp.texts[11].color = main.battleCtrl.skills[(int)kind].popUp.texts[11].color;
            }

            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Name_str, popUp.texts[0], popUp.texts[0].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Description_str, popUp.texts[1], popUp.texts[1].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Need_str, popUp.texts[3], popUp.texts[2].gameObject, popUp.texts[3].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].LearnCost_str, popUp.texts[5], popUp.texts[4].gameObject, popUp.texts[5].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].UseCost_str, popUp.texts[7], popUp.texts[6].gameObject, popUp.texts[7].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].UseEffect_str, popUp.texts[9], popUp.texts[8].gameObject, popUp.texts[9].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Interval_str, popUp.texts[11], popUp.texts[10].gameObject, popUp.texts[11].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Level_str, popUp.texts[12], popUp.texts[12].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Tag_str, popUp.texts[14], popUp.texts[13].gameObject, popUp.texts[14].gameObject);
        }
    }
}
