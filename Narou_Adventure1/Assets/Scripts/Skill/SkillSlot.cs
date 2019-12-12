using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class SkillSlot : BASE {
    public SkillKind kind;

    public Slider slider;
    public Button button;
    public Text text;

    public void CopyValue(SkillSlot slot)
    {
        slider.value = slot.slider.value;
        text.text = slot.text.text;
    }

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
