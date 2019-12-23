using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// ショップに全て置く
/// </summary>
public class ITEM : BASE, INeed
{
    public ItemKind kind;
    public List<NeedKind> sources = new List<NeedKind>();
    public int size;
    public int? MaxEquip;
    public List<Dealing> BuyLists = new List<Dealing>();
    public List<Dealing> SellLists = new List<Dealing>();
    public List<Dealing> EffectLists = new List<Dealing>();
    public Button buyButton, sellButton, levelUpButton;
    public Text spaceText, nameText, numText;
    Text text;
    PopUp popUp;
    ReleaseFunction release;
    public NeedFunciton need;
    public bool Watched { get => main.SR.watched_Item[(int)kind]; set => main.SR.watched_Item[(int)kind] = value; }
    public string Name_str, Description_str, Max_Str, Need_str, Effect_str, Cost_str, Sell_str;

    public int[] InventoryNum { get => main.SR.inventoryNum_Item; set => main.SR.inventoryNum_Item = value; }
    public int[] equipNum { get => main.SR.equipNum_Item; set => main.SR.equipNum_Item = value; }
    public int level { get => main.SR.level_Item[(int)kind]; set => main.SR.level_Item[(int)kind] = value; }
    public int maxLevel = 30;
    public double level_power;
    public double LevelFactor() { return levelFactor_cash ?? CalculateLFactor(); }
    double? levelFactor_cash;//レベルアップしたときにnullを入れる
    public void LevelUp() { level++; levelFactor_cash = null; }
    double CalculateLFactor()
    {
        levelFactor_cash = Math.Pow(level, level_power);
        if(level >= maxLevel)
        {
            levelFactor_cash += 0.5;//50%する
        }
        return (double)levelFactor_cash;
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
    public void AwakeItem (ItemKind kind, int size, int? max = null) {
		StartBASE();
        text = GetComponentInChildren<Text>();
        this.kind = kind;
        this.size = size;
        this.MaxEquip = max;
        main.itemCtrl.items[(int)kind] = this;
        popUp = main.itemPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_Item[(int)kind], x), x => Sync(ref main.SR.completed_Item[(int)kind], x), x => Requires());
        need = gameObject.AddComponent<NeedFunciton>();

        buyButton = gameObject.GetComponentsInChildren<Button>()[0];
        sellButton = gameObject.GetComponentsInChildren<Button>()[1];
        levelUpButton = gameObject.GetComponentsInChildren<Button>()[2];
        spaceText = GetComponentsInChildren<Text>()[0];
        nameText = GetComponentsInChildren<Text>()[1];
        numText = GetComponentsInChildren<Text>()[2];
        buyButton.onClick.AddListener(() => main.itemCtrl.Buy(kind));
        buyButton.onClick.AddListener(() => { Watched = true; }); //watchedをtrueにする処理をbuybuttonに追加
        sellButton.onClick.AddListener(() => main.itemCtrl.Sell_Shop(kind));
        levelUpButton.onClick.AddListener(() => main.itemCtrl.LevelUp(kind));
    }

    // Use this for initialization
    public void StartItem () {
        if (level <= 0) { level = 1; }//レベルは0以上でなければならない
        level_power = CalculatePower(30, 2);
    }

    // Update is called once per frame
    public void UpdateItem () {
        CheckButton();
        ApplyTexts();
        ApplyPopUp();
    }

    // Update is called once per frame
    public void FixedUpdateItem()
    {
        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
        }
        text.text = Name_str;
    }

    double CalculatePower(int _maxLevel, double objective)
    {
        return Math.Log(objective, _maxLevel);
    }

    void ApplyTexts()
    {
        spaceText.text = main.itemCtrl.items[(int)kind].size.ToString();
        nameText.text = main.enumCtrl.items[(int)kind].Name();
        numText.text = main.itemCtrl.equipNum[(int)kind].ToString() + "/" + (main.itemCtrl.equipNum[(int)kind] + main.itemCtrl.InventoryNum[(int)kind]).ToString();
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
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            //Name_str = main.enumCtrl.items[(int)kind].Name();
            Description_str = main.enumCtrl.items[(int)kind].Description();
            if (haveSource) { Description_str += Description_str == "" ? SourceDetail() : "\n" + SourceDetail(); }
            Max_Str = "Max:" + ((MaxEquip == null) ? "∞" : MaxEquip.ToString());
            Effect_str = ProgressDetail(EffectLists, LevelFactor());
            Cost_str = ProgressDetail(BuyLists);
            Sell_str = ProgressDetail(SellLists);
            Cost_str = ProgressDetail(BuyLists);

            //needが設定されている場合にのみ書き換える。
            //そのため、ない場合は手動でNeed_strを変えることが可能。
            if (need.hasNeeds) { Need_str = need.Detail(); }

            ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
            ChangeTextAdaptive(Description_str, popUp.texts[1], popUp.texts[1].gameObject);
            ChangeTextAdaptive(Max_Str, popUp.texts[2], popUp.texts[2].gameObject);
            ChangeTextAdaptive(Need_str, popUp.texts[4], popUp.texts[3].gameObject, popUp.texts[4].gameObject);
            ChangeTextAdaptive(Effect_str, popUp.texts[6], popUp.texts[5].gameObject, popUp.texts[6].gameObject);
            ChangeTextAdaptive(Cost_str, popUp.texts[8], popUp.texts[7].gameObject, popUp.texts[8].gameObject);
            ChangeTextAdaptive(Sell_str, popUp.texts[10], popUp.texts[9].gameObject, popUp.texts[10].gameObject);
        }
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
