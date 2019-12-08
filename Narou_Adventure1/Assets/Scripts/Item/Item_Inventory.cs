using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Item_Inventory : BASE {
    public ItemKind kind;
    public Button equipButton, sellButtion;
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
            Effect_str = ProgressDetail(main.itemCtrl.items[(int)kind].EffectLists);
            Cost_str = ProgressDetail(main.itemCtrl.items[(int)kind].BuyLists);
            Sell_str = ProgressDetail(main.itemCtrl.items[(int)kind].SellLists);
            Cost_str = ProgressDetail(main.itemCtrl.items[(int)kind].BuyLists);
            //needいらない

            if (Name_str == "" || Name_str == null)
            {
                setFalse(popUp.texts[0].gameObject);
            }
            else
            {
                popUp.texts[0].text = Name_str;
            }

            if (Description_str == "" || Description_str == null)
            {
                setFalse(popUp.texts[1].gameObject);
            }
            else
            {
                popUp.texts[1].text = Description_str;
            }

            if (Max_Str == "" || Max_Str == null)
            {
                setFalse(popUp.texts[2].gameObject);
            }
            else
            {
                popUp.texts[2].text = Max_Str;
            }

            if (Need_str == "" || Need_str == null)
            {
                setFalse(popUp.texts[3].gameObject);
                setFalse(popUp.texts[4].gameObject);
            }
            else
            {
                popUp.texts[4].text = Need_str;
            }

            if (Effect_str == "" || Effect_str == null)
            {
                setFalse(popUp.texts[5].gameObject);
                setFalse(popUp.texts[6].gameObject);
            }
            else
            {
                popUp.texts[6].text = Effect_str;
            }

            if (Cost_str == "" || Cost_str == null)
            {
                setFalse(popUp.texts[7].gameObject);
                setFalse(popUp.texts[8].gameObject);
            }
            else
            {
                popUp.texts[8].text = Cost_str;
            }

            if (Sell_str == "" || Sell_str == null)
            {
                setFalse(popUp.texts[9].gameObject);
                setFalse(popUp.texts[10].gameObject);
            }
            else
            {
                popUp.texts[10].text = Sell_str;
            }
        }
    }
}
