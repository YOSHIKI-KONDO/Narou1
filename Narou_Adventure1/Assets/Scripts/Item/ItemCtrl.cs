﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

[DefaultExecutionOrder(-1)]
public class ItemCtrl : BASE {
    public int currentNum_I;
    public int maxNum_I;
    public int currentNum_E;
    public int maxNum_E;
    public ITEM[] items;
    public Item_Inventory[] inventorys;
    public Item_Equip[] equips;
    public Item_Inventory inventoryPre;
    public Item_Equip equipPre;
    public Transform inventoryTra, equipTra;
    public int[] InventoryNum { get => main.SR.inventoryNum_Item; set => main.SR.inventoryNum_Item = value; }
    public int[] equipNum { get => main.SR.equipNum_Item; set => main.SR.equipNum_Item = value; }
    public bool[] exitSources; //sourceがあればtrue

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
        R_max = new double[Enum.GetNames(typeof(ResourceKind)).Length];
        R_regen = new double[Enum.GetNames(typeof(ResourceKind)).Length];
        A_maxLevel = new int[Enum.GetNames(typeof(AbilityKind)).Length];
        A_trainRate = new double[Enum.GetNames(typeof(AbilityKind)).Length];
        exitSources = new bool[Enum.GetNames(typeof(NeedKind)).Length];
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
        ArrayToDefault(exitSources);
        for (int i = 0; i < items.Length; i++)
        {
            currentNum_I += items[i].size * InventoryNum[i];
            currentNum_E += items[i].size * equipNum[i];
            CalculateItemEffect(items[i].EffectLists, equipNum[i]);//effect計算

            foreach (var src in items[i].sources)
            {
                if (src == NeedKind.nothing) { continue; }
                if (equipNum[i] > 0)
                {
                    exitSources[(int)src] = true;
                }
            }
        }
        main.rsc.Value[(int)ResourceKind.inventorySpace] = currentNum_I;
        main.rsc.Value[(int)ResourceKind.equipSpace] = currentNum_E;
    }

    /// <summary>
    /// エフェクトの計算。0にした後加算する書き方をしている。
    /// </summary>
    void CalculateItemEffect(List<Dealing> dealings, int num)
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
                switch ((Dealing.R_ParaKind)deal.paraKind)
                {
                    case Dealing.R_ParaKind.current:
                        throw new Exception("対応していない項目です。");
                    case Dealing.R_ParaKind.max:
                        R_max[(int)(ResourceKind)deal.rscKind] += deal.Value * num;//計算
                        break;
                    case Dealing.R_ParaKind.regen:
                        R_regen[(int)(ResourceKind)deal.rscKind] += deal.Value * num;//計算
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
                    case Dealing.A_ParaKind.maxLevel:
                        A_maxLevel[(int)(ResourceKind)deal.rscKind] += (int)deal.Value * num;//計算
                        break;
                    case Dealing.A_ParaKind.trainRate:
                        A_trainRate[(int)(ResourceKind)deal.rscKind] += deal.Value * num;//計算
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

    void ArrayToDefault<T>(T[] ary)
        where T:struct
    {
        for (int i = 0; i < ary.Length; i++)
        {
            ary[i] = default(T);
        }
    }

    void ApplyActive()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(equipNum[i] > 0)
            {
                setActive(equips[i].gameObject);
            }
            else
            {
                setFalse(equips[i].gameObject);
                setFalse(equips[i].popUp.gameObject);
            }

            if(InventoryNum[i] > 0)
            {
                setActive(inventorys[i].gameObject);
            }
            else
            {
                setFalse(inventorys[i].gameObject);
                setFalse(inventorys[i].popUp.gameObject);
            }
        }
    }


    /* bool関数
       1.CanGetInventory 2.CanGetEquipを用いて
       1.CanBuy 2.CanRemove 3.CanEquipを作っている
       (Sellは別)*/
    private bool CanGetInventory(ItemKind kind)
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
        return CanGetEquip(kind);
    }

    public bool CanSell_Inventory(ItemKind kind)
    {
        if (InventoryNum[(int)kind] > 0)
        {
            return true;
        }
        return false;
    }
    public bool CanSell_Equip(ItemKind kind)
    {
        if (equipNum[(int)kind] > 0)
        {
            return true;
        }
        return false;
    }
    public bool CanSell_Shop(ItemKind kind)
    {
        return CanSell_Inventory(kind) || CanSell_Equip(kind);
    }

    /* void関数
       1.GetItem 2.ThrowAwayItemを用いて
       1.Buy 2.Sell 3.Equip 4.Removeを作っている
       (Sellは別)*/
    private void GetItem(ItemKind kind)
    {
        InventoryNum[(int)kind]++;
    }
    public void Buy(ItemKind kind)
    {
        if (CanBuy(kind))
        {
            Calculate(items[(int)kind].BuyLists, false);
            GetItem(kind);
        }
    }
    public void Equip(ItemKind kind)
    {
        if (CanEquip(kind))
        {
            equipNum[(int)kind]++;
            InventoryNum[(int)kind]--;
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
}
