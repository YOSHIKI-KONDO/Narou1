using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Item_Inventory : BASE {
    public ItemKind kind;
    public Button equipButton, sellButtion, levelUpButton;
    public Text spaceText, nameText, numText;
    public PopUp popUp;
    public string Name_str, Description_str, Max_Str, Need_str, Effect_str, Cost_str, Sell_str;

    //ItemCtrlから呼ぶ
    public void StartInventory(ItemKind kind)
    {
        this.kind = kind;

        StartBASE();
        popUp = main.itemPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);

        equipButton.onClick.AddListener(() => main.itemCtrl.Equip(kind));
        sellButtion.onClick.AddListener(() => main.itemCtrl.Sell_Inventory(kind));
        levelUpButton.onClick.AddListener(() => main.itemCtrl.LevelUp(kind));
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
        CheckButton();
        ApplyTexts();
        ApplyPopUp();
    }

    void ApplyTexts()
    {
        spaceText.text = main.itemCtrl.items[(int)kind].size.ToString();
        nameText.text = main.enumCtrl.items[(int)kind].Name();
        numText.text = main.itemCtrl.equipNum[(int)kind].ToString() + "/" + (main.itemCtrl.equipNum[(int)kind] + main.itemCtrl.InventoryNum[(int)kind]).ToString();
    }

    void CheckButton()
    {
        //buy
        if (main.itemCtrl.CanEquip(kind))
        {
            equipButton.interactable = true;
        }
        else
        {
            equipButton.interactable = false;
        }
        //sell
        if (main.itemCtrl.CanSell_Inventory(kind))
        {
            sellButtion.interactable = true;
        }
        else
        {
            sellButtion.interactable = false;
        }
        //levelUP
        if (main.itemCtrl.CanLevelUp(kind))
        {
            levelUpButton.interactable = true;
        }
        else
        {
            levelUpButton.interactable = false;
        }
    }

    //NOTE:ItemCtrlで一括管理したい
    void ApplyPopUp()
    {
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            Name_str = main.enumCtrl.items[(int)kind].Name();
            Description_str = main.enumCtrl.items[(int)kind].Description();
            if (main.itemCtrl.items[(int)kind].haveSource) { Description_str += Description_str == "" ? main.itemCtrl.items[(int)kind].SourceDetail() : "\n" + main.itemCtrl.items[(int)kind].SourceDetail(); }
            Max_Str = "Max:" + ((main.itemCtrl.items[(int)kind].MaxEquip == null) ? "∞" : main.itemCtrl.items[(int)kind].MaxEquip.ToString());
            Effect_str = ProgressDetail(main.itemCtrl.items[(int)kind].EffectLists, main.itemCtrl.items[(int)kind].LevelFactor());
            Cost_str = ProgressDetail(main.itemCtrl.items[(int)kind].BuyLists);
            Sell_str = ProgressDetail(main.itemCtrl.items[(int)kind].SellLists);
            Cost_str = ProgressDetail(main.itemCtrl.items[(int)kind].BuyLists);
            //needいらない

            ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
            ChangeTextAdaptive(Description_str, popUp.texts[1], popUp.texts[1].gameObject);
            ChangeTextAdaptive(Max_Str, popUp.texts[2], popUp.texts[2].gameObject);
            ChangeTextAdaptive(Need_str, popUp.texts[4], popUp.texts[3].gameObject, popUp.texts[4].gameObject);
            ChangeTextAdaptive(Effect_str, popUp.texts[6], popUp.texts[5].gameObject, popUp.texts[6].gameObject);
            ChangeTextAdaptive(Cost_str, popUp.texts[8], popUp.texts[7].gameObject, popUp.texts[8].gameObject);
            ChangeTextAdaptive(Sell_str, popUp.texts[10], popUp.texts[9].gameObject, popUp.texts[10].gameObject);
        }
    }
}
