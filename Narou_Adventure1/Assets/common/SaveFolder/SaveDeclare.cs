﻿using System.Collections;
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
    [NonSerialized]
    public int num_skill = Enum.GetNames(typeof(SkillKind)).Length;
    [NonSerialized]
    public int num_dungeon =  Enum.GetNames(typeof(DungeonKind)).Length;
    [NonSerialized]
    public int num_ally = Enum.GetNames(typeof(AllyKind)).Length;
    [NonSerialized]
    public int num_need = Enum.GetNames(typeof(NeedKind)).Length;
    [NonSerialized]
    public int num_element = Enum.GetNames(typeof(ElementKind)).Length;

    // Use this for initialization
    void Awake () {
		StartBASE();

        InitializeArray(ref main.SR.released_resource, num_resource);
        InitializeArray(ref main.SR.completed_resource, num_resource);

        InitializeArray(ref main.SR.released_instant, num_instantAction);
        InitializeArray(ref main.SR.completed_instant, num_instantAction);
        InitializeArray(ref main.SR.watched_instant, num_instantAction);

        InitializeArray(ref main.SR.max_resource, num_resource);
        InitializeArray(ref main.SR.current_resource, num_resource);
        InitializeArray(ref main.SR.regen_resource, num_resource);


        InitializeArray(ref main.SR.released_loop, num_loopAction);
        InitializeArray(ref main.SR.completed_loop, num_loopAction);
        InitializeArray(ref main.SR.currentValue_loop, num_loopAction);
        InitializeArray(ref main.SR.paid_loop, num_loopAction);
        InitializeArray(ref main.SR.watched_loop, num_loopAction);

        InitializeArray(ref main.SR.released_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.completed_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.currentValue_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.paid_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.clearNum_upgrade, num_upgradeAction);
        InitializeArray(ref main.SR.watched_upgrade, num_upgradeAction);

        /* Ability */
        InitializeArray(ref main.SR.released_ability, num_ability);
        InitializeArray(ref main.SR.completed_ability, num_ability);
        InitializeArray(ref main.SR.currentValue_ability, num_ability);
        InitializeArray(ref main.SR.paid_ability, num_ability);
        InitializeArray(ref main.SR.levels_ability, num_ability);
        InitializeArray(ref main.SR.maxLevels_ability, num_ability);
        InitializeArray(ref main.SR.unlocked_ability, num_ability);
        InitializeArray(ref main.SR.trainRate_ability, num_ability);
        InitializeArray(ref main.SR.watched_ability, num_ability);

        /* Item */
        InitializeArray(ref main.SR.equipNum_Item, num_item);
        InitializeArray(ref main.SR.inventoryNum_Item, num_item);
        InitializeArray(ref main.SR.released_Item, num_item);
        InitializeArray(ref main.SR.completed_Item, num_item);
        InitializeArray(ref main.SR.watched_Shop, num_item);
        InitializeArray(ref main.SR.watched_Inventory, num_item);
        InitializeArray(ref main.SR.level_Item, num_item);
        InitializeArray(ref main.SR.locked_Item, num_item);

        /* Need */
        InitializeArray(ref main.SR.needLimits, num_need);

        /* Skill */
        InitializeArray(ref main.SR.released_Skill, num_skill);
        InitializeArray(ref main.SR.completed_Skill, num_skill);
        InitializeArray(ref main.SR.learnt_Skill, num_skill);
        InitializeArray(ref main.SR.slotKinds, main.battleCtrl.ROW_SLOT * main.battleCtrl.COLUMN_SLOT);
        InitializeArray(ref main.SR.watched_Skill, num_skill);

        /* Dungeon */
        InitializeArray(ref main.SR.released_Dungeon, num_dungeon);
        InitializeArray(ref main.SR.completed_Dungeon, num_dungeon);
        InitializeArray(ref main.SR.currentFloor_Dungeon, num_dungeon);
        InitializeArray(ref main.SR.clearNum_Dungeon, num_dungeon);
        InitializeArray(ref main.SR.watched_Dungeon, num_dungeon);
        InitializeArray(ref main.SR.maxFloor_Dungeon, num_dungeon);

        /* Npc */
        InitializeArray(ref main.SR.levels_Ally, num_ally);
        InitializeArray(ref main.SR.exps_Ally, num_ally);

        /* 要素 */
        InitializeArray(ref main.SR.released_element, num_element);
        InitializeArray(ref main.SR.completed_element, num_element);
    }

	// Use this for initialization
	void Start () {

	}
}
