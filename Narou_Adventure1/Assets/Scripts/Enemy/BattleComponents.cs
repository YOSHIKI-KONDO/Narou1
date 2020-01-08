using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BattleComponents : BASE {
    public bool isEnemy; //敵の場合はInspectorからこれをtrueにする
    public EnemyKind kind;
    public Text Name_text;
    public Text hp_text;
    public Text atk_text;
    public Slider hp_slider;
    public Slider int_slider;
    public Toggle targetToggle; //enemyの時のみ使う
    PopUp popUp;

    public string maxHp;
    public string currentHp;

    private void Awake()
    {
        Name_text = GetComponentsInChildren<Text>()[0];
        hp_text = GetComponentsInChildren<Text>()[1];
        atk_text = GetComponentsInChildren<Text>()[2];
        hp_slider = GetComponentsInChildren<Slider>()[0];
        int_slider = GetComponentsInChildren<Slider>()[1];
        targetToggle = GetComponentInChildren<Toggle>();
        if(targetToggle != null)
        {
            targetToggle.group = targetToggle.GetComponentInParent<ToggleGroup>();
        }
        if (isEnemy)
        {
            StartBASE();
            popUp = main.enemyPopUp.StartPopUp(gameObject, main.windowShowCanvas);
            popUp.UpdateAction = ApplyPopUp;
            setFalse(popUp.texts[2].gameObject);
        }
    }

    public void ApplyNormalObj(string Name, string currentHp, string maxHp, string atk, float hp_sliderValue, float int_sliderValue, EnemyKind kind = EnemyKind.nothing)
    {
        this.kind = kind;
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        float sqrt_int_sliderValue = Mathf.Sqrt(int_sliderValue);

        if (Name_text != null) { Name_text.text = Name; }
        if (hp_text != null) { hp_text.text = this.currentHp + "/" + this.maxHp; }
        if (atk_text != null) { atk_text.text = atk; }
        if (hp_slider != null) { hp_slider.value = hp_sliderValue; }
        if (int_slider != null) { int_slider.value = sqrt_int_sliderValue; }
    }

    void ApplyPopUp()
    {
        if (kind == EnemyKind.nothing)
        {
            Debug.Log("ApplyNOrmalObjでEnumが入力されていません");
            return;
        }
        popUp.texts[0].text = Name_text.text;
        popUp.texts[1].text = StarFromRank(main.battleCtrl.enemys[(int)kind].rank);
        //popUp.texts[2]
        popUp.texts[3].text = "HP:" + maxHp + ", " + atk_text.text + ", Int:" + main.battleCtrl.enemys[(int)kind].interval.ToString("F0");
        ChangeTextAdaptive(DropsDetail(main.battleCtrl.enemys[(int)kind].drops), popUp.texts[5], popUp.texts[4].gameObject, popUp.texts[5].gameObject);
    }

    string StarFromRank(int Rank)
    {
        string sum = "";
        for (int i = 0; i < Rank; i++)
        {
            sum += "★";
        }
        return sum;
    }
}
