using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BattleComponents : BASE {
    public Text Name_text;
    public Text hp_text;
    public Text atk_text;
    public Slider hp_slider;
    public Slider int_slider;

    private void Awake()
    {
        Name_text = GetComponentsInChildren<Text>()[0];
        hp_text = GetComponentsInChildren<Text>()[1];
        atk_text = GetComponentsInChildren<Text>()[2];
        hp_slider = GetComponentsInChildren<Slider>()[0];
        int_slider = GetComponentsInChildren<Slider>()[1];
    }

    public void ApplyNormalObj(string Name, string hp, string atk, float hp_sliderValue, float int_sliderValue)
    {
        if (Name_text != null) { Name_text.text = Name; }
        if (hp_text != null) { hp_text.text = hp; }
        if (atk_text != null) { atk_text.text = atk; }
        if (hp_slider != null) { hp_slider.value = hp_sliderValue; }
        if (int_slider != null) { int_slider.value = int_sliderValue; }
    }
}
