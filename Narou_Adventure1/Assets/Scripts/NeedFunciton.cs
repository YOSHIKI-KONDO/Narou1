using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class NeedFunciton : BASE {
    List<ItemKind> itemKinds;
    List<NeedKind> sourceKinds;
    public bool hasNeeds;

    public void AddItemNeed(ItemKind kind)
    {
        if (itemKinds == null) { itemKinds = new List<ItemKind>(); }
        if (sourceKinds == null) { sourceKinds = new List<NeedKind>(); }
        hasNeeds = true;
        itemKinds.Add(kind);
    }

    public void AddSourceNeed(NeedKind kind)
    {
        if (itemKinds == null) { itemKinds = new List<ItemKind>(); }
        if (sourceKinds == null) { sourceKinds = new List<NeedKind>(); }
        hasNeeds = true;
        sourceKinds.Add(kind);
    }

    public bool TemplateNeed()
    {
        bool all_true = true;
        foreach(var itemKind in itemKinds)
        {
            if(main.itemCtrl.equipNum[(int)itemKind] <= 0)
            {
                all_true = false;
                break;
            }
        }
        foreach (var sourceKind in sourceKinds)
        {
            if (main.itemCtrl.exitSources[(int)sourceKind] == false)
            {
                all_true = false;
                break;
            }
        }
        return all_true;
    }

    public string Detail()
    {
        string item_str = ItemDetail();
        string src_str = SourceDetail();
        if(item_str != "" && src_str != "")
        {
            return item_str + "\n" + src_str;
        }
        else
        {
            return item_str + src_str;
        }
    }

    string ItemDetail()
    {
        string sum_str = "";
        foreach (var itemKind in itemKinds)
        {
            if (sum_str != "") { sum_str += ", "; }
            sum_str += main.enumCtrl.items[(int)itemKind].Name();
        }
        return sum_str;
    }

    string SourceDetail()
    {
        string sum_str = "";
        foreach (var sourceKind in sourceKinds)
        {
            if (sum_str != "") { sum_str += ", "; }
            sum_str += main.enumCtrl.needs[(int)sourceKind].Name();
        }
        return sum_str;
    }

    // Use this for initialization
    void Awake () {
		StartBASE();
	}
}
