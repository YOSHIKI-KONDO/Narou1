using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// items        ...ショップのITEMの配列。処理は全てこの配列から参照する。
/// equips       ...itemsの数分だけ装備にインスタンティエイト。
/// inventorys   ...itemsの数分だけインベントリにインスタンティエイト。
/// equipNum     ...int[]で、アイテムの個数を格納。
/// inventoryNum ...int[]で、アイテムの個数を格納。
/// R_max, R_regen, A_maxLevel, A_trainRate
///              ...リソースとアビリティに反映させる値。
///
/// equipNumの配列の分だけ R_max などを更新している。
/// </summary>
[DefaultExecutionOrder(-1)]
public class ItemCtrl : BASE {
    public int currentNum_I;                                    //現在使用しているインベントリの数
    public int maxNum_I;                                        //インベントリの最大の数
    public int currentNum_E;                                    //現在使用している装備の数
    public int maxNum_E;                                        //装備の最大の数
    public int RestNum_I { get => maxNum_I - currentNum_I; }    //インベントリの残りの数
    public int RestNum_E { get => maxNum_E - currentNum_E; }    //装備の残りの数
    public ITEM[] items;
    public Item_Inventory[] inventorys;
    public Item_Equip[] equips;
    public Item_LevelUp[] levelUps;
    public Item_Inventory inventoryPre;
    public Item_Equip equipPre;
    public Item_LevelUp levelUpPre;
    public Transform inventoryTra, equipTra, levelUpTra;
    public int[] InventoryNum { get => main.SR.inventoryNum_Item; set => main.SR.inventoryNum_Item = value; }
    public int[] equipNum { get => main.SR.equipNum_Item; set => main.SR.equipNum_Item = value; }
    public int[] exitSourceNums; //sourceがあればtrue

    public double[] R_max;//リソースに加算
    public double[] R_regen;//リソースに加算
    public int[] A_maxLevel;//アビリティに加算
    public double[] A_trainRate;//アビリティに加算

    // Use this for initialization
    void Awake()
    {
        StartBASE();
        items = new ITEM[Enum.GetNames(typeof(ItemKind)).Length];
        inventorys = new Item_Inventory[Enum.GetNames(typeof(ItemKind)).Length];
        equips = new Item_Equip[Enum.GetNames(typeof(ItemKind)).Length];
        levelUps = new Item_LevelUp[Enum.GetNames(typeof(ItemKind)).Length];
        R_max = new double[Enum.GetNames(typeof(ResourceKind)).Length];
        R_regen = new double[Enum.GetNames(typeof(ResourceKind)).Length];
        A_maxLevel = new int[Enum.GetNames(typeof(AbilityKind)).Length];
        A_trainRate = new double[Enum.GetNames(typeof(AbilityKind)).Length];
        exitSourceNums = new int[Enum.GetNames(typeof(NeedKind)).Length];
    }

    // Use this for initialization
    void Start()
    {
        InitializeArrays();

        TestItems();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyActive();
    }

    private void FixedUpdate()
    {
        CalculateItems();
    }

    void TestItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) { Debug.Log((ItemKind)i + " がショップに設置されていません。"); }
        }
    }

    void InitializeArrays()
    {
        for (int i = 0; i < items.Length; i++)
        {
            //inventory
            inventorys[i] = Instantiate(inventoryPre, inventoryTra);
            inventorys[i].StartInventory((ItemKind)i);
            //equip
            equips[i] = Instantiate(equipPre, equipTra);
            equips[i].StartEquip((ItemKind)i);
            //level up
            levelUps[i] = Instantiate(levelUpPre, levelUpTra);
            levelUps[i].StartLevelUp((ItemKind)i);
        }
    }

    void CalculateItems()
    {
        //size計算
        maxNum_I = (int)main.rsc.Max((int)ResourceKind.inventorySpace);
        maxNum_E = (int)main.rsc.Max((int)ResourceKind.equipSpace);
        currentNum_I = 0;
        currentNum_E = 0;
        ArrayToDefault(R_max);
        ArrayToDefault(R_regen);
        ArrayToDefault(A_maxLevel);
        ArrayToDefault(A_trainRate);
        ArrayToDefault(exitSourceNums);
        for (int i = 0; i < items.Length; i++)
        {
            currentNum_I += items[i].size * InventoryNum[i];
            currentNum_E += items[i].size * equipNum[i];
            CalculateItemEffect(items[i].EffectLists, equipNum[i], items[i].kind);//effect計算

            foreach (var src in items[i].sources)
            {
                if (src == NeedKind.nothing) { continue; }
                if (equipNum[i] > 0)
                {
                    exitSourceNums[(int)src] += equipNum[i];
                }
            }
        }
        main.rsc.Value[(int)ResourceKind.inventorySpace] = currentNum_I;
        main.rsc.Value[(int)ResourceKind.equipSpace] = currentNum_E;
    }

    /// <summary>
    /// エフェクトの計算。0にした後加算する書き方をしている。
    /// </summary>
    void CalculateItemEffect(List<Dealing> dealings, int num, ItemKind kind)
    {
        foreach (var deal in dealings)
        {
            if (deal.rscKind is ResourceKind)
            {
                /* リソース */
                if ((deal.paraKind is Dealing.R_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                /* ステータス */
                if ((IsStatus((ResourceKind)deal.rscKind) == true && (Dealing.R_ParaKind)deal.paraKind != Dealing.R_ParaKind.status) ||
                    (IsStatus((ResourceKind)deal.rscKind) == false && (Dealing.R_ParaKind)deal.paraKind == Dealing.R_ParaKind.status))
                {
                    throw new Exception("ステータスの設定が間違っています。(" + kind.ToString() + ")");
                }
                switch ((Dealing.R_ParaKind)deal.paraKind)
                {
                    case Dealing.R_ParaKind.current:
                        throw new Exception("対応していない項目です。");
                    case Dealing.R_ParaKind.max:// * items[(int)(ResourceKind)deal.rscKind].LevelFactor()は長いので新たに関数で宣言してもいいかもしれない
                        R_max[(int)(ResourceKind)deal.rscKind] += deal.Value * items[(int)kind].LevelFactor() * num;//計算
                        break;
                    case Dealing.R_ParaKind.regen:
                        R_regen[(int)(ResourceKind)deal.rscKind] += deal.Value * items[(int)kind].LevelFactor() * num;//計算
                        break;
                    case Dealing.R_ParaKind.status:
                        R_regen[(int)(ResourceKind)deal.rscKind] += deal.Value * items[(int)kind].LevelFactor() * num;//計算
                        break;
                    case Dealing.R_ParaKind.effect:
                        throw new Exception("まだ対応してないお♡");
                    default:
                        throw new Exception("対応していない項目です。");
                }
            }
            else if (deal.rscKind is AbilityKind)
            {
                /* アビリティ */
                if ((deal.paraKind is Dealing.A_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                switch ((Dealing.A_ParaKind)deal.paraKind)
                {
                    case Dealing.A_ParaKind.maxLevel://NOTE:doubleにしたい
                        A_maxLevel[(int)(ResourceKind)deal.rscKind] += (int)deal.Value * (int)items[(int)kind].LevelFactor() * num;//計算
                        break;
                    case Dealing.A_ParaKind.trainRate:
                        A_trainRate[(int)(ResourceKind)deal.rscKind] += deal.Value * items[(int)kind].LevelFactor() * num;//計算
                        break;
                    case Dealing.A_ParaKind.currentExp:
                        throw new Exception("対応していない項目です。");
                    default:
                        throw new Exception("対応していない項目です。");
                }
            }
            else
            {
                throw new Exception("対応していない項目です。");
            }
        }
    }

    void ApplyActive()
    {
        for (int i = 0; i < items.Length; i++)
        {
            //equip
            if(equipNum[i] > 0)
            {
                setActive(equips[i].gameObject);
            }
            else
            {
                setFalse(equips[i].gameObject);
                setFalse(equips[i].popUp.gameObject);
            }

            //inventory
            if(InventoryNum[i] > 0)
            {
                setActive(inventorys[i].gameObject);
            }
            else
            {
                setFalse(inventorys[i].gameObject);
                setFalse(inventorys[i].popUp.gameObject);
            }

            //level up
            if(equipNum[i] > 0 || InventoryNum[i] > 0)
            {
                setActive(levelUps[i].gameObject);
            }
            else
            {
                setFalse(levelUps[i].gameObject);
                setFalse(levelUps[i].popUp.gameObject);
            }
        }
    }


    /* bool関数
       1.CanGetInventory 2.CanGetEquipを用いて
       1.CanBuy 2.CanRemove 3.CanEquipを作っている
       (Sellは別)
       CanGetInventoryをDealingの判定にも使う
         */
    public bool CanGetInventory(ItemKind kind)
    {
        //残りの数
        if ((maxNum_I - currentNum_I) < items[(int)kind].size)
        {
            return false;
        }
        //--最大数に達していないか
        return true;
    }
    private bool CanGetEquip(ItemKind kind)
    {
        //残りの数
        if ((maxNum_E - currentNum_E) < items[(int)kind].size)
        {
            return false;
        }
        //最大数に達していないか
        if (equipNum[(int)kind] >= items[(int)kind].MaxEquip)
        {
            return false;
        }
        return true;
    }

    public bool CanBuy(ItemKind kind)
    {
        //コストが足りるか
        if(CanPurchase(items[(int)kind].BuyLists) == false)
        {
            return false;
        }
        //条件を満たしているか
        if(items[(int)kind].Need() == false)
        {
            return false;
        }
        return CanGetInventory(kind);
    }
    public bool CanRemove(ItemKind kind)
    {
        return CanGetInventory(kind);
    }
    public bool CanEquip(ItemKind kind)
    {
        return CanGetEquip(kind) && items[(int)kind].IsMaxNeed() == false; //上限に達していない必要がある
    }

    public bool CanSell_Inventory(ItemKind kind)
    {
        //lockされていたらfalseを返す
        if (items[(int)kind].lockToggle.isOn) { return false; }

        if (InventoryNum[(int)kind] > 0)
        {
            return true;
        }
        return false;
    }
    public bool CanSell_Equip(ItemKind kind)
    {
        //lockされていたらfalseを返す
        if (items[(int)kind].lockToggle.isOn) { return false; }

        if (equipNum[(int)kind] > 0)
        {
            return true;
        }
        return false;
    }
    public bool CanSell_Shop(ItemKind kind)
    {
        //lockされていたらfalseを返す
        if (items[(int)kind].lockToggle.isOn) { return false; }

        return CanSell_Inventory(kind) || CanSell_Equip(kind);
    }

    public bool CanLevelUp(ItemKind kind)//各レア度で分岐
    {
        double itemPoint = 0;
        //アイテムポイントがあるかどうか
        switch (items[(int)kind].rarity)
        {
            case 1:
                itemPoint = main.rsc.Value[(int)ResourceKind.itemPoint1];
                break;
            case 2:
                itemPoint = main.rsc.Value[(int)ResourceKind.itemPoint2];
                break;
            case 3:
                itemPoint = main.rsc.Value[(int)ResourceKind.itemPoint3];
                break;
            case 4:
                itemPoint = main.rsc.Value[(int)ResourceKind.itemPoint4];
                break;
            case 5:
                itemPoint = main.rsc.Value[(int)ResourceKind.itemPoint5];
                break;
            default:
                break;
        }
        if (itemPoint < 1)
        {
            return false;
        }
        //最大レベルに達していないか
        if(items[(int)kind].level >= items[(int)kind].maxLevel)
        {
            return false;
        }
        return true;
    }

    /* void関数
       1.GetItem 2.ThrowAwayItemを用いて
       1.Buy 2.Sell 3.Equip 4.Removeを作っている
       (Sellは別)*/
    private void GetItem(ItemKind kind)
    {
        InventoryNum[(int)kind]++;
        main.SR.discover_Item[(int)kind] = true; //発見
    }
    public void Buy(ItemKind kind)
    {
        if (CanBuy(kind))
        {
            Calculate(items[(int)kind].BuyLists, false);
            GetItem(kind);
            inventorys[(int)kind].Watched = false;//未読に
            main.SR.watched_InventoryButton = false;//inventoryボタンを未読に
            main.SR.watched_ShopButton = true;//shopボタンを既読に 
        }
    }
    public void Equip(ItemKind kind)
    {
        if (CanEquip(kind))
        {
            equipNum[(int)kind]++;
            InventoryNum[(int)kind]--;
            inventorys[(int)kind].Watched = true;//既読に
        }
    }
    public void Remove(ItemKind kind)
    {
        if (CanRemove(kind))
        {
            equipNum[(int)kind]--;
            GetItem(kind);
        }
    }

    public void Sell_Inventory(ItemKind kind)
    {
        if (CanSell_Inventory(kind))
        {
            Calculate(items[(int)kind].SellLists, false);
            InventoryNum[(int)kind]--;
        }
    }
    public void Sell_Equip(ItemKind kind)
    {
        if (CanSell_Equip(kind))
        {
            Calculate(items[(int)kind].SellLists, false);
            equipNum[(int)kind]--;
        }
    }
    public void Sell_Shop(ItemKind kind)
    {
        if (CanSell_Inventory(kind))
        {
            Calculate(items[(int)kind].SellLists, false);
            InventoryNum[(int)kind]--;
            return;
        }

        if (CanSell_Equip(kind))
        {
            Calculate(items[(int)kind].SellLists, false);
            equipNum[(int)kind]--;
        }
    }

    public void LevelUp(ItemKind kind)//レア度で分岐
    {
        if (CanLevelUp(kind))
        {
            switch (items[(int)kind].rarity)
            {
                case 1:
                    main.rsc.Value[(int)ResourceKind.itemPoint1] -= 1;
                    break;
                case 2:
                    main.rsc.Value[(int)ResourceKind.itemPoint2] -= 1;
                    break;
                case 3:
                    main.rsc.Value[(int)ResourceKind.itemPoint3] -= 1;
                    break;
                case 4:
                    main.rsc.Value[(int)ResourceKind.itemPoint4] -= 1;
                    break;
                case 5:
                    main.rsc.Value[(int)ResourceKind.itemPoint5] -= 1;
                    break;
                default:
                    break;
            }
            items[(int)kind].LevelUp();
        }
    }

    /// <summary>
    /// インベントリーが空いていたら入手してtrueを返す。
    /// 空いていなければfalseを返す。
    /// (Dealingでも一箇所のみ使う)
    /// </summary>
    public bool Drop_Inventory(ItemKind kind)
    {
        if (CanGetInventory(kind))
        {
            GetItem(kind);
            inventorys[(int)kind].Watched = false;//未読に
            main.SR.watched_InventoryButton = false;//inventoryボタンを未読に
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 全てのトグルを同期させる。セーブもする。
    /// </summary>
    public void SynchronizeLock(bool isOn, ItemKind kind)
    {
        items[(int)kind].lockToggle.isOn = isOn;
        equips[(int)kind].lockToggle.isOn = isOn;
        inventorys[(int)kind].lockToggle.isOn = isOn;
        main.SR.locked_Item[(int)kind] = isOn;
    }
}
