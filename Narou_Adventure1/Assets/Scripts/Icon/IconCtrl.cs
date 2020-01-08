using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class IconCtrl : BASE {
    public Sprite[] sprites;
    public Icon iconPre;

	// Use this for initialization
	void Awake () {
		StartBASE();
	}

	// Use this for initialization
	void Start () {
        TestSprites();
	}

    void TestSprites()
    {
        if(sprites.Length < main.SD.num_need) { throw new Exception("iconの配列の数がneedKindの数より小さいです"); }
    }

    public void AddIcon(List<NeedKind> Tags, Transform parent)
    {
        foreach (var Tag in Tags)
        {
            //spriteが設定されている場合だけアイコンを表示。
            if(sprites[(int)Tag] != null)
            {
                Icon icon = Instantiate(iconPre, parent);
                icon.image.sprite = sprites[(int)Tag];
            }
        }
    }
}
