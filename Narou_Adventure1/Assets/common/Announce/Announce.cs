﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using TMPro;

public class Announce : BASE {

    public TextMeshProUGUI announceTextShort;
    public TextMeshProUGUI announceTextLong;
    public Button switchButton;
    public GameObject textDetailPanel;
    public Color baseColor;

    string baseColorStr;
    string colorString;

    void SwitchPanel()
    {
        textDetailPanel.SetActive(!textDetailPanel.activeSelf);
        switchButton.gameObject.GetComponentInChildren<Text>().text
            = textDetailPanel.activeSelf ? "▼" : "▲";
    }

    public void Add(string Txt, Color? Clr = null)
    {
        if (Clr == null)
        {
            colorString = baseColorStr;
        }
        else
        {
            colorString = ColorUtility.ToHtmlStringRGB((Color)Clr);
        }

        if (announceTextShort != null)
        {
            announceTextShort.text = "<color=#" + colorString + ">" + Txt + "</color>";
        }
        announceTextLong.text = announceTextLong.text + "\n" + "<color=#" + colorString + ">"+ Txt + "</color>";
        SubstringLongText();
    }

    /// <summary>
    /// overflow対策
    /// </summary>
    void SubstringLongText()
    {
        announceTextLong.text=
        announceTextLong.text.Substring(announceTextLong.text.IndexOf("\n", StringComparison.CurrentCulture)+1);
    }

    public void ResetText()
    {
        announceTextLong.text = "";
        announceTextShort.text = "";
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
        switchButton?.onClick.AddListener(SwitchPanel);
        baseColorStr = ColorUtility.ToHtmlStringRGB(baseColor);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
