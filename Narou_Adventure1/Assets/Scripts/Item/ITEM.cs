using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// ショップに全て置く
/// </summary>
public class ITEM : BASE, INeed, ISetSource
{
    public ItemKind kind;
    public List<NeedKind> sources = new List<NeedKind>();
    public int size;
    public int? MaxEquip;
    public int rarity;
    public List<Dealing> BuyLists = new List<Dealing>();
    public List<Dealing> SellLists = new List<Dealing>();
    public List<Dealing> EffectLists = new List<Dealing>();
    ItemComponents components;
    //newやlockなども宣言する
    Button buyButton, sellButton, levelUpButton;
    Text spaceText, nameText, numText, rarityText, levelText, maxNumText;
    public Toggle lockToggle;
    GameObject newObject;
    Transform AttributesParent;
    PopUp popUp;
    ReleaseFunction release;
    public NeedFunciton need;
    public string Name_str, Description_str, Max_Str, Need_str, Effect_str, Cost_str, Sell_str, LvCost_Str, LvEffect_Str;
    public List<Dealing> itemPointDeal = new List<Dealing>(); //実際の処理はdealingを通じて行わない。表示用。

    public int[] InventoryNum { get => main.SR.inventoryNum_Item; set => main.SR.inventoryNum_Item = value; }
    public int[] equipNum { get => main.SR.equipNum_Item; set => main.SR.equipNum_Item = value; }
    public int level { get => main.SR.level_Item[(int)kind]; set => main.SR.level_Item[(int)kind] = value; }
    public int maxLevel = 30;
    public double level_power;
    public double LevelFactor() { return levelFactor_cash ?? CalculateCurrentLFactor(); }
    double? levelFactor_cash;//レベルアップしたときにnullを入れる
    public void LevelUp() { level++; levelFactor_cash = null; }
    double CalculateCurrentLFactor()
    {
        levelFactor_cash = CalculateLFactor(level);
        return (double)levelFactor_cash;
    }

    //重い
    public string DetailNextLEffect(int Level)
    {
        if(Level >= maxLevel)
        {
            return "Max";
        }
        else
        {
            return "+" + tDigit(100d * (CalculateLFactor( Level + 1 ) - CalculateLFactor(Level)), 2) + "%";
        }
    }

    //重い
    double CalculateLFactor(int Level)
    {
        if (maxLevel <= 1)
        {
            return 1;
        }
        double cal = Math.Pow(Level, level_power);
        if (Level >= maxLevel)
        {
            cal += 0.5; //+50%bonus
        }
        return cal;
    }

    public bool haveSource;
    public void SetSource(params NeedKind[] sourceKinds)
    {
        haveSource = true;
        foreach (var n in sourceKinds)
        {
            sources.Add(n);
        }
    }

    public string SourceDetail()
    {
        string sum_str = "";
        foreach (var src in sources)
        {
            if (src == NeedKind.nothing) { continue; }
            if (sum_str != "") { sum_str += ", "; }
            sum_str += main.enumCtrl.needs[(int)src].Name();
        }
        return sum_str;
    }

    public virtual bool Requires() { return true; }           //表示されるための条件
    public virtual bool CompleteCondition() { return false; } //もう表示しなくなる条件
    public virtual bool Need()                                //表示した後で設置したりするための条件(trueなら置ける)
    {
        // need(tag)が設定してあり、かつ満たしていなければfalseを返す
        if (need.hasNeeds)
        {
            if(need.TemplateNeed() == false)
            {
                return false;
            }
        }
        return true;
    }               

    // Use this for initialization
    public void AwakeItem (ItemKind kind, int size, int? max = null, int rarity = 1, int maxLevel = 30, int maxLevelMag = 2) {
		StartBASE();
        this.kind = kind;
        this.size = size;
        this.MaxEquip = max;
        this.rarity = rarity;
        this.maxLevel = maxLevel;
        main.itemCtrl.items[(int)kind] = this;
        if (level <= 0) { level = 1; }//レベルは0以上でなければならない
        level_power = CalculatePower(maxLevel, maxLevelMag);


        //アイテムコンポーネントから参照をコピー
        components = GetComponent<ItemComponents>();
        buyButton = components.buyButton;
        sellButton = components.sellButton;
        levelUpButton = components.levelUpButton;
        spaceText = components.spaceText;
        nameText = components.nameText;
        numText = components.numText;
        levelText = components.levelText;
        newObject = components.newObj;
        rarityText = components.rarityText;
        maxNumText = components.maxNumText;
        lockToggle = components.lockToggle;
        AttributesParent = components.AttributesParent;
        buyButton.onClick.AddListener(() => main.itemCtrl.Buy(kind));
        sellButton.onClick.AddListener(() => main.itemCtrl.Sell_Shop(kind));
        levelUpButton.onClick.AddListener(() => main.itemCtrl.LevelUp(kind));
        rarityText.text = StarFromRarity(rarity);
        SetItemPointDealing(rarity);
        lockToggle.onValueChanged.AddListener(x => main.itemCtrl.SynchronizeLock(x, kind));

        popUp = main.itemPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_Item[(int)kind], x),
            x => Sync(ref main.SR.completed_Item[(int)kind], x),
            x => Requires(),
            x => Sync(ref main.SR.watched_Shop[(int)kind], x),
            newObject,
            main.enumCtrl.items[(int)kind].Name() + "(Shop)");
        release.action_activated = () => { main.SR.discover_Item[(int)kind] = true; }; //発見
        need = gameObject.AddComponent<NeedFunciton>();
    }

    // Use this for initialization
    public void StartItem () {
        //lock toggle
        lockToggle.onValueChanged.AddListener(x => main.itemCtrl.SynchronizeLock(x, kind));
        lockToggle.isOn = main.SR.locked_Item[(int)kind];//セーブを代入

        //アイコンを表示
        main.iconCtrl.AddIcon(sources, AttributesParent);
    }

    // Update is called once per frame
    public void UpdateItem () {
        CheckButton();
        ApplyTexts();
        if (popUp.gameObject.activeSelf)
        {
            ApplyPopUp();
        }
    }

    // Update is called once per frame
    public void FixedUpdateItem()
    {
        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
        }
        nameText.text = Name_str;
    }

    public string StarFromRarity(int Rarity)
    {
        string sum = "";
        for (int i = 0; i < Rarity; i++)
        {
            sum += "★";
        }
        return sum;
    }

    //表示用にitemPointをDealingに入れるだけの関数。実際の処理はdealingを通じて行わない。
    void SetItemPointDealing(int Rarity)
    {
        if (itemPointDeal.Count == 0) { itemPointDeal.Add(null); }
        switch (Rarity)
        {
            case 1:
                itemPointDeal[0] = new Dealing(ResourceKind.itemPoint1, Dealing.R_ParaKind.current, -1);
                break;
            case 2:
                itemPointDeal[0] = new Dealing(ResourceKind.itemPoint2, Dealing.R_ParaKind.current, -1);
                break;
            case 3:
                itemPointDeal[0] = new Dealing(ResourceKind.itemPoint3, Dealing.R_ParaKind.current, -1);
                break;
            case 4:
                itemPointDeal[0] = new Dealing(ResourceKind.itemPoint4, Dealing.R_ParaKind.current, -1);
                break;
            case 5:
                itemPointDeal[0] = new Dealing(ResourceKind.itemPoint5, Dealing.R_ParaKind.current, -1);
                break;
            default:
                break;
        }
    }

    double CalculatePower(int _maxLevel, double objective)
    {
        return Math.Log(objective, _maxLevel);
    }

    void ApplyTexts()
    {
        spaceText.text = main.itemCtrl.items[(int)kind].size.ToString();
        nameText.text = main.enumCtrl.items[(int)kind].Name();
        maxNumText.text = (MaxEquip == null) ? "∞" : MaxEquip.ToString();
        numText.text = main.itemCtrl.equipNum[(int)kind].ToString() + "/" + (main.itemCtrl.equipNum[(int)kind] + main.itemCtrl.InventoryNum[(int)kind]).ToString();
        levelText.text = level >= maxLevel ? "Lv.Max" : "Lv." + level.ToString() + "/" + maxLevel.ToString();
    }

    public void CheckButton()
    {
        //buy
        if (main.itemCtrl.CanBuy(kind) && Need())
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
        //sell
        if (main.itemCtrl.CanSell_Shop(kind))
        {
            sellButton.interactable = true;
        }
        else
        {
            sellButton.interactable = false;
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
        //自動でコストの文章を生成
        Name_str = main.enumCtrl.items[(int)kind].Name();
        //自動でコストの文章を生成
        //Name_str = main.enumCtrl.items[(int)kind].Name();
        Description_str = main.enumCtrl.items[(int)kind].Description();
        if (haveSource) { Description_str += Description_str == "" ? SourceDetail() : "\n" + SourceDetail(); }
        Max_Str = "Max:" + ((MaxEquip == null) ? "∞" : MaxEquip.ToString());
        Effect_str = ProgressDetail(EffectLists, LevelFactor());
        Cost_str = ProgressDetail(BuyLists);
        Sell_str = ProgressDetail(SellLists);
        LvCost_Str = ProgressDetail(itemPointDeal);
        LvEffect_Str = DetailNextLEffect(level);


        //needが設定されている場合にのみ書き換える。
        //そのため、ない場合は手動でNeed_strを変えることが可能。
        if (need.hasNeeds) { Need_str = need.Detail(); }

        ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
        ChangeTextAdaptive(rarityText.text, popUp.texts[1], popUp.texts[1].gameObject);
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

    //アイテムのNeed(タグ)の数が上限に達していたらtrue
    public bool IsMaxNeed()
    {
        bool IsMax = false;
        foreach (var Tag in sources)
        {
            //無制限に置けるtagだったら続行
            if(LimitNum(Tag) == null) { continue; }
            if(LimitNum(Tag) <= main.itemCtrl.exitSourceNums[(int)Tag])
            {
                IsMax = true;
            }
        }
        return IsMax;
    }


    //数が0ならnullを返し、違えばその数を返す
    public int? LimitNum(NeedKind kind)
    {
        if (main.SR.needLimits[(int)kind] <= 0)
        {
            return null;
        }
        else
        {
            return main.SR.needLimits[(int)kind];
        }
    }
}
