using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static UsefulStatic;
using MainAction;

/// <summary>
/// 主にsaveしたい配列の初期化を行うクラス
/// InitializeArray(ref main.SR.hoge, サイズ);
/// のようにして初期化する。アップデートなどで途中から変更することも可能。
/// 初期化はAwake()のAwakeBASE();のあとに書くことを推奨。
/// </summary>
public class SaveDeclare : BASE {
    [NonSerialized]
    public int num_resource = Enum.GetNames(typeof(ResourceKind)).Length;
    [NonSerialized]
    public int num_loopAction = Enum.GetNames(typeof(ActionEnum.Loop)).Length;
    [NonSerialized]
    public int num_upgradeAction = Enum.GetNames(typeof(ActionEnum.Upgrade)).Length;
    [NonSerialized]
    public int num_instantAction = Enum.GetNames(typeof(ActionEnum.Instant)).Length;
    [NonSerialized]
    public int num_ability = Enum.GetNames(typeof(AbilityKind)).Length;
    [NonSerialized]
    public int num_item = Enum.GetNames(typeof(ItemKind)).Length;

    // Use this for initialization
    void Awake () {
		StartBASE();

        InitializeArray(ref main.SR.released_resource, num_resource);
        InitializeArray(ref main.SR.completed_resource, num_resource);

        InitializeArray(ref main.SR.released_instant, num_instantAction);
        InitializeArray(ref main.SR.completed_instant, num_instantAction);

        InitializeArray(ref main.SR.max_resource, num_resource);
        InitializeArray(ref main.SR.current_resource, num_resource);
        InitializeArray(ref main.SR.regen_resource, num_resource);


        InitializeArray(ref main.SR.released_loop, num_loopAction);
        InitializeArray(ref main.SR.completed_loop, num_loopAction);
        InitializeArray(ref main.SR.currentValue_loop, num_loopAction);
        InitializeArray(ref main.SR.paid_loop, num_loopAction);

        InitializeArray(ref main.SR.released_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.completed_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.currentValue_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.paid_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.clearNum_upgrade, num_upgradeAction);

        /* Ability */
        InitializeArray(ref main.SR.released_ability, num_ability);
        InitializeArray(ref main.SR.completed_ability, num_ability);
        InitializeArray(ref main.SR.currentValue_ability, num_ability);
        InitializeArray(ref main.SR.paid_ability, num_ability);
        InitializeArray(ref main.SR.levels_ability, num_ability);
        InitializeArray(ref main.SR.maxLevels_ability, num_ability);
        InitializeArray(ref main.SR.unlocked_ability, num_ability);
        InitializeArray(ref main.SR.trainRate_ability, num_ability);

        /* Item */
        InitializeArray(ref main.SR.equipNum_Item, num_item);
        InitializeArray(ref main.SR.inventoryNum_Item, num_item);
        InitializeArray(ref main.SR.released_Item, num_item);
        InitializeArray(ref main.SR.completed_Item, num_item);
    }

	// Use this for initialization
	void Start () {

	}
}
