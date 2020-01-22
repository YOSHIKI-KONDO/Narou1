using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using TMPro;

public class Item_Equip : BASE
{
    public ItemKind kind;
    public Button removeButton, sellButtion, levelUpButton;
    public Text spaceText, nameText, numText, levelText, maxNumText;
    public TextMeshProUGUI rarityText;
    public Toggle lockToggle;
    public Transform attributesParent;
    public PopUp popUp;
    public string Name_str, Description_str, Max_Str, Need_str, Effect_str, Cost_str, Sell_str, LvCost_Str, LvEffect_Str;
    ITEM[] items;
    public GameObject highLight;
    HighLightFunction highLightF;

    //ItemCtrlから呼ぶ
    public void StartEquip(ItemKind kind)
    {
        this.kind = kind;

        StartBASE();
        popUp = main.itemPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);

        removeButton.onClick.AddListener(() => main.itemCtrl.Remove(kind));
        sellButtion.onClick.AddListener(() => main.itemCtrl.Sell_Equip(kind));
        levelUpButton.onClick.AddListener(() => main.itemCtrl.LevelUp(kind));
    }

    // Use this for initialization
    void Awake()
    {
        StartBASE();

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
    }

    void ApplyTexts()
    {
        spaceText.text = items[(int)kind].size.ToString();
        nameText.text = main.enumCtrl.items[(int)kind].Name();
        maxNumText.text = (items[(int)kind].MaxEquip == null) ? "Inf" : items[(int)kind].MaxEquip.ToString();
        numText.text = main.itemCtrl.equipNum[(int)kind].ToString() + "/" + (main.itemCtrl.equipNum[(int)kind] + main.itemCtrl.InventoryNum[(int)kind]).ToString();
        levelText.text = items[(int)kind].level >= items[(int)kind].maxLevel ?
            "Lv Max" :
            "Lv" + items[(int)kind].level.ToString() + "/" + items[(int)kind].maxLevel.ToString();
    }

    void CheckButton()
    {
        //buy
        if (main.itemCtrl.CanRemove(kind))
        {
            removeButton.interactable = true;
        }
        else
        {
            removeButton.interactable = false;
        }
        //sell
        if (main.itemCtrl.CanSell_Equip(kind))
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

    void ApplyPopUp()
    {
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            Name_str = main.enumCtrl.items[(int)kind].Name();
            Description_str = main.enumCtrl.items[(int)kind].Description();
            if (items[(int)kind].haveSource) { Description_str += Description_str == "" ? items[(int)kind].SourceDetail() : "\n" + items[(int)kind].SourceDetail(); }
            Max_Str = "Max:" + ((items[(int)kind].MaxEquip == null) ? "Inf" : items[(int)kind].MaxEquip.ToString());
            Effect_str = ProgressDetail(items[(int)kind].EffectLists, items[(int)kind].LevelFactor());
            Cost_str = ProgressDetail(items[(int)kind].BuyLists);
            Sell_str = ProgressDetail(items[(int)kind].SellLists);
            LvCost_Str = ProgressDetail(items[(int)kind].itemPointDeal);
            LvEffect_Str = items[(int)kind].DetailNextLEffect(items[(int)kind].level);

            //needいらない

            ChangeTextAdaptive(Name_str, popUp.textPros[0], popUp.textPros[0].gameObject);
            ChangeTextAdaptive(rarityText.text, popUp.textPros[1], popUp.textPros[1].gameObject);
            ChangeTextAdaptive(Description_str, popUp.textPros[2], popUp.textPros[2].gameObject);
            ChangeTextAdaptive(levelText.text, popUp.textPros[3], popUp.textPros[3].gameObject);
            ChangeTextAdaptive(Need_str, popUp.textPros[5], popUp.textPros[4].gameObject, popUp.textPros[5].gameObject);
            ChangeTextAdaptive(Cost_str, popUp.textPros[7], popUp.textPros[6].gameObject, popUp.textPros[7].gameObject);
            ChangeTextAdaptive(Sell_str, popUp.textPros[9], popUp.textPros[8].gameObject, popUp.textPros[9].gameObject);
            ChangeTextAdaptive(LvCost_Str, popUp.textPros[11], popUp.textPros[10].gameObject, popUp.textPros[11].gameObject);
            ChangeTextAdaptive(LvEffect_Str, popUp.textPros[13], popUp.textPros[12].gameObject, popUp.textPros[13].gameObject);
            ChangeTextAdaptive(Effect_str, popUp.textPros[15], popUp.textPros[14].gameObject, popUp.textPros[15].gameObject);
            ChangeTextAdaptive(Max_Str, popUp.textPros[16], popUp.textPros[16].gameObject);
        }
    }
}
