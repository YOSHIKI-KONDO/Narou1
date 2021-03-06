﻿using System.Collections;
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
    public List<template> needs = new List<template>();

    // Use this for initialization
    void Awake () {
		StartBASE();

        /* リソース */
        resources.Add(new template(ResourceKind.nothing, "nothing", "nothing", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.focus, "focus", "フォーカス", "", ""));
        resources.Add(new template(ResourceKind.equipSpace, "Equip", "装備", "", ""));
        resources.Add(new template(ResourceKind.inventorySpace, "Inventory", "インベントリ", "", ""));
            //ステータスリソース
        resources.Add(new template(ResourceKind.stamina, "Stamina", "スタミナ", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.hp, "HP", "HP", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.mp, "MP", "MP", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.fire, "Fire", "火", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.water, "Water", "水", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.wind, "Wind", "風", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.earth, "Earth", "土", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.thunder, "Thunder", "雷", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.ice, "Ice", "氷", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.light, "Light", "光", "use for everything", "あらゆる行動に使います"));
        resources.Add(new template(ResourceKind.dark, "Dark", "闇", "use for everything", "あらゆる行動に使います"));
            //物資リソース
        resources.Add(new template(ResourceKind.gold, "Gold", "Gold", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.research, "Research", "リサーチ", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.paper, "Paper", "用紙", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.book, "Book", "本", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.dictionary, "Dictionary", "辞典", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.herb, "Herb", "ハーブ", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.medicine, "Medicine", "薬", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.potion, "Potion", "ポーション", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.wood, "Wood", "木", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.firewood, "Firewood", "薪", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.charcoal, "Charcoal", "木炭", "nothing", "nothing"));
            //食べ物
        resources.Add(new template(ResourceKind.wheat, "Wheat", "小麦", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.bread, "Bread", "パン", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.fish, "Fish", "魚", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.anchovy_sandwich, "Anchovy Sandwich", "アンチョビサンド", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.filet_o_fish, "Filet-O-Fish", "フィレオフィッシュ", "nothing", "nothing"));
            //石
        resources.Add(new template(ResourceKind.stone, "Stone", "石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.magi_stone, "Magi Stone", "魔石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.fire_stone, "Fire Stone", "火魔石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.water_stone, "Water Stone", "水魔石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.wind_stone, "Wind Stone", "風魔石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.earth_stone, "Earth Stone", "土魔石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.thunder_stone, "Thunder Stone", "雷魔石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.ice_stone, "Ice Stone", "氷魔石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.light_stone, "Light Stone", "光魔石", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.dark_stone, "Dark Stone", "闇魔石", "nothing", "nothing"));
             //鉄
        resources.Add(new template(ResourceKind.iron, "Iron", "鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.steel, "Steel", "鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.magi_steel, "Magi Steel", "魔鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.fire_steel, "Fire Steel", "魔鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.water_steel, "Water Steel", "魔鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.wind_steel, "Wind Steel", "魔鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.earth_steel, "Earth Steel", "魔鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.thunder_steel, "Thunder Steel", "魔鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.ice_steel, "Ice Steel", "魔鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.light_steel, "Light Steel", "魔鋼鉄", "nothing", "nothing"));
        resources.Add(new template(ResourceKind.dark_steel, "Dark Steel", "魔鋼鉄", "nothing", "nothing"));
            //上位鉱石
        resources.Add(new template(ResourceKind.mithril, "Mithril", "ミスリル", "nothing", "高価な鉱石。とても硬く、魔力を通す。"));
        resources.Add(new template(ResourceKind.adamantite, "Adamantite", "アダマンタイト", "nothing", "高価な鉱石。非常に硬く、魔力を通さない。"));
        resources.Add(new template(ResourceKind.orichalcum, "Orichalcum", "オリハルコン", "nothing", "天下最強の鉱石。"));

          //アビリティポイント
        resources.Add(new template(ResourceKind.ap, "AP", "AP", "nothing", "nothing"));

        /* インスタントアクション */
        instantActions.Add(new template(ActionEnum.Instant.nothing, "nothing", "nothing", "nothing", "nothing"));
          //フェーズ１（入学前）
        instantActions.Add(new template(ActionEnum.Instant.weeding, "Weeding", "草むしり", "nothing", "アルバイト。たまにハーブが生えている。"));
        instantActions.Add(new template(ActionEnum.Instant.eat_anchovy_sandwich, "Eat Anchovy Sandwich", "アンチョビサンドを食べる", "nothing", "母の味。"));
        instantActions.Add(new template(ActionEnum.Instant.writing_paper, "Writing Paper", "執筆", "nothing", "nothing"));
        instantActions.Add(new template(ActionEnum.Instant.bind_a_book, "Bind a Book", "本を綴じる", "nothing", "nothing"));
        /* ループアクション */
        loopActions.Add(new template(ActionEnum.Loop.nothing, "nothing", "nothing", "nothing", "nothing"));
            //常用
        loopActions.Add(new template(ActionEnum.Loop.rest, "Rest", "休憩", "nothing", "nothing"));
        loopActions.Add(new template(ActionEnum.Loop.study, "Study", "勉強", "nothing", "nothing"));
          //フェーズ１（入学前）
        loopActions.Add(new template(ActionEnum.Loop.chores, "Chores", "雑用", "nothing", "nothing"));
          //フェーズ２（学校）
        loopActions.Add(new template(ActionEnum.Loop.manual_labor, "Manual Labor", "肉体労働", "nothing", "nothing"));
        loopActions.Add(new template(ActionEnum.Loop.desk_work, "Desk Work", "デスクワーク", "nothing", "nothing"));
        loopActions.Add(new template(ActionEnum.Loop.service_trade, "Service Trade", "サービス業", "nothing", "nothing"));
        /* アップグレードアクション */
        upgradeActions.Add(new template(ActionEnum.Upgrade.nothing, "nothing", "nothing", "nothing", "nothing"));
          //フェーズ１（入学前）
            //父の道場
        upgradeActions.Add(new template(ActionEnum.Upgrade.training, "Training", "トレーニング", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.sword_practice, "Sword Practice", "剣の稽古", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.spear_practice, "Spear Practice", "槍の稽古", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.rod_practice, "Rod Practice", "棒の稽古", "nothing", "nothing"));
            //母の書斎                             
        upgradeActions.Add(new template(ActionEnum.Upgrade.mental_training, "Mental Training", "メンタルトレーニング", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.read_fire_spellbook, "Read Fire Spellbook", "火の魔導書", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.read_water_spellbook, "Read Water Spellbookg", "水の魔導書", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.read_wind_spellbook, "Read Wind Spellbook", "風の魔導書", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.read_earth_spellbook, "Read Earth Spellbook", "土の魔導書", "nothing", "nothing"));
            //村の広場                              
        upgradeActions.Add(new template(ActionEnum.Upgrade.play_with_cat, "Play With Cat", "猫と遊ぶ", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.learn_use_tools, "Learn Use Tools", "道具の使い方を教わる", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.succession_life_magic, "Succession Life Magic", "生活魔法継承", "nothing", "nothing"));

        upgradeActions.Add(new template(ActionEnum.Upgrade.buy_wallet, "Buy Wallet", "財布を買う", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.study_in_church, "Study in Church", "教会で勉強", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.buy_bag, "Buy Bag", "鞄を買う", "nothing", "nothing"));
        //進学
        upgradeActions.Add(new template(ActionEnum.Upgrade.warrior_school, "Warrior School", "戦士学校", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.sorcerer_school, "Sorcerer School", "魔法学校", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.tamer_school, "Tamer School", "テイマー学校", "nothing", "nothing"));

          //フェーズ２（学校）
        
            //戦士
        upgradeActions.Add(new template(ActionEnum.Upgrade.apprentice_warrior, "Apprentice Warrior", "戦士見習い", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.warrior, "Warrior", "ウォーリアー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.soldier, "Soldierg", "ソルジャー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.mercenary, "Mercenary", "マーセナリー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.trooper, "Trooper", "トルーパー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.knight, "Knight", "ナイト", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.slayer, "Slayer", "スレイヤー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.fighter, "Fighter", "ファイター", "nothing", "nothing"));
            //魔導士
        upgradeActions.Add(new template(ActionEnum.Upgrade.apprentice_sorcerer, "Apprentice Sorcerer", "魔導士見習い", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.sorcerer, "Sorcerer", "ソーサラー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.wizard, "Wizard", "ウィザード", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.warlock, "Warlock", "ウォーロック", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.high_sorcerer, "High Sorcerer", "ハイソーサラー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.priest, "Priest", "プリースト", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.conjurer, "Conjurer", "コンジュラー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.black_mage, "Black Mage", "ブラックメイジ", "nothing", "nothing"));
            //テイマー
        upgradeActions.Add(new template(ActionEnum.Upgrade.apprentice_tamer, "Apprentice Tamer", "テイマー見習い", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.tamer, "Tamer", "テイマー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.beast_tamer, "Beast Tamer", "ビーストテイマー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.elementaler, "Elementaler", "エレメンタラー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.monster_tamer, "Monster Tamer", "モンスターテイマー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.summoner, "Summoner", "サモナー", "nothing", "nothing"));
            //生産職
        upgradeActions.Add(new template(ActionEnum.Upgrade.crafter, "Crafter", "クラフター", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.iron_crafter, "Iron Crafter", "アイアンクラフター", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.magicrafter, "MagiCrafter", "マギクラフター", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.weapon_maker, "Weapon Maker", "ウェポンメイカー", "nothing", "nothing"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.alchemist, "Alchemist", "アルケミスト", "nothing", "nothing"));
        /* アビリティ */
        abilitys.Add(new template(AbilityKind.nothing, "nothing", "nothing", "nothing", "nothing"));
            //戦士基本職初級
        abilitys.Add(new template(AbilityKind.beginner_swordmanship, "Beginner Swordmanship", "初級剣術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.beginner_spearmanship, "Beginner Spearmanship", "初級槍術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.beginner_bojutsu, "Beginner Bojutsu", "初級棒術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.beginner_battleaxe, "Beginner Battleaxe", "初級斧術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.beginner_shieldmanship, "Beginner Shieldmanship", "初級盾術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.beginner_martialart, "Beginner MartialArt", "初級体術", "nothing", "nothing"));
            //戦士基本職中級
        abilitys.Add(new template(AbilityKind.intermediate_swordmanship, "Intermediate Swordmanship", "中級剣術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_spearmanship, "Intermediate Spearmanship", "中級槍術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_bojutsu, "Intermediate Bojutsu", "中級棒術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_battleaxe, "Intermediate Battleaxe", "中級斧術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_shieldmanship, "Intermediate Shieldmanship", "中級盾術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_martialart, "Intermediate MartialArt", "中級体術", "nothing", "nothing"));
            //戦士基本職上級
        abilitys.Add(new template(AbilityKind.advanced_swordmanship, "Advanced Swordmanship", "上級剣術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_spearmanship, "Advanced Spearmanship", "上級槍術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_bojutsu, "Advanced Bojutsu", "上級棒術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_battleaxe, "Advanced Battleaxe", "上級斧術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_shieldmanship, "Advanced Shieldmanship", "上級盾術", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_martialart, "Advanced MartialArt", "上級体術", "nothing", "nothing"));

            //魔術士基本職初級
        abilitys.Add(new template(AbilityKind.primary_fire_magic, "Primary Fire Magic", "初級火魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.primary_water_magic, "Primary Water Magic", "初級水魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.primary_wind_magic, "Primary Wind Magic", "初級風魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.primary_earth_magic, "Primary Earth Magic", "初級土魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.primary_thunder_magic, "Primary Thunder Magic", "初級雷魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.primary_ice_magic, "Primary Ice Magic", "初級氷魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.primary_light_magic, "Primary Light Magic", "初級光魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.primary_dark_magic, "Primary Dark Magic", "初級闇魔法", "nothing", "nothing"));
            //魔術士基本職中級
        abilitys.Add(new template(AbilityKind.intermediate_fire_magic, "Intermediate Fire Magic", "中級火魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_water_magic, "Intermediate Water Magic", "中級水魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_wind_magic, "Intermediate Wind Magic", "中級風魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_earth_magic, "Intermediate Earth Magic", "中級土魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_thunder_magic, "Intermediate Thunder Magic", "中級雷魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_ice_magic, "Intermediate Ice Magic", "中級氷魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_light_magic, "Intermediate Light Magic", "中級光魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.intermediate_dark_magic, "Intermediate Dark Magic", "中級闇魔法", "nothing", "nothing"));
            //魔術士基本職上級
        abilitys.Add(new template(AbilityKind.advanced_fire_magic, "Advanced Fire Magic", "上級火魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_water_magic, "Advanced Water Magic", "上級水魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_wind_magic, "Advanced Wind Magic", "上級風魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_earth_magic, "Advanced Earth Magic", "上級土魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_thunder_magic, "Advanced Thunder Magic", "上級雷魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_ice_magic, "Advanced Ice Magic", "上級氷魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_light_magic, "Advanced Light Magic", "上級光魔法", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.advanced_dark_magic, "Advanced Dark Magic", "上級闇魔法", "nothing", "nothing"));

            //使役者基本職初級
        abilitys.Add(new template(AbilityKind.animal_handling, "Animal Handling", "動物使役", "nothing", "nothing"));
            //使役者基本職中級
        abilitys.Add(new template(AbilityKind.beast_tamer, "Beast Tamer", "獣使い", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.insect_handling, "Insect Handling", "虫使い", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.bird_handling, "Bird Handling", "鳥使い", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.elementalor, "Elementalor", "精霊使い", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.summon_fairy, "Summon Fairy", "妖精使い", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.summon_familiar, "Summon Familiar", "使い魔召喚", "nothing", "nothing"));
            //使役者基本職上級
        abilitys.Add(new template(AbilityKind.monster_tamer, "Monster Tamer", "魔物使い", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.summoner, "Summoner", "召喚士", "nothing", "nothing"));

            //共通基本職（戦士初級）
        abilitys.Add(new template(AbilityKind.hoodlum, "Hoodlum", "ごろつき", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.seclusion, "Seclusion", "隠密", "nothing", "nothing"));
            //共通基本職（戦士中級）
        abilitys.Add(new template(AbilityKind.thief, "Thief", "盗賊", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.assassin, "Assassin", "暗殺者", "nothing", "nothing"));
            //共通基本職（戦士上級）
        abilitys.Add(new template(AbilityKind.ninja, "Ninja", "忍者", "nothing", "nothing"));

            //共通基本職（魔術師初級）
        abilitys.Add(new template(AbilityKind.black_mage, "Black_Mage", "黒魔術師", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.believer, "Believer", "教徒", "nothing", "nothing"));
            //共通基本職（魔術師中級）
        abilitys.Add(new template(AbilityKind.necromancer, "Necromancer", "死霊術師", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.priest, "Priest", "聖職者", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.fanatic, "Fanatic", "狂信者", "nothing", "nothing"));
            //共通基本職（魔術師上級）
        abilitys.Add(new template(AbilityKind.demonolater, "Demonolater", "悪魔崇拝者", "nothing", "nothing"));

            //共通基本職（生産職初級）
        abilitys.Add(new template(AbilityKind.pharmacist, "Pharmacist", "薬師", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.use_tools, "Use Tools", "道具使役", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.life_magic, "Life Magic", "生活魔法", "nothing", "nothing"));
            //共通基本職（生産職中級）
        abilitys.Add(new template(AbilityKind.doctor, "Doctor", "医者", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.crafter, "Crafter", "クラフター", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.whitesmith, "Whitesmith", "からくり士", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.magicrafter, "Magicrafter", "マギクラフター", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.enchanter, "Enchanter", "エンチャンター", "nothing", "nothing"));
            //共通基本職（生産職上級）
        abilitys.Add(new template(AbilityKind.blacksmith, "Blacksmith", "鍛冶師", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.machine_engineer, "Machine Engineer", "機械技術士", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.alchemist, "Alchemist", "錬金術師", "nothing", "nothing"));
        abilitys.Add(new template(AbilityKind.high_enchanter, "High Enchanter", "ハイエンチャンター", "nothing", "nothing"));

        /* アイテム */
        items.Add(new template(ItemKind.c, "C", "C言語", "", ""));
        items.Add(new template(ItemKind.java, "Java", "Java", "", ""));
        items.Add(new template(ItemKind.python, "Python", "Python", "", ""));

        /* 必要条件 */
        needs.Add(new template(NeedKind.nothing, "nothing", "nothing", "nothing", "nothing"));
        needs.Add(new template(NeedKind.fire, "fire", "", "", ""));
        needs.Add(new template(NeedKind.water, "water", "", "", ""));
        needs.Add(new template(NeedKind.sword, "sword", "", "", ""));

        //テスト
        TestAll();
    }

    public void TestAll()
    {
        TestOneList<ResourceKind>(resources);
        TestOneList<ActionEnum.Instant>(instantActions);
        TestOneList<ActionEnum.Loop>(loopActions);
        TestOneList<ActionEnum.Upgrade>(upgradeActions);
        TestOneList<AbilityKind>(abilitys);
        TestOneList<ItemKind>(items);
        TestOneList<NeedKind>(needs);
    }

    public void TestOneList<T>(List<template> templates)
        where T : Enum
    {
        if (Enum.GetNames(typeof(T)).Length != templates.Count)
        {
            throw new Exception("enumとenumの詳細のリストの要素の数が違います。(" + typeof(T) + ")");
        }
        for (int i = 0; i < templates.Count; i++)
        {
            if (Enum.GetNames(typeof(T))[i] != templates[i].kind.ToString()) { throw new Exception("enumとenumの詳細のリストの順番が違います。(" + typeof(T) + "." + Enum.GetNames(typeof(T))[i] + ")"); }
        }
    }
}
