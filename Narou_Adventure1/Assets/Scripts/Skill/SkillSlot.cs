using System.Collections;
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
                popUp.textPros[9].color = main.battleCtrl.skills[(int)kind].popUp.textPros[9].color;
                popUp.textPros[7].color = main.battleCtrl.skills[(int)kind].popUp.textPros[7].color;
                popUp.textPros[11].color = main.battleCtrl.skills[(int)kind].popUp.textPros[11].color;
            }

            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Name_str, popUp.textPros[0], popUp.textPros[0].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Description_str, popUp.textPros[1], popUp.textPros[1].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Need_str, popUp.textPros[3], popUp.textPros[2].gameObject, popUp.textPros[3].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].LearnCost_str, popUp.textPros[5], popUp.textPros[4].gameObject, popUp.textPros[5].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].UseCost_str, popUp.textPros[7], popUp.textPros[6].gameObject, popUp.textPros[7].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].UseEffect_str, popUp.textPros[9], popUp.textPros[8].gameObject, popUp.textPros[9].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Interval_str, popUp.textPros[11], popUp.textPros[10].gameObject, popUp.textPros[11].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Level_str, popUp.textPros[12], popUp.textPros[12].gameObject);
            ChangeTextAdaptive(main.battleCtrl.skills[(int)kind].Tag_str, popUp.textPros[14], popUp.textPros[13].gameObject, popUp.textPros[14].gameObject);
        }
    }
}
