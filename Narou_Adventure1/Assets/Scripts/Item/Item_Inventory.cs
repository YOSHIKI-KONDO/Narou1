using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UsefulMethod;

public class Item_Inventory : BASE, IPointerDownHandler{
    public ItemKind kind;
    public Button equipButton, sellButtion, levelUpButton;
    public Text spaceText, nameText, numText, rarityText;
    public Toggle lockToggle;
    public GameObject newObject;
    public PopUp popUp;
    public string Name_str, Description_str, Max_Str, Need_str, Effect_str, Cost_str, Sell_str;
    public bool Watched { get => main.SR.watched_Inventory[(int)kind]; set => main.SR.watched_Inventory[(int)kind] = value; }

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
        //星で表記
        rarityText.text = main.itemCtrl.items[(int)kind].StarFromRarity(main.itemCtrl.items[(int)kind].rarity);

        //lock toggle
        lockToggle.onValueChanged.AddListener(x => main.itemCtrl.SynchronizeLock(x, kind));
        lockToggle.isOn = main.SR.locked_Item[(int)kind];//セーブを代入
    }
	
	// Update is called once per frame
	void Update () {
        CheckButton();
        ApplyTexts();
        ApplyPopUp();
        ApplyNewObj();
    }

    //見られていたらnewをfalseにする。
    void ApplyNewObj()
    {
        if (Watched)
        {
            setFalse(newObject);
        }
        else
        {
            setActive(newObject);
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Watched = true;
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
