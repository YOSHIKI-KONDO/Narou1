using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using TMPro;

public class Item_LevelUp : BASE
{
    public ItemKind kind;
    public Button equipButton, sellButtion, levelUpButton;
    public Text spaceText, nameText, numText, levelText, maxNumText;
    public TextMeshProUGUI rarityText;
    public Toggle lockToggle;
    public GameObject newObject;
    public Transform attributesParent;
    public PopUp popUp;
    public string Name_str, Description_str, Max_Str, Need_str, Effect_str, Cost_str, Sell_str, LvCost_Str, LvEffect_Str;
    //public bool Watched { get => main.SR.watched_Inventory[(int)kind]; set => main.SR.watched_Inventory[(int)kind] = value; }
    bool hovered;
    EnterExitEvent eeevent;
    ITEM[] items;
    public GameObject highLight;
    HighLightFunction highLightF;

    //ItemCtrlから呼ぶ
    public void StartLevelUp(ItemKind kind)
    {
        this.kind = kind;

        StartBASE();
        popUp = main.itemPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);

        equipButton.onClick.AddListener(() => main.itemCtrl.Equip(kind));
        sellButtion.onClick.AddListener(() => main.itemCtrl.Sell_Inventory(kind));
        levelUpButton.onClick.AddListener(() => main.itemCtrl.LevelUp(kind));
    }

    // Use this for initialization
    void Awake()
    {
        StartBASE();
        eeevent = gameObject.AddComponent<EnterExitEvent>();
        eeevent.EnterEvent = () => { hovered = true; };
        eeevent.ExitEvent = () => { hovered = false; };

        items = main.itemCtrl.items;
    }

    // Use this for initialization
    void Start()
    {
        //星で表記
        rarityText.text = items[(int)kind].StarFromRarity(items[(int)kind].rarity);

        //lock toggle
        lockToggle.onValueChanged.AddListener(x => main.itemCtrl.SynchronizeLock(x, kind));
        lockToggle.isOn = main.SR.locked_Item[(int)kind];//セーブを代入

        main.iconCtrl.AddIcon(items[(int)kind].sources, attributesParent);

        //ハイライト
        highLightF = gameObject.AddComponent<HighLightFunction>();
        highLightF.StartContents(highLight, items[(int)kind].EffectLists);
    }

    // Update is called once per frame
    void Update()
    {
        CheckButton();
        ApplyTexts();
        ApplyPopUp();
        //ApplyNewObj();
    }

    //見られていたらnewをfalseにする。
    //void ApplyNewObj()
    //{
    //    if (hovered)
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            Watched = true;
    //        }
    //    }

    //    if (Watched)
    //    {
    //        setFalse(newObject);
    //    }
    //    else
    //    {
    //        setActive(newObject);
    //    }
    //}

    void ApplyTexts()
    {
        spaceText.text = items[(int)kind].size.ToString();
        nameText.text = main.enumCtrl.items[(int)kind].Name();
        maxNumText.text = (items[(int)kind].MaxEquip == null) ? "∞" : items[(int)kind].MaxEquip.ToString();
        numText.text = main.itemCtrl.equipNum[(int)kind].ToString() + "/" + (main.itemCtrl.equipNum[(int)kind] + main.itemCtrl.InventoryNum[(int)kind]).ToString();
        levelText.text = items[(int)kind].level >= items[(int)kind].maxLevel ?
            "Lv Max" :
            "Lv" + items[(int)kind].level.ToString() + "/" + items[(int)kind].maxLevel.ToString();
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
            if (items[(int)kind].haveSource) { Description_str += Description_str == "" ? items[(int)kind].SourceDetail() : "\n" + items[(int)kind].SourceDetail(); }
            Max_Str = "Max:" + ((items[(int)kind].MaxEquip == null) ? "∞" : items[(int)kind].MaxEquip.ToString());
            Effect_str = ProgressDetail(items[(int)kind].EffectLists, items[(int)kind].LevelFactor());
            Cost_str = ProgressDetail(items[(int)kind].BuyLists);
            Sell_str = ProgressDetail(items[(int)kind].SellLists);
            LvCost_Str = ProgressDetail(items[(int)kind].itemPointDeal);
            LvEffect_Str = items[(int)kind].DetailNextLEffect(items[(int)kind].level);

            //needいらない

            ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
            //ChangeTextAdaptive(rarityText.text, popUp.texts[1], popUp.texts[1].gameObject);
            ChangeTextAdaptive(rarityText.text, popUp.textPros[0], popUp.textPros[0].gameObject);
            ChangeTextAdaptive(Description_str, popUp.texts[2], popUp.texts[2].gameObject);
            ChangeTextAdaptive(levelText.text, popUp.texts[3], popUp.texts[3].gameObject);
            ChangeTextAdaptive(Need_str, popUp.texts[5], popUp.texts[4].gameObject, popUp.texts[5].gameObject);
            ChangeTextAdaptive(Cost_str, popUp.texts[7], popUp.texts[6].gameObject, popUp.texts[7].gameObject);
            ChangeTextAdaptive(Sell_str, popUp.texts[9], popUp.texts[8].gameObject, popUp.texts[9].gameObject);
            ChangeTextAdaptive(LvCost_Str, popUp.texts[11], popUp.texts[10].gameObject, popUp.texts[11].gameObject);
            ChangeTextAdaptive(LvEffect_Str, popUp.texts[13], popUp.texts[12].gameObject, popUp.texts[13].gameObject);
            ChangeTextAdaptive(Effect_str, popUp.texts[15], popUp.texts[14].gameObject, popUp.texts[15].gameObject);
            ChangeTextAdaptive(Max_Str, popUp.texts[16], popUp.texts[16].gameObject);
        }
    }
}
