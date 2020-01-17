using System.Collections;
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
    public Image switchImage;
    public Sprite upSprite, downSprite;

    string baseColorStr;
    string colorString;

    void SwitchPanel()
    {
        textDetailPanel.SetActive(!textDetailPanel.activeSelf);
        announceTextShort.gameObject.SetActive(!textDetailPanel.activeSelf); //detailとactiveを反対にする

        if (switchImage != null)
        {
            switchImage.sprite = textDetailPanel.activeSelf ? downSprite : upSprite;
        }
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
            announceTextShort.text += "\n" + "<color=#" + colorString + ">" + Txt + "</color>";
            SubstringLongText(announceTextShort);
        }
        if(announceTextLong != null)
        {
            announceTextLong.text += "\n" + "<color=#" + colorString + ">" + Txt + "</color>";
            SubstringLongText(announceTextLong);
        }
    }

    /// <summary>
    /// overflow対策
    /// </summary>
    void SubstringLongText(TextMeshProUGUI text)
    {
        text.text =
        text.text.Substring(text.text.IndexOf("\n", StringComparison.CurrentCulture) + 1);
    }

    public void ResetText()
    {
        for (int i = 0; i < 100; i++)
        {
            Add(" ");
        }
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
}
