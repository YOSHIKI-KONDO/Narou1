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
    public Button buyButton, sellButton;
    public Text spaceText, nameText, numText;
    Text text;
    PopUp popUp;
    ReleaseFunction release;
    public NeedFunciton need;
    public string Name_str, Description_str, Max_Str, Need_str, Effect_str, Cost_str, Sell_str;

    public int[] InventoryNum { get => main.SR.inventoryNum_Item; set => main.SR.inventoryNum_Item = value; }
    public int[] equipNum { get => main.SR.equipNum_Item; set => main.SR.equipNum_Item = value; }

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
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
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
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_Item[(int)kind], x), x => Sync(ref main.SR.completed_Item[(int)kind], x), x => Requires());
        need = gameObject.AddComponent<NeedFunciton>();

        buyButton = gameObject.GetComponentsInChildren<Button>()[0];
        sellButton = gameObject.GetComponentsInChildren<Button>()[1];
        spaceText = GetComponentsInChildren<Text>()[0];
        nameText = GetComponentsInChildren<Text>()[1];
        numText = GetComponentsInChildren<Text>()[2];
        buyButton.onClick.AddListener(() => main.itemCtrl.Buy(kind));
        sellButton.onClick.AddListener(() => main.itemCtrl.Sell_Shop(kind));
    }

    // Use this for initialization
    public void StartItem () {
		
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
            Effect_str = ProgressDetail(EffectLists);
            Cost_str = ProgressDetail(BuyLists);
            Sell_str = ProgressDetail(SellLists);
            Cost_str = ProgressDetail(BuyLists);

            //needが設定されている場合にのみ書き換える。
            //そのため、ない場合は手動でNeed_strを変えることが可能。
            if (need.hasNeeds) { Need_str = need.Detail(); }

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
