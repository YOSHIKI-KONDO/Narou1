using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using MainAction;

/// <summary>
/// 全てのEnumの表示名と概要を記述するクラス。
/// それぞれのクラスから参照しているので、漏れがあるとエラーが出る。
/// </summary>
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
    public List<template> skills = new List<template>();
    public List<template> enemys = new List<template>();
    public List<template> dungeons = new List<template>();
    public List<template> allys = new List<template>();
    public List<template> attributes = new List<template>();
    public List<template> elements = new List<template>();

    // Use this for initialization
    void Awake () {
		StartBASE();

        /* リソース */
        resources.Add(new template(ResourceKind.nothing, "", "", "", ""));
        resources.Add(new template(ResourceKind.focus, "Focus", "フォーカス", "Improve action efficiency with mp.", ""));
        resources.Add(new template(ResourceKind.equipSpace, "Equip", "装備", "", ""));
        resources.Add(new template(ResourceKind.inventorySpace, "Inventory", "インベントリ", "", ""));
        //プレイヤーステータス
        resources.Add(new template(ResourceKind.strength, "Strength", "筋力", "", ""));
        resources.Add(new template(ResourceKind.mentalStrength, "Mental Strength", "精神力", "", ""));
        resources.Add(new template(ResourceKind.defense, "Defense", "防御力", "", ""));
        resources.Add(new template(ResourceKind.dodge, "Dodge Chance", "回避率", "", ""));
        resources.Add(new template(ResourceKind.criticalChance, "Critical Chance", "会心率", "", ""));
        resources.Add(new template(ResourceKind.exp, "Exp", "経験値", "", ""));
        resources.Add(new template(ResourceKind.itemPoint1, "Item Point[1]", "アイテムポイント★1", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint2, "Item Point[2]", "アイテムポイント★2", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint3, "Item Point[3]", "アイテムポイント★3", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint4, "Item Point[4]", "アイテムポイント★4", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint5, "Item Point[5]", "アイテムポイント★5", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint6, "Item Point[6]", "アイテムポイント★6", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint7, "Item Point[7]", "アイテムポイント★7", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint8, "Item Point[8]", "アイテムポイント★8", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint9, "Item Point[9]", "アイテムポイント★9", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        resources.Add(new template(ResourceKind.itemPoint10, "Item Point[10]", "アイテムポイント★10", "Use for Item level up.", "アイテムのレベルアップに使用します。"));
        //武器
        resources.Add(new template(ResourceKind.attack, "Attack", "攻撃力", "", ""));
        resources.Add(new template(ResourceKind.magic_attack, "Magic Attack", "魔法攻撃力", "", ""));
          //ステータスリソース
        resources.Add(new template(ResourceKind.action, "Action", "アクション", "", ""));
        resources.Add(new template(ResourceKind.hp, "HP", "HP", "", ""));
        resources.Add(new template(ResourceKind.mp, "MP", "MP", "", ""));
        resources.Add(new template(ResourceKind.stamina, "Stamina", "スタミナ", "", ""));
        resources.Add(new template(ResourceKind.fire, "Fire", "火", "", ""));
        resources.Add(new template(ResourceKind.water, "Water", "水", "", ""));
        resources.Add(new template(ResourceKind.wind, "Wind", "風", "", ""));
        resources.Add(new template(ResourceKind.earth, "Earth", "土", "", ""));
        resources.Add(new template(ResourceKind.thunder, "Thunder", "雷", "", ""));
        resources.Add(new template(ResourceKind.ice, "Ice", "氷", "", ""));
        resources.Add(new template(ResourceKind.light, "Light", "光", "", ""));
        resources.Add(new template(ResourceKind.dark, "Dark", "闇", "", ""));
        resources.Add(new template(ResourceKind.animal, "Animal", "アニマル", "", ""));
        //物資リソース
        resources.Add(new template(ResourceKind.gold, "Gold", "Gold", "", ""));
        resources.Add(new template(ResourceKind.research, "Destiny", "運命", "Everything starts from here.", ""));
        resources.Add(new template(ResourceKind.paper, "Rune", "ルーン", "", ""));
        resources.Add(new template(ResourceKind.book, "Rune Stone", "ルーン石碑", "", ""));
        resources.Add(new template(ResourceKind.dictionary, "Gospel", "福音書", "", ""));
        resources.Add(new template(ResourceKind.herb, "Herb", "ハーブ", "", ""));
        resources.Add(new template(ResourceKind.medicine, "Medicine", "薬", "", ""));
        resources.Add(new template(ResourceKind.potion, "Potion", "ポーション", "", ""));
        resources.Add(new template(ResourceKind.wood, "Wood", "木", "the best wood to make firewood", "薪を作るのに最適な木。"));
        resources.Add(new template(ResourceKind.firewood, "Firewood", "薪", "good firewood", "良いサイズの薪。"));
        resources.Add(new template(ResourceKind.charcoal, "Charcoal", "木炭", "", ""));
        resources.Add(new template(ResourceKind.flower, "Flower", "花", "This is called Flower of Yggdrasil. You can feel the intense power.", "ユグドラシルの花と呼ばれている。強い生命力を感じる。"));
        //食べ物
        resources.Add(new template(ResourceKind.wheat, "Wheat", "小麦", "", ""));
        resources.Add(new template(ResourceKind.dough, "Dough", "パン生地", "", ""));//add
        resources.Add(new template(ResourceKind.bread, "Bread", "パン", "", ""));
        resources.Add(new template(ResourceKind.fish, "Fish", "魚", "", ""));
        resources.Add(new template(ResourceKind.anchovy_sandwich, "AnchovySandwich", "アンチョビサンド", "Local specialty. Salty.", "故郷の名産物。塩味が効いている。"));
        resources.Add(new template(ResourceKind.filet_o_fish, "Filet-O-Fish", "フィレオフィッシュ", "Eating this makes you feel better even if you are depressed.", "落ち込んでいてもこれを食べたら元気が出る。"));
            //石
        resources.Add(new template(ResourceKind.stone, "Stone", "石", "Not just a stone. Easy to use, selected stone.", "ただの石じゃない。加工しやすい、選ばれし石。"));
        resources.Add(new template(ResourceKind.magi_stone, "Magi Stone", "魔石", "", ""));
        resources.Add(new template(ResourceKind.fire_stone, "Fire Stone", "火魔石", "", ""));
        resources.Add(new template(ResourceKind.water_stone, "Water Stone", "水魔石", "", ""));
        resources.Add(new template(ResourceKind.wind_stone, "Wind Stone", "風魔石", "", ""));
        resources.Add(new template(ResourceKind.earth_stone, "Earth Stone", "土魔石", "", ""));
        resources.Add(new template(ResourceKind.thunder_stone, "Thunder Stone", "雷魔石", "", ""));
        resources.Add(new template(ResourceKind.ice_stone, "Ice Stone", "氷魔石", "", ""));
        resources.Add(new template(ResourceKind.light_stone, "Light Stone", "光魔石", "", ""));
        resources.Add(new template(ResourceKind.dark_stone, "Dark Stone", "闇魔石", "", ""));
             //鉄
        resources.Add(new template(ResourceKind.iron, "Iron", "鉄", "", ""));
        resources.Add(new template(ResourceKind.steel, "Steel", "鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.magi_steel, "Magi Steel", "魔鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.fire_steel, "Fire Steel", "魔鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.water_steel, "Water Steel", "魔鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.wind_steel, "Wind Steel", "魔鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.earth_steel, "Earth Steel", "魔鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.thunder_steel, "Thunder Steel", "魔鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.ice_steel, "Ice Steel", "魔鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.light_steel, "Light Steel", "魔鋼鉄", "", ""));
        resources.Add(new template(ResourceKind.dark_steel, "Dark Steel", "魔鋼鉄", "", ""));
            //上位鉱石
        resources.Add(new template(ResourceKind.mithril, "Mithril", "ミスリル", "Expensive ore. It is very hard and passes magic power.", "高価な鉱石。とても硬く、魔力を通す。"));
        resources.Add(new template(ResourceKind.adamantite, "Adamantite", "アダマンタイト", "Expensive ore. It is very hard and doesn't pass magic power.", "高価な鉱石。非常に硬く、魔力を通さない。"));
        resources.Add(new template(ResourceKind.orichalcum, "Orichalcum", "オリハルコン", "The most marvelous ore.", "天下最強の鉱石。"));

          //アビリティポイント
        resources.Add(new template(ResourceKind.ap, "AP", "AP", "", ""));

            //モンスター素材
        resources.Add(new template(ResourceKind.fur, "Pelt", "獣の皮", "Pelt from beasts.", "獣から剥ぎ取った毛皮。"));
        resources.Add(new template(ResourceKind.premium_fur, "Prized plet", "上質な皮", "Has no scratches.", "傷一つついていない、上質な毛皮。"));

        /* インスタントアクション */
        instantActions.Add(new template(ActionEnum.Instant.nothing, "", "", "", ""));
          //フェーズ１（入学前）
        instantActions.Add(new template(ActionEnum.Instant.weeding, "Weeding", "草むしり", "Part time job. Once in a while, you can get herbs.", "アルバイト。たまにハーブが生えている。"));
        instantActions.Add(new template(ActionEnum.Instant.intensive_training, "Intensive Training", "猛特訓", "Gain destiny by training hard.", "体を酷使することで運命力を得る。"));
        instantActions.Add(new template(ActionEnum.Instant.eat_anchovy_sandwich, "Eat Anchovy Sandwich", "アンチョビサンドを食べる", "The salty taste recovers you from fatigue.", "塩味が効いていて、疲れた体に旨みが染み渡る。"));
        instantActions.Add(new template(ActionEnum.Instant.eat_filet_o_fish, "Eat Filet-O-Fish", "フィレオフィッシュを食べる", "As soon as you put it in your mouth, the deliciousness of fried fish between fluffy patties fills your mouth.", "一口噛むと、ふっくらとしたバンズに挟まれたフィッシュフライの油がほとばしる。"));
        instantActions.Add(new template(ActionEnum.Instant.drink_herb_tea, "Drink Herb tea", "ハーブティーを飲む", "It tastes sacred.", "神聖な味がする。"));
        instantActions.Add(new template(ActionEnum.Instant.rune_generation, "Rune Generation", "ルーン生成", "Obtain Rune from the destiny power.", "運命の力でルーンを得る。"));
        instantActions.Add(new template(ActionEnum.Instant.runic_carving, "Runic Carving", "ルーンを刻む", "Obtain further destiny from carving with runes.", "ルーンを石碑に刻み、更なる運命を得る。"));
        instantActions.Add(new template(ActionEnum.Instant.split_firewood, "Split Firewood", "薪割り", "", ""));
        instantActions.Add(new template(ActionEnum.Instant.sell_firewood, "Sell Firewood", "薪を売る", "", ""));
        instantActions.Add(new template(ActionEnum.Instant.sell_bread, "Sell Bread", "パンを売る", "", ""));
        instantActions.Add(new template(ActionEnum.Instant.sell_anchovy_sandwich, "Sell Anchovy Sandwich", "アンチョビサンドを売る", "", ""));
        //アイテムポイントコンバージョン
        instantActions.Add(new template(ActionEnum.Instant.ip1down, "Downgrade Item Point[1]", "ダウングレードアイテムポイント★1", "Item Point★1", "Item Point★1"));//modify
        instantActions.Add(new template(ActionEnum.Instant.ip2up, "Upgrade Item Point[2]", "アップグレードアイテムポイント★2", "Item Point★2", "Item Point★2"));    //modify
        instantActions.Add(new template(ActionEnum.Instant.ip2down, "Downgrade Item Point[2]", "ダウングレードアイテムポイント★2", "Item Point★2", "Item Point★2"));//modify
        instantActions.Add(new template(ActionEnum.Instant.ip3up, "Upgrade Item Point[3]", "アップグレードアイテムポイント★3", "Item Point★3", "Item Point★3"));    //modify
        instantActions.Add(new template(ActionEnum.Instant.ip3down, "Downgrade Item Point[3]", "ダウングレードアイテムポイント★3", "Item Point★3", "Item Point★3"));//modify
        instantActions.Add(new template(ActionEnum.Instant.ip4up, "Upgrade Item Point[4]", "アップグレードアイテムポイント★4", "Item Point★4", "Item Point★4"));    //modify
        instantActions.Add(new template(ActionEnum.Instant.ip4down, "Downgrade Item Point[4]", "ダウングレードアイテムポイント★4", "Item Point★4", "Item Point★4"));//modify
        instantActions.Add(new template(ActionEnum.Instant.ip5up, "Upgrade Item Point[5]", "アップグレードアイテムポイント★5", "Item Point★5", "Item Point★5"));    //modify
        instantActions.Add(new template(ActionEnum.Instant.ip5down, "Downgrade Item Point[5]", "ダウングレードアイテムポイント★5", "Item Point★5", "Item Point★5"));//modify
        instantActions.Add(new template(ActionEnum.Instant.ip6up, "Upgrade Item Point[6]", "アップグレードアイテムポイント★6", "Item Point★6", "Item Point★6"));    //modify
        instantActions.Add(new template(ActionEnum.Instant.ip6down, "Downgrade Item Point[6]", "ダウングレードアイテムポイント★6", "Item Point★6", "Item Point★6"));//modify
        instantActions.Add(new template(ActionEnum.Instant.ip7up, "Upgrade Item Point[7]", "アップグレードアイテムポイント★7", "Item Point★7", "Item Point★7"));    //modify

        //フェーズ２（学校）
        //アビリティアクション
        instantActions.Add(new template(ActionEnum.Instant.mugged, "Extortion", "カツアゲ", "Rob someone of money! 1 gold per 1 click!", "お金を巻き上げろ！１クリック１パンチ！"));
        instantActions.Add(new template(ActionEnum.Instant.devotion, "Devotion", "祈祷", "", ""));
        instantActions.Add(new template(ActionEnum.Instant.take_medicine, "Take Medicine", "薬を飲む", "", ""));
        /* ループアクション */
        loopActions.Add(new template(ActionEnum.Loop.nothing, "", "", "", ""));
            //常用
        loopActions.Add(new template(ActionEnum.Loop.rest, "Rest", "休憩", "", ""));
        loopActions.Add(new template(ActionEnum.Loop.pray, "Pray", "祈る", "", ""));
          //フェーズ１（入学前）
        loopActions.Add(new template(ActionEnum.Loop.farmwork, "Farmwork", "農作業", "", ""));
        loopActions.Add(new template(ActionEnum.Loop.harvest_wheat, "Harvest Wheat", "小麦の収穫", "", ""));
        loopActions.Add(new template(ActionEnum.Loop.grow_herb, "Grow Herb", "ハーブ栽培", "", ""));
        loopActions.Add(new template(ActionEnum.Loop.lumberjack, "Lumberjack", "木こり", "You mastered how to use an axe.", "手斧の扱いをマスターした。"));
        loopActions.Add(new template(ActionEnum.Loop.craft_magi_stone, "Craft:Magi Stone", "作成：マギストーン", "Generate the magical stone.", "魔力を込めた石を作成する。"));
          //フェーズ２（学校）
        loopActions.Add(new template(ActionEnum.Loop.manual_labor, "Manual Labor", "肉体労働", "", ""));
        loopActions.Add(new template(ActionEnum.Loop.desk_work, "Desk Work", "デスクワーク", "", ""));
        loopActions.Add(new template(ActionEnum.Loop.service_trade, "Pub Waitron", "酒場の接客", "", ""));//modify
            //アビリティアクション
        loopActions.Add(new template(ActionEnum.Loop.pickpocket, "Pickpocket", "スリ", "", ""));
        loopActions.Add(new template(ActionEnum.Loop.bake_bread, "Baking", "パンを焼く", "", ""));//modify
        loopActions.Add(new template(ActionEnum.Loop.dispense_medicines, "Making Medicines", "薬の調合", "", ""));

        /* アップグレードアクション */
        upgradeActions.Add(new template(ActionEnum.Upgrade.nothing, "", "", "", ""));
          //フェーズ１（入学前）
        upgradeActions.Add(new template(ActionEnum.Upgrade.talk_fatherA, "Talk with Father", "父の話を聞く", "My son, Be strong.", "息子よ、強くなるのだ。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.talk_fatherB, "Talk with Father", "父の話を聞く", "Your mother was taken away by the Demon King.I want you to help her.", "お前の母は魔王に連れ去られた。助け出して欲しい。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.talk_fatherC, "Talk with Father", "父の話を聞く", "I resisted but couldn't help and lost my right arm.", "私は抵抗したが力及ばず、右腕を失ってしまった。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.talk_fatherD, "Talk with Father", "父の話を聞く", "Go to school. Learn what I can't teach.", "学校に行くと良い。私では教えられないことを学べる。"));
            //父の道場
        upgradeActions.Add(new template(ActionEnum.Upgrade.training, "Training", "トレーニング", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.fathers_gym, "Father's Training room", "父の道場", "", ""));//modify
        upgradeActions.Add(new template(ActionEnum.Upgrade.sword_practice, "Sword Practice", "剣の稽古", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.spear_practice, "Spear Practice", "槍の稽古", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.rod_practice, "Rod Practice", "棒の稽古", "", ""));
            //母の書斎                             
        upgradeActions.Add(new template(ActionEnum.Upgrade.mental_training, "Mental Training", "メンタルトレーニング", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.mothers_den, "Mother's Den", "母の書斎", "", ""));//add
        upgradeActions.Add(new template(ActionEnum.Upgrade.read_fire_spellbook, "Read Fire Spellbook", "火の魔導書", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.read_water_spellbook, "Read Water Spellbook", "水の魔導書", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.read_wind_spellbook, "Read Wind Spellbook", "風の魔導書", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.read_earth_spellbook, "Read Earth Spellbook", "土の魔導書", "", ""));
            //村の広場
        upgradeActions.Add(new template(ActionEnum.Upgrade.go_to_the_town, "Go to the Town", "村へ行く", "", ""));//add
        upgradeActions.Add(new template(ActionEnum.Upgrade.play_with_cat, "Play wit Cat", "猫と遊ぶ", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.learn_use_tools, "Learn Use Tools", "道具の使い方を教わる", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.succession_life_magic, "Life Magic Inheritance", "生活魔法継承", "", ""));
            //純アップグレード
        upgradeActions.Add(new template(ActionEnum.Upgrade.buy_wallet, "Buy Purse", "財布を買う", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.study_in_church, "Study in Church", "教会で勉強", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.buy_bag, "Buy Bag", "鞄を買う", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.rune_augmentation, "Rune Augmentation", "ルーン増強", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.buy_fire_spellbook, "Buy Fire Spellbook", "火の魔導書を買う", "", ""));
            //少女イベント
        upgradeActions.Add(new template(ActionEnum.Upgrade.girl_is_crying, "A Girl is Crying", "少女が泣いている", "It seems that the flowers she cherished have been broken down into bad kids.", "大切にしていた花を、悪ガキにへし折られてしまってようだ。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.pick_flowers, "Pick Flowers", "花を摘みに行く", "Give the girl a new flower.", "少女に新しい花をプレゼントしよう。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.punish_the_bad_kids, "Punish the Bad Kids", "悪ガキをこらしめる", "Let's break bones and minds.", "骨と心をへし折ってやろう。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.thank_you, "Thank you", "ありがとう", "The girl gently grasped the flower and smiled shyly.", "少女は花を優しく握りしめ、はにかみながら笑った。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.ill_get_you_for_this, "I'll get you for this!", "覚えておけよ！", "The bad kid ran away in tears. nothing to concern.", "悪ガキは涙目になりながら走り去っていった。これで一安心だ。"));
            //進学
        upgradeActions.Add(new template(ActionEnum.Upgrade.warrior_school, "Warrior School", "戦士学校", "Decide to enter warrior school. You can't go back, so consider where you want to go.", "戦士学校への入学を決意する。後戻りはできないので入学先はしっかり考えよう。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.sorcerer_school, "Sorcerer School", "魔法学校", "Decide to enter Sorcerer school. You can't go back, so consider where you want to go.", "魔法学校への入学を決意する。後戻りはできないので入学先はしっかり考えよう。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.tamer_school, "Tamer School", "テイマー学校", "Decide to enter Tamer school. You can't go back, so consider where you want to go.", "テイマー学校への入学を決意する。後戻りはできないので入学先はしっかり考えよう。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.leave_the_town, "Talk with Father", "父と話す", "Leave the village. Head to the school city beyond the plains and forests. Make sure that you have nothing to do now.", "村を出るのか。母のことは任せたぞ。それと、やり残したことはないか確認しておきなさい。それと、この御守りを持っていきなさい。"));//modify
        upgradeActions.Add(new template(ActionEnum.Upgrade.academic_city, "Leave the Town", "村を出る", "The girl says she does want to go with me. Prepare your luggage for two people.", "平原と森を越えた先の学園都市に向かう。村を出ようとすると少女に呼び止められた。少女が付いてくると言って聞かない。二人分の荷物を用意しよう。"));//modify
        upgradeActions.Add(new template(ActionEnum.Upgrade.shed, "Cabin", "小屋", "After a short walk through the plains, You see a cabin in front of the forest.", "平原をしばらく進むと、森の手前に小屋が見えた。"));//modify
        upgradeActions.Add(new template(ActionEnum.Upgrade.talk_old_man, "Talk with old man", "老人の話を聞く", "[Look weak. In the forest ahead there will be stronger monsters. Equip armor. Obtain a wooden shield from the goblin corpse.]", "弱そうな子供だな。この先の森はより強い魔物が出る。防具を装備しなさい。ゴブリンの死体から木の盾を拝借するのだ。"));//add
        upgradeActions.Add(new template(ActionEnum.Upgrade.open_closet, "Open Closet", "クローゼットを開ける", "So easy to do everything, hiding from an old man.", "老人の目を欺くことなどたやすい。"));//add

        //フェーズ２（学校）
        
        upgradeActions.Add(new template(ActionEnum.Upgrade.into_a_dormitory, "Into a Dormitory", "学生寮に入る", "Breaks up with accompanying person and enters dormitory. \n[Please say hello to me when you are calm down.]", "同行者と別れ、寮に入る。「また後で、落ち着いたら挨拶に来てね。」"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.good_sleep, "Good Sleep", "快眠", "Rest Level Up!!", "休憩レベルアップ！！"));//add
        upgradeActions.Add(new template(ActionEnum.Upgrade.entrance_ceremony, "Entrance Ceremony", "入学式", "A stage for us. Feel like walking on air with thinking about my future.", "晴れ舞台だ。これからの生活に向けて気分が高揚する。"));
            //攫われイベント
        upgradeActions.Add(new template(ActionEnum.Upgrade.norns_room, "Norn's room", "ノルンの部屋に行く", "Go to the girl's room to say hello", "少女の部屋へ挨拶に向かう。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.desolate_room, "Trashed room", "荒らされた部屋", "But no one is inside. There is evidence of competing. I remembered my mother, and became upset.", "しかし、中には誰もいない。争った形跡がある。攫われた母のことを思い出し、胸が高鳴った。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.rumor, "Rumor", "ウワサ", "Recently, demons have settled in the notorious garbage mansion. It's so dirty that the monsters settled down even though it's not sewers.", "近頃、悪評高いゴミ屋敷には魔物が住み着いている。下水道でもないのに魔物が住み着くなんてよほど汚いみたいだ。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.resucue_a_girl, "Saving a Girl", "少女を救出する", "Saving a girl who has been in a jail.  Though she is fainted, it looks like she is safe.", "囚われていた少女を救出する。気を失っているが、命に別状はないようだ。"));//modify
            //フリークエスト
        upgradeActions.Add(new template(ActionEnum.Upgrade.delivery_of_fur, "Delivery of pelt", "毛皮の納品", "[Deliver the pelt that you can get by defeating Wolf. Since it will be for sale, I call for less scratches.]", "ウルフを倒すと手に入る、毛皮を納品してくれ。商品なんだから傷は少なめで頼むよ。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.house_clean_up, "House clean up", "ゴミ屋敷の掃除", "[What is the landlord doing!? Somebody clean it up! It's absolutely annoying!!]", "家主は何をしているんだ？近所迷惑だから誰でもいい、掃除してくれ！"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.get_rid_of_rat, "Get rid of rat", "ネズミ退治", "A request to get rid of poison rats that lived in the sewer. Unpopular.", "下水道に発生したポイズンラットを定期的に駆除しないといけない。不人気な依頼。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.wholesaler_of_drugs, "Wholesaler of Medicine", "医薬品卸", "I cannot have too much medicines. I'd like to buy some of them.(Up to 3 times)", "薬はいくつあっても安心だ。市場に回すため引き取らせてもらいたい。(３回まで)"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.in_cellar, "Into cellar", "地下室へ", "Hoarding house seems to have the cellar. Petals are scattered at the feet.", "ゴミ屋敷から、地下へと続くあなぐらが掘られていた。足元には花びらが散乱している。"));

            //授業
        upgradeActions.Add(new template(ActionEnum.Upgrade.sword_classwork, "Sword Class", "剣術の授業", "A muscular teacher will teach you the steps.", "筋骨隆々な先生が手取り足取り教えてくれるぞ。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.spear_classwork, "Spear Class", "槍術の授業", "Spear teacher's hobby is spearfishing.", "槍術の先生の趣味はスピアフィッシングとのこと。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.rod_classwork, "Rod Class", "棒術の授業", "According to the teacher, it seems to be useful to knead the dough if you learn stick techniques.", "先生曰く、棒術を覚えるとパン生地をこねるのに便利らしい。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.fire_magic_classwork, "Fire Magic Class", "火魔術の授業", "Fire magic is dangerous, but essential for production.", "火魔術は危険だが、生産には必須である。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.water_magic_classwork, "Water Magic Class", "水魔術の授業", "A wonderful magic that can easily catch underwater fish.", "水中の魚を容易く捕らえることができる、素晴らしい魔術だ。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.wind_magic_classwork, "Wind Magic Class", "風魔術の授業", "A terrible magic that can handle the wind like a blade.", "風を刃のように扱うことができる、おそろしい魔術だ。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.earth_magic_classwork, "Earth Magic Class", "土魔術の授業", "Those who are familiar with earth magic are helpful for land reclamation.", "土魔術に精通した人は土地の開拓に重宝されるらしい。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.animal_handling_classwork, "Animal Handling Class", "動物使役の授業", "The classroom overflows with pets and is noisy.", "教室がペットで溢れ返り、うるさい。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.use_tools_classwork, "Use Tools Class", "道具使役の授業", "It is said that familiarity with tools will open the way to various production jobs.", "道具に精通することで、様々な生産職への道が開かれるとのこと。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.life_magic_classwork, "Life Magic Class", "生活魔法の授業", "Many people take classes for the time being because it is useful.", "いずれ役立つので、とりあえず授業を受ける人が多い。"));

            //戦士
        upgradeActions.Add(new template(ActionEnum.Upgrade.apprentice_warrior, "Apprentice Warrior", "戦士見習い", "Approved as an apprentice. Work on further technical skills.", "見習いとして認められる。更なる技術の習熟に臨むべし。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.warrior, "Warrior", "ウォーリアー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.soldier, "Soldierg", "ソルジャー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.mercenary, "Mercenary", "マーセナリー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.trooper, "Trooper", "トルーパー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.knight, "Knight", "ナイト", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.slayer, "Slayer", "スレイヤー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.fighter, "Fighter", "ファイター", "", ""));
            //魔導士
        upgradeActions.Add(new template(ActionEnum.Upgrade.apprentice_sorcerer, "Apprentice Sorcerer", "魔導士見習い", "Approved as an apprentice. Work on further technical skills.", "見習いとして認められる。更なる技術の習熟に臨むべし。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.sorcerer, "Sorcerer", "ソーサラー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.wizard, "Wizard", "ウィザード", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.warlock, "Warlock", "ウォーロック", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.high_sorcerer, "High Sorcerer", "ハイソーサラー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.priest, "Priest", "プリースト", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.conjurer, "Conjurer", "コンジュラー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.black_mage, "Black Mage", "ブラックメイジ", "", ""));
            //テイマー
        upgradeActions.Add(new template(ActionEnum.Upgrade.apprentice_tamer, "Apprentice Tamer", "テイマー見習い", "Approved as an apprentice. Work on further technical skills.", "見習いとして認められる。更なる技術の習熟に臨むべし。"));
        upgradeActions.Add(new template(ActionEnum.Upgrade.tamer, "Tamer", "テイマー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.beast_tamer, "Beast Tamer", "ビーストテイマー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.elementaler, "Elementaler", "エレメンタラー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.monster_tamer, "Monster Tamer", "モンスターテイマー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.summoner, "Summoner", "サモナー", "", ""));
            //生産職
        upgradeActions.Add(new template(ActionEnum.Upgrade.crafter, "Crafter", "クラフター", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.iron_crafter, "Iron Crafter", "アイアンクラフター", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.magicrafter, "MagiCrafter", "マギクラフター", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.weapon_maker, "Weapon Maker", "ウェポンメイカー", "", ""));
        upgradeActions.Add(new template(ActionEnum.Upgrade.alchemist, "Alchemist", "アルケミスト", "", ""));

        upgradeActions.Add(new template(ActionEnum.Upgrade.thank_you_for_playing, "Thank you for playing", "Thank you for playing", "", ""));

        /* アビリティ */
        abilitys.Add(new template(AbilityKind.nothing, "", "", "", ""));
            //戦士基本職初級
        abilitys.Add(new template(AbilityKind.beginner_swordmanship, "Beginner Swordmanship", "初級剣術", "", ""));
        abilitys.Add(new template(AbilityKind.beginner_spearmanship, "Beginner Spearmanship", "初級槍術", "", ""));
        abilitys.Add(new template(AbilityKind.beginner_bojutsu, "Beginner Bojutsu", "初級棒術", "", ""));
        abilitys.Add(new template(AbilityKind.beginner_battleaxe, "Beginner Battleaxe", "初級斧術", "", ""));
        abilitys.Add(new template(AbilityKind.beginner_shieldmanship, "Beginner Shieldmanship", "初級盾術", "", ""));
        abilitys.Add(new template(AbilityKind.beginner_martialart, "Beginner MartialArt", "初級体術", "", ""));
            //戦士基本職中級
        abilitys.Add(new template(AbilityKind.intermediate_swordmanship, "Intermediate Swordmanship", "中級剣術", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_spearmanship, "Intermediate Spearmanship", "中級槍術", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_bojutsu, "Intermediate Bojutsu", "中級棒術", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_battleaxe, "Intermediate Battleaxe", "中級斧術", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_shieldmanship, "Intermediate Shieldmanship", "中級盾術", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_martialart, "Intermediate MartialArt", "中級体術", "", ""));
            //戦士基本職上級
        abilitys.Add(new template(AbilityKind.advanced_swordmanship, "Advanced Swordmanship", "上級剣術", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_spearmanship, "Advanced Spearmanship", "上級槍術", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_bojutsu, "Advanced Bojutsu", "上級棒術", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_battleaxe, "Advanced Battleaxe", "上級斧術", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_shieldmanship, "Advanced Shieldmanship", "上級盾術", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_martialart, "Advanced MartialArt", "上級体術", "", ""));

            //魔術士基本職初級
        abilitys.Add(new template(AbilityKind.primary_fire_magic, "Primary Fire Magic", "初級火魔法", "", ""));
        abilitys.Add(new template(AbilityKind.primary_water_magic, "Primary Water Magic", "初級水魔法", "", ""));
        abilitys.Add(new template(AbilityKind.primary_wind_magic, "Primary Wind Magic", "初級風魔法", "", ""));
        abilitys.Add(new template(AbilityKind.primary_earth_magic, "Primary Earth Magic", "初級土魔法", "", ""));
        abilitys.Add(new template(AbilityKind.primary_thunder_magic, "Primary Thunder Magic", "初級雷魔法", "", ""));
        abilitys.Add(new template(AbilityKind.primary_ice_magic, "Primary Ice Magic", "初級氷魔法", "", ""));
        abilitys.Add(new template(AbilityKind.primary_light_magic, "Primary Light Magic", "初級光魔法", "", ""));
        abilitys.Add(new template(AbilityKind.primary_dark_magic, "Primary Dark Magic", "初級闇魔法", "", ""));
            //魔術士基本職中級
        abilitys.Add(new template(AbilityKind.intermediate_fire_magic, "Intermediate Fire Magic", "中級火魔法", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_water_magic, "Intermediate Water Magic", "中級水魔法", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_wind_magic, "Intermediate Wind Magic", "中級風魔法", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_earth_magic, "Intermediate Earth Magic", "中級土魔法", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_thunder_magic, "Intermediate Thunder Magic", "中級雷魔法", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_ice_magic, "Intermediate Ice Magic", "中級氷魔法", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_light_magic, "Intermediate Light Magic", "中級光魔法", "", ""));
        abilitys.Add(new template(AbilityKind.intermediate_dark_magic, "Intermediate Dark Magic", "中級闇魔法", "", ""));
            //魔術士基本職上級
        abilitys.Add(new template(AbilityKind.advanced_fire_magic, "Advanced Fire Magic", "上級火魔法", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_water_magic, "Advanced Water Magic", "上級水魔法", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_wind_magic, "Advanced Wind Magic", "上級風魔法", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_earth_magic, "Advanced Earth Magic", "上級土魔法", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_thunder_magic, "Advanced Thunder Magic", "上級雷魔法", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_ice_magic, "Advanced Ice Magic", "上級氷魔法", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_light_magic, "Advanced Light Magic", "上級光魔法", "", ""));
        abilitys.Add(new template(AbilityKind.advanced_dark_magic, "Advanced Dark Magic", "上級闇魔法", "", ""));

            //使役者基本職初級
        abilitys.Add(new template(AbilityKind.animal_handling, "Animal Handling", "動物使役", "", ""));
            //使役者基本職中級
        abilitys.Add(new template(AbilityKind.beast_tamer, "Beast Tamer", "獣使い", "", ""));
        abilitys.Add(new template(AbilityKind.insect_handling, "Insect Handling", "虫使い", "", ""));
        abilitys.Add(new template(AbilityKind.bird_handling, "Bird Handling", "鳥使い", "", ""));
        abilitys.Add(new template(AbilityKind.elementalor, "Elementalor", "精霊使い", "", ""));
        abilitys.Add(new template(AbilityKind.summon_fairy, "Summon Fairy", "妖精使い", "", ""));
        abilitys.Add(new template(AbilityKind.summon_familiar, "Summon Familiar", "使い魔召喚", "", ""));
            //使役者基本職上級
        abilitys.Add(new template(AbilityKind.monster_tamer, "Monster Tamer", "魔物使い", "", ""));
        abilitys.Add(new template(AbilityKind.summoner, "Summoner", "召喚士", "", ""));

            //共通基本職（戦士初級）
        abilitys.Add(new template(AbilityKind.hoodlum, "Hoodlum", "ごろつき", "", ""));
        abilitys.Add(new template(AbilityKind.seclusion, "Seclusion", "隠密", "", ""));
            //共通基本職（戦士中級）
        abilitys.Add(new template(AbilityKind.thief, "Thief", "盗賊", "", ""));
        abilitys.Add(new template(AbilityKind.assassin, "Assassin", "暗殺者", "", ""));
            //共通基本職（戦士上級）
        abilitys.Add(new template(AbilityKind.ninja, "Ninja", "忍者", "", ""));

            //共通基本職（魔術師初級）
        abilitys.Add(new template(AbilityKind.black_mage, "Black Mage", "黒魔術師", "", ""));
        abilitys.Add(new template(AbilityKind.believer, "Believer", "教徒", "", ""));
            //共通基本職（魔術師中級）
        abilitys.Add(new template(AbilityKind.necromancer, "Necromancer", "死霊術師", "", ""));
        abilitys.Add(new template(AbilityKind.priest, "Priest", "聖職者", "", ""));
        abilitys.Add(new template(AbilityKind.fanatic, "Fanatic", "狂信者", "", ""));
            //共通基本職（魔術師上級）
        abilitys.Add(new template(AbilityKind.demonolater, "Demonolater", "悪魔崇拝者", "", ""));

            //共通基本職（生産職初級）
        abilitys.Add(new template(AbilityKind.pharmacist, "Pharmacist", "薬師", "", ""));
        abilitys.Add(new template(AbilityKind.use_tools, "Use Tools", "道具使役", "", ""));
        abilitys.Add(new template(AbilityKind.life_magic, "Life Magic", "生活魔法", "", ""));
            //共通基本職（生産職中級）
        abilitys.Add(new template(AbilityKind.doctor, "Doctor", "医者", "", ""));
        abilitys.Add(new template(AbilityKind.crafter, "Crafter", "クラフター", "", ""));
        abilitys.Add(new template(AbilityKind.whitesmith, "Whitesmith", "からくり士", "", ""));
        abilitys.Add(new template(AbilityKind.magicrafter, "Magicrafter", "マギクラフター", "", ""));
        abilitys.Add(new template(AbilityKind.enchanter, "Enchanter", "エンチャンター", "", ""));
            //共通基本職（生産職上級）
        abilitys.Add(new template(AbilityKind.blacksmith, "Blacksmith", "鍛冶師", "", ""));
        abilitys.Add(new template(AbilityKind.machine_engineer, "Machine Engineer", "機械技術士", "", ""));
        abilitys.Add(new template(AbilityKind.alchemist, "Alchemist", "錬金術師", "", ""));
        abilitys.Add(new template(AbilityKind.high_enchanter, "High Enchanter", "ハイエンチャンター", "", ""));

        /* アイテム */
          //フェーズ１
        items.Add(new template(ItemKind.reference_book, "Reference Book", "参考書", "", ""));
        items.Add(new template(ItemKind.toolbox, "Toolbox", "工具箱", "", ""));
        items.Add(new template(ItemKind.woodsword, "Wood Sword", "木の剣", "", ""));
        items.Add(new template(ItemKind.woodspear, "Wood Spear", "木の槍", "", ""));
        items.Add(new template(ItemKind.woodstick, "Wood Stick", "木の杖", "", ""));
        items.Add(new template(ItemKind.fire_orb, "Fire Orb", "ファイアーオーブ", "An orb that holds the magic of fire. Magic beginners train with this.", "火の魔力を閉じ込めたオーブ。魔法初心者はこれを用いて訓練する。"));
        items.Add(new template(ItemKind.water_orb, "Water Orb", "ウォーターオーブ", "An orb that holds the magic of water. Magic beginners train with this.", "水の魔力を閉じ込めたオーブ。魔法初心者はこれを用いて訓練する。"));
        items.Add(new template(ItemKind.wind_orb, "Wind Orb", "ウインドオーブ", "An orb that holds the magic of wind. Magic beginners train with this.", "風の魔力を閉じ込めたオーブ。魔法初心者はこれを用いて訓練する。"));
        items.Add(new template(ItemKind.earth_orb, "Earth Orb", "アースオーブ", "An orb that holds the magic of earth. Magic beginners train with this.", "土の魔力を閉じ込めたオーブ。魔法初心者はこれを用いて訓練する。"));
        items.Add(new template(ItemKind.animalfood, "Animal Food", "アニマルフード", "", ""));
          //Phase2
        items.Add(new template(ItemKind.stone_sword, "Stone Sword", "石の剣", "", ""));
        items.Add(new template(ItemKind.stone_spear, "Stone Spear", "石の槍", "", ""));
        items.Add(new template(ItemKind.fire_rod, "Fire Rod", "火の魔杖", "", ""));
        items.Add(new template(ItemKind.water_rod, "Water Rod", "水の魔杖", "", ""));
        items.Add(new template(ItemKind.wind_rod, "Wind Rod", "風の魔杖", "", ""));
        items.Add(new template(ItemKind.earth_rod, "Earth Rod", "地の魔杖", "", ""));
        items.Add(new template(ItemKind.stone_axe, "Stone Axe", "石の斧", "", ""));
        items.Add(new template(ItemKind.stone_shield, "Stone Shield", "石の盾", "", ""));
        items.Add(new template(ItemKind.leather_vest, "Leather Vest", "レザーベスト", "", ""));
        items.Add(new template(ItemKind.plate_mail, "Plate Mail", "プレートメイル", "", ""));
        items.Add(new template(ItemKind.fire_ruby, "Fire Ruby", "ファイアールビー", "", ""));
        items.Add(new template(ItemKind.sea_breeze_amulet, "Sea Breeze Amulet", "潮風のお守り", "", ""));
        items.Add(new template(ItemKind.rosary, "Rosary", "ロザリオ", "", ""));
        items.Add(new template(ItemKind.pouch, "Pouch", "ポーチ", "", ""));
        items.Add(new template(ItemKind.small_basket, "Small Basket", "スモールバスケット", "", ""));
        items.Add(new template(ItemKind.kiln, "Kiln", "窯", "", ""));
        items.Add(new template(ItemKind.medicine_box, "Medicine Box", "薬箱", "", ""));
        items.Add(new template(ItemKind.sandwich_stall, "Sandwich Stall", "サンドイッチの屋台", "", ""));
        items.Add(new template(ItemKind.planter, "Planter", "植木鉢", "", ""));

        items.Add(new template(ItemKind.warrior_textbook, "Warrior Textbook", "戦士教本", "", ""));
        items.Add(new template(ItemKind.sorcerer_textbook, "Sorcerer Textbook", "魔術士教本", "", ""));
        items.Add(new template(ItemKind.tamer_textbook, "Tamer Textbook", "テイマー教本", "", ""));
        items.Add(new template(ItemKind.umbrella, "Umbrella", "傘", "Legendary weapon every boy has.", "物心ついた少年が手にする、伝説の武器。"));

        items.Add(new template(ItemKind.cutlass, "Cutlass", "カットラス", "", ""));//add
        items.Add(new template(ItemKind.trident, "Trident", "トライデント", "", ""));//add
        items.Add(new template(ItemKind.whip, "Whip", "鞭", "", ""));//add
        items.Add(new template(ItemKind.claw_bar, "Crowbar", "バール", "A magic wand. Feel the magic power.", "魔法のステッキ。魔力を感じる。"));//add
        items.Add(new template(ItemKind.charm, "Charm", "御守り", "", ""));//add
        items.Add(new template(ItemKind.darkness_sword, "Darkness Sword", "ダークネスソード", "", ""));//add
        items.Add(new template(ItemKind.woodshield, "Wooden Shield", "木の盾", "", ""));//add
        items.Add(new template(ItemKind.old_robe, "Old Robe", "古いローブ", "", ""));//add

        /* 必要条件 */
        needs.Add(new template(NeedKind.nothing, "", "", "", ""));
        needs.Add(new template(NeedKind.weapon, "weapon", "weapon", "", ""));
        needs.Add(new template(NeedKind.armor, "armor", "armor", "", ""));
        needs.Add(new template(NeedKind.goods, "goods", "goods", "", ""));

        needs.Add(new template(NeedKind.sword, "sword", "sword", "", ""));
        needs.Add(new template(NeedKind.spear, "spear", "spear", "", ""));
        needs.Add(new template(NeedKind.rod, "rod", "rod", "", ""));
        needs.Add(new template(NeedKind.shield, "shield", "shield", "", ""));
        needs.Add(new template(NeedKind.axe, "axe", "axe", "", ""));

        needs.Add(new template(NeedKind.fire, "fire", "fire", "", ""));
        needs.Add(new template(NeedKind.water, "water", "water", "", ""));
        needs.Add(new template(NeedKind.wind, "wind", "wind", "", ""));
        needs.Add(new template(NeedKind.earth, "earth", "earth", "", ""));
        needs.Add(new template(NeedKind.thunder, "thunder", "thunder", "", ""));
        needs.Add(new template(NeedKind.ice, "ice", "ice", "", ""));
        needs.Add(new template(NeedKind.light, "light", "light", "", ""));
        needs.Add(new template(NeedKind.dark, "dark", "dark", "", ""));

        needs.Add(new template(NeedKind.animal, "animal", "animal", "", ""));
        needs.Add(new template(NeedKind.kiln, "kiln", "kiln", "", ""));
        needs.Add(new template(NeedKind.medic, "medic", "medic", "", ""));
        needs.Add(new template(NeedKind.stall, "stall", "stall", "", ""));

        needs.Add(new template(NeedKind.attack, "attack", "attack", "", ""));
        needs.Add(new template(NeedKind.production, "production", "production", "", ""));
        needs.Add(new template(NeedKind.enhance, "enhance", "enhance", "", ""));
        needs.Add(new template(NeedKind.debuff, "debuff", "debuff", "", ""));

        needs.Add(new template(NeedKind.combo_arts, "combo arts", "combo arts", "", ""));


        /* スキル */
        skills.Add(new template(SkillKind.nothing, "", "", "", ""));
        skills.Add(new template(SkillKind.normalAttack, "Normal Attack", "通常攻撃", "", ""));
        skills.Add(new template(SkillKind.normalAttack_npc1, "Normal Attack", "通常攻撃", "", ""));
        //レベル解放スキル
        skills.Add(new template(SkillKind.punch, "Punch", "パンチ", "", ""));
        skills.Add(new template(SkillKind.impact, "Impact", "インパクト", "", ""));
        skills.Add(new template(SkillKind.throwing, "Throwing", "投てき", "", ""));
        skills.Add(new template(SkillKind.boost, "Boost", "ブースト", "", ""));
        //戦士
        skills.Add(new template(SkillKind.right_slash, "Right Slash", "袈裟斬り", "", ""));
        skills.Add(new template(SkillKind.left_upper_slash, "Left Upper Slash", "左斬上げ", "", ""));
        skills.Add(new template(SkillKind.cooking_sandwich, "Cooking Sandwich", "クッキングサンドイッチ", "", ""));//生産

        skills.Add(new template(SkillKind.stab, "Stab", "突き", "", ""));
        skills.Add(new template(SkillKind.upper_slash, "Upper Slash", "斬上げ", "", ""));
        skills.Add(new template(SkillKind.spearfishing, "Spearfishing", "スピアフィッシング", "", ""));//生産

        skills.Add(new template(SkillKind.left_small_swing, "Left Small Swing", "左小振り", "", ""));
        skills.Add(new template(SkillKind.parry, "Parry", "受け流し", "", ""));
        skills.Add(new template(SkillKind.knead_dough, "Knead Dough", "パンをこねる", "", ""));//生産
        /*skills.Add(new template(SkillKind.helm_splitter, "Helm Splitter", "兜割り", "", ""));
        skills.Add(new template(SkillKind.left_whirlslash, "Left Whirlslash", "左回転斬り", "", ""));
        skills.Add(new template(SkillKind.guard, "Guard", "ガード", "", ""));
        skills.Add(new template(SkillKind.absolute_defense, "Absolute Defense", "絶対防御", "", ""));
        skills.Add(new template(SkillKind.dodge_step, "Dodge Step", "ステップ", "", ""));
        skills.Add(new template(SkillKind.right_spinkick, "Right Spinkick", "右回し蹴り", "", ""));
        */
        //魔導士
        skills.Add(new template(SkillKind.fireball, "Fireball", "ファイアーボール", "", ""));
        skills.Add(new template(SkillKind.hot_body, "Hot Body", "ホットボディ", "", ""));
        skills.Add(new template(SkillKind.create_charcoal, "Create Charcoal", "クリエイト：木炭", "", ""));//生産
        skills.Add(new template(SkillKind.burning_shot, "Burning Shot", "バーニングショット", "", ""));

        skills.Add(new template(SkillKind.waterball, "Waterball", "ウォーターボール", "", ""));
        skills.Add(new template(SkillKind.cure_water, "Cure Water", "キュアウォーター", "", ""));
        skills.Add(new template(SkillKind.catch_fish, "Catch Fish", "キャッチ：魚", "", ""));//生産
        skills.Add(new template(SkillKind.hydro_blade, "Hydro Blade", "ハイドロブレード", "", ""));

        skills.Add(new template(SkillKind.air_cutter, "Air Cutter", "エアーカッター", "", ""));
        skills.Add(new template(SkillKind.wind_step, "Wind Step", "ウインドステップ", "", ""));
        skills.Add(new template(SkillKind.create_firewood, "Create Firewood", "クリエイト：薪", "", ""));//生産
        skills.Add(new template(SkillKind.air_bazooka, "Air Bazooka", "エアーバズーカ", "", ""));

        skills.Add(new template(SkillKind.stone_bullet, "Stone Bullet", "ストーンバレット", "", ""));
        skills.Add(new template(SkillKind.earth_wall, "Earth Wall", "アースウォール", "", ""));
        skills.Add(new template(SkillKind.grow_wheat, "Grow Wheat", "グロウ：小麦", "", ""));//生産
        skills.Add(new template(SkillKind.rock_press, "Rock Press", "ロックプレス", "", ""));

        /*skills.Add(new template(SkillKind.electric_arrow, "Electric Arrow", "エレクトリックアロー", "", ""));
        skills.Add(new template(SkillKind.electric_step, "Electric Step", "エレクトリックステップ", "", ""));
        skills.Add(new template(SkillKind.ice_arrow, "Ice Arrow", "アイスアロー", "", ""));
        skills.Add(new template(SkillKind.ice_shell, "Ice Shell", "アイスシェル", "", ""));
        skills.Add(new template(SkillKind.holy_circle, "Holy Circle", "ホーリーサークル", "", ""));
        skills.Add(new template(SkillKind.dark_wave, "Dark Wave", "ダークウェーブ", "", ""));
        */
        //テイマー
        skills.Add(new template(SkillKind.animal_attack, "Animal Attack", "アニマルアタック", "", ""));
        skills.Add(new template(SkillKind.picking_up_coins, "Picking up Coins", "小銭拾い", "", ""));

        //複合スキル
        skills.Add(new template(SkillKind.flame_slash, "Flame Slash", "火炎斬り", "", ""));//add
        skills.Add(new template(SkillKind.mud_shot, "Mud Shot", "マッドショット", "", ""));//add
        skills.Add(new template(SkillKind.storm_spike, "Storm Spike", "ストームスパイク", "", ""));//add
        skills.Add(new template(SkillKind.animal_rush, "Animal Rush", "アニマルラッシュ", "", ""));//add

        /* 敵 */
        enemys.Add(new template(EnemyKind.nothing, "", "", "", ""));
        //ランク1
        enemys.Add(new template(EnemyKind.slime, "Slime", "スライム", "", ""));
        enemys.Add(new template(EnemyKind.goblin, "Goblin", "ゴブリン", "", ""));
        enemys.Add(new template(EnemyKind.rat, "Rat", "ラット", "", ""));
        enemys.Add(new template(EnemyKind.bird, "Bird", "バード", "", ""));
        enemys.Add(new template(EnemyKind.bat, "Bat", "バット", "", ""));
        enemys.Add(new template(EnemyKind.wolf, "Wolf", "ウルフ", "", ""));
        enemys.Add(new template(EnemyKind.snake, "Snake", "スネーク", "", ""));
        enemys.Add(new template(EnemyKind.demonic, "Demonic", "魔族", "", ""));
        //ランク1人物
        enemys.Add(new template(EnemyKind.sigurd, "Sigurd", "シグルズ", "", ""));
        enemys.Add(new template(EnemyKind.askr, "Askr", "アスク", "", ""));
        enemys.Add(new template(EnemyKind.embla, "Embla", "エムブラ", "", ""));
        //ランク2
        enemys.Add(new template(EnemyKind.red_slime, "Red Slime", "レッドスライム", "", ""));
        enemys.Add(new template(EnemyKind.orc, "Orc", "オーク", "", ""));
        enemys.Add(new template(EnemyKind.poison_rat, "Poison Rat", "ポイズンラット", "", ""));
        enemys.Add(new template(EnemyKind.harpy, "Harpy", "ハーピィ", "", ""));
        enemys.Add(new template(EnemyKind.ghoul, "Ghoul", "グール", "", ""));
        enemys.Add(new template(EnemyKind.werewolf, "Werewolf", "ウェアウルフ", "", ""));
        enemys.Add(new template(EnemyKind.lizard_man, "Lizard Man", "リザードマン", "", ""));
        enemys.Add(new template(EnemyKind.demonic_warrior, "Demonic Warrior", "魔族の戦士", "", ""));
        //ランク3
        /*enemys.Add(new template(EnemyKind.element_slime, "Element Slime", "エレメントスライム", "", ""));
        enemys.Add(new template(EnemyKind.ogre, "Ogre", "オーガ", "", ""));
        enemys.Add(new template(EnemyKind.electric_rat, "Electric Rat", "エレクトリックラット", "", ""));
        enemys.Add(new template(EnemyKind.griffon, "Griffon", "グリフォン", "", ""));
        enemys.Add(new template(EnemyKind.vampire, "Vampire", "ヴァンパイア", "", ""));
        enemys.Add(new template(EnemyKind.hati, "Hati", "ハティ", "", ""));
        enemys.Add(new template(EnemyKind.nidhogg, "Nidhogg", "ニーズヘッグ", "", ""));
        enemys.Add(new template(EnemyKind.demonic_guards, "Demonic Guards", "魔族の近衛兵", "", ""));
        enemys.Add(new template(EnemyKind.angel, "Angel", "エンジェル", "", ""));
        //ランク4
        enemys.Add(new template(EnemyKind.aqua_element,"Aqua Element", "アクアエレメント", "", ""));
        enemys.Add(new template(EnemyKind.troll, "Troll", "トロール", "", ""));
        enemys.Add(new template(EnemyKind.ratatoskr, "Ratatoskr", "ラタトスク", "", ""));
        enemys.Add(new template(EnemyKind.garuda, "Garuda", "ガルーダ", "", ""));
        enemys.Add(new template(EnemyKind.vampire_lord, "Vampire Lord", "ヴァンパイアロード", "", ""));
        enemys.Add(new template(EnemyKind.managarmr, "Mánagarmr", "マーナガルム", "", ""));
        enemys.Add(new template(EnemyKind.fire_drake, "Fire Drake", "ファイアードレイク", "", ""));
        enemys.Add(new template(EnemyKind.satan, "Satan", "魔王", "", ""));
        enemys.Add(new template(EnemyKind.einherjar, "Einherjar", "エインヘリアル", "", ""));
        //ランク4魔界
        enemys.Add(new template(EnemyKind.warg, "Warg", "ワーグ", "", ""));
        enemys.Add(new template(EnemyKind.ice_tortois, "Ice Tortois", "アイスタートル", "", ""));
        //ランク5
        enemys.Add(new template(EnemyKind.skoll, "Sköll", "スコル", "", ""));
        enemys.Add(new template(EnemyKind.dragon_ice, "Dragon Ice", "アイスドラゴン", "", ""));
        enemys.Add(new template(EnemyKind.heidrun, "", "ヘイズルーン", "", ""));
        enemys.Add(new template(EnemyKind.hraesvelgr, "", "フレスベルグ", "", ""));
        enemys.Add(new template(EnemyKind.valkyrie, "", "ワルキューレ", "", ""));
        //ランク5天界
        enemys.Add(new template(EnemyKind.fafnir, "Fafnir", "ファフニール", "", ""));
        enemys.Add(new template(EnemyKind.huginn, "Huginn", "フギン", "", ""));
        enemys.Add(new template(EnemyKind.muninn, "Muninn", "ムニン", "", ""));
        enemys.Add(new template(EnemyKind.geri, "Geri", "ゲリ", "", ""));
        enemys.Add(new template(EnemyKind.freki, "Freki", "フレキ", "", ""));
        enemys.Add(new template(EnemyKind.garm, "Garm", "ガルム", "", ""));
        //ランク6
        enemys.Add(new template(EnemyKind.valkyrja, "Valkyrja", "ヴァルキュリア", "", ""));
        enemys.Add(new template(EnemyKind.heimdall, "Heimdall", "ヘイムダル", "", ""));
        enemys.Add(new template(EnemyKind.sleipnir, "Sleipnir", "スレイプニル", "", ""));
        //ランク6召喚獣
        enemys.Add(new template(EnemyKind.fenrir, "Fenrir", "フェンリル", "", ""));
        enemys.Add(new template(EnemyKind.jormungandr, "Jörmungandr", "ヨルムンガンド", "", ""));
        //ランク7
        enemys.Add(new template(EnemyKind.odin, "Odin", "オーディン", "", ""));
        enemys.Add(new template(EnemyKind.hel, "Hel", "ヘル", "", ""));
        */
        /* ダンジョン */
        dungeons.Add(new template(DungeonKind.nothing, "", "", "", ""));
        dungeons.Add(new template(DungeonKind.edge_of_town, "Edge of Town", "村の外れ", "Called to the bad kids.", "悪ガキを呼び出した。"));
        dungeons.Add(new template(DungeonKind.small_hill, "Small Hill", "小高い丘", "It is said that beautiful flowers are blooming at the top of the hill.", "丘の頂上に綺麗な花が咲いているらしい。"));
        dungeons.Add(new template(DungeonKind.plain, "Plain", "平原", "You need to go over the plains to get to the city", "都市へ向かうには平原を超える必要がある。"));
        dungeons.Add(new template(DungeonKind.lost_forest, "Lostlorn Forest", "迷いの森", "Every year, some people seem to get lost. I feel like I heard it with rumors.", "毎年、そこそこの人がそこそこ迷うらしい。と、噂で聞いた気がする。"));
        dungeons.Add(new template(DungeonKind.oak_forest, "Oak Forest", "オークの森", "Orc doesn't live here.", "楢の森。オークは住んでいない。"));
        dungeons.Add(new template(DungeonKind.moor, "Moor", "湿原", "Wetlands beyond the Oak Forest", "オークの森を抜けた先にある湿原"));
        dungeons.Add(new template(DungeonKind.hoarding_house, "Hoarding House", "ゴミ屋敷", "", "都内で噂のゴミ屋敷。"));
        dungeons.Add(new template(DungeonKind.sewer, "Sewer", "下水道", "Stink.", "むぅ、臭い。"));
        dungeons.Add(new template(DungeonKind.bog, "Bog", "沼地", "Let's proceed with caution of your footing.", "足場に気を付けて進もう。"));
        dungeons.Add(new template(DungeonKind.demonic_cellar, "Demonic Cellar", "魔族のあなぐら", "There was a basement under the hoarding house.", "ゴミ屋敷の下に地下室があった。"));

        /* 味方 */
        allys.Add(new template(AllyKind.nothing, "", "", "", ""));
        allys.Add(new template(AllyKind.npcA, "Norn", "ノルン", "", ""));
        allys.Add(new template(AllyKind.npcB, "Stela", "ステラ", "", ""));
        allys.Add(new template(AllyKind.npcB, "Kashima", "鹿島", "", ""));

        /* スキルの属性 */
        attributes.Add(new template(AttributeKind.nothing, "", "", "", ""));
        attributes.Add(new template(AttributeKind.fireMagic, "Fire Magic", "火炎魔法", "", ""));
        attributes.Add(new template(AttributeKind.waterSword, "Water Sword", "火炎斬り", "", ""));

        /* 要素 */
        elements.Add(new template(ElementKind.nothing, "", "", "", ""));
        elements.Add(new template(ElementKind.main, "Main", "メイン", "", ""));
        elements.Add(new template(ElementKind.ability, "アビリティ", "", "", ""));
        elements.Add(new template(ElementKind.item, "Item", "アイテム", "", ""));
        elements.Add(new template(ElementKind.skill, "Skill", "スキル", "", ""));
        elements.Add(new template(ElementKind.dungeon, "Dungeon", "ダンジョン", "", ""));
        elements.Add(new template(ElementKind.status, "Status", "ステータス", "", ""));

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
        TestOneList<SkillKind>(skills);
        TestOneList<EnemyKind>(enemys);
        TestOneList<DungeonKind>(dungeons);
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
