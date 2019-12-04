using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using MainAction;

public class EnumCtrl : BASE {

    public class template
    {
        public Enum kind;
        string name_us;
        string name_jp;
        string des_us;
        string des_jp;

        public string Name()
        {
            return Main.isJapanese ? name_jp : name_us;
        }

        public string Description()
        {
            return Main.isJapanese ? des_jp : des_us;
        }

        public template(Enum kind, string name_us, string name_jp, string des_us, string des_jp)
        {
            this.kind = kind;
            this.name_us = name_us;
            this.name_jp = name_jp;
            this.des_us = des_us;
            this.des_jp = des_jp;
        }
    }

    public List<template> resources = new List<template>();
    public List<template> instantActions = new List<template>();
    public List<template> loopActions = new List<template>();
    public List<template> upgradeActions = new List<template>();
    public List<template> abilitys = new List<template>();
    public List<template> items = new List<template>();

    // Use this for initialization
    void Awake () {
		StartBASE();

        /* リソース */
        resources.Add(new template(ResourceKind.nothing, "nothing", "nothing", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.focus, "focus", "focus", "", ""));
        resources.Add(new template(ResourceKind.stamina, "Stamina", "スタミナ", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.mana, "mana", "マナ", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.hp, "hp", "HP", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.fire, "fire", "ファイヤ", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.water, "water", "ウォーター", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.light, "light", "ライト", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.shadow, "shadow", "シャドウ", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.gems, "gems", "ジェム", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.scrolls, "scrolls", "スクロール", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.codices, "codices", "コーデック", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.looms, "looms", "魔術書", "use for everything", "あらゆる行動に使います"));

        /* インスタントアクション */
        instantActions.Add(new template(ActionEnum.Instant.nothing, "nothing", "nothing", "nothing", "nothing"));
        instantActions.Add(new template(ActionEnum.Instant.rest, "rest", "休憩", "", ""));
        instantActions.Add(new template(ActionEnum.Instant.study, "study", "勉強", "", ""));
        /* ループアクション */
        loopActions.Add(new template(ActionEnum.Loop.nothing, "nothing", "nothing", "nothing", "nothing"));
        loopActions.Add(new template(ActionEnum.Loop.eatChildren, "Eat Children", "子供を食べる", "Are you insane!?", "正気じゃない"));
        loopActions.Add(new template(ActionEnum.Loop.Murder, "Murder", "人を殺す", "You don't have any consciences?", "一切の良心を感じない"));
        /* アップグレードアクション */
        upgradeActions.Add(new template(ActionEnum.Upgrade.nothing, "nothing", "nothing", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.SlainMaster, "Slain Master", "師匠を殺す", "Return evil of good", "恩を仇で返す"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.CrystalMind, "Crystal Mind", "クリスタルのこころ", "", ""));

        /* アビリティ */
        abilitys.Add(new template(AbilityKind.nothing, "nothing", "nothing", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.lore, "Lore", "伝承", "none", "none"));
        abilitys.Add(new template(AbilityKind.reanimate, "Reanimate", "死者蘇生", "none", "none"));

        /* アイテム */
        items.Add(new template(ItemKind.nothing, "nothing", "nothing", "nothing", "nothing"));
        items.Add(new template(ItemKind.creepingVine, "Creeping Vine", "つたうつた", "generates herbs automatically", "じどうで はーぶが 手に入る。"));
        items.Add(new template(ItemKind.c, "C", "C言語", "", ""));
        items.Add(new template(ItemKind.java, "Java", "Java", "", ""));
        items.Add(new template(ItemKind.python, "Python", "Python", "", ""));


        //テスト
        TestAll();
    }

    public void TestAll()
    {
        TestOneList<ResourceKind>(resources);
        TestOneList<ActionEnum.Instant>(instantActions);
        TestOneList<ActionEnum.Loop>(loopActions);
        TestOneList<ActionEnum.Upgrade>(upgradeActions);
    }

    public void TestOneList<T>(List<template> templates)
        where T : Enum
    {
        if (Enum.GetNames(typeof(T)).Length != templates.Count)
        {
            throw new Exception("enumとenumの詳細のリストの要素の数が違います。");
        }
        for (int i = 0; i < templates.Count; i++)
        {
            if (Enum.GetNames(typeof(T))[i] != templates[i].kind.ToString()) { throw new Exception("enumとenumの詳細のリストの順番が違います。"); }
        }
    }
}
