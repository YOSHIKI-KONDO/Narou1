using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class UICtrl : BASE {
    public Text apText;
    public Text inventoryText;
    public Text equipText;
    public Slider inventorySlider, equipSlider;
    //public Text ipText1, ipText2, ipText3, ipText4, ipText5;
    public Toggle mainToggle, abilityToggle, itemToggle, skillToggle, dungeonToggle, statusToggle, optionToggle;
    public GameObject new_main, new_ability, new_item, new_skill, new_dungeon, new_status, new_option, new_shop, new_inventory;
    public GameObject skill_menu, shopButtonDecoy, inventoryButtonDecoy;

    public GameObject thankPanel;

	// Use this for initialization
	void Awake () {
		StartBASE();

        mainToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(mainToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.main], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.main], x),
            (x) => { return false; },
            null, null, "");

        //一旦変数に代入
        var abilityRelease = abilityToggle.gameObject.AddComponent<ReleaseFunction>();
        abilityRelease.StartFunction(abilityToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.ability], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.ability], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.ability], x),
            new_ability, "Ability");
        abilityRelease.action_activated = () =>
        {
            main.announce.Add("<b><i>Start training! And let's listen to my father.</i></b>");
        };

        var itemRelease = itemToggle.gameObject.AddComponent<ReleaseFunction>();
        itemRelease.StartFunction(itemToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.item], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.item], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.item], x),
            new_item, "Item");
        itemRelease.action_activated = () =>
        {
            main.announce.Add("<b><i>Equip items! Items purchased at the shop can be effective by equipping them.</i></b>");
            main.announce.Add("<b><i>Also, new items are released by upgrade actions and release of resources.</i></b>");
        };

        var skillRelease = skillToggle.gameObject.AddComponent<ReleaseFunction>();
        skillRelease.StartFunction(skillToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.skill], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.skill], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.skill], x),
            new_skill, "Skill");
        skillRelease.action_activated = () =>
        {
            main.announce.Add("<b><i>Set your skills in slots! Set your attack skills in the dungeon and challenge.</i></b>");
            main.announce.Add("<b><i>Production skills can always be looped by unchecking the dungeon only check.</i></b>");
        };

        dungeonToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(dungeonToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.dungeon], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.dungeon], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.dungeon], x),
            new_dungeon, "Dungeon");

        statusToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(statusToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.status], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.status], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.status], x),
            new_status, "");

       optionToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(optionToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.option], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.option], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.option], x),
            new_option, "");


        skill_menu.AddComponent<ReleaseFunction>().StartFunction(skill_menu,
            x => Sync(ref main.SR.released_element[(int)ElementKind.skill], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.skill], x),
            (x) => { return false; },
            null, null, "");

        shopButtonDecoy.AddComponent<ReleaseFunction>().StartFunction(shopButtonDecoy,
            x => Sync(ref main.SR.released_ShopButton_Decoy, x),
            x => Sync(ref main.SR.completed_ShopButton_Decoy, x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_ShopButton, x),
            new_shop, "");

        inventoryButtonDecoy.AddComponent<ReleaseFunction>().StartFunction(inventoryButtonDecoy,
           x => Sync(ref main.SR.released_InventoryButton_Decoy, x),
           x => Sync(ref main.SR.completed_InventoryButton_Decoy, x),
           (x) => { return false; },
           (x) => Sync(ref main.SR.watched_InventoryButton, x),
           new_inventory, "");
    }

    private void FixedUpdate()
    {
        apText.text = "Ability Point : " + tDigit(main.rsc.Value[(int)ResourceKind.ap]);
        equipText.text = main.rsc.Value[(int)ResourceKind.equipSpace].ToString() + " / " + main.rsc.Max((int)ResourceKind.equipSpace).ToString();
        inventoryText.text = main.rsc.Value[(int)ResourceKind.inventorySpace].ToString() + " / " + main.rsc.Max((int)ResourceKind.inventorySpace).ToString();
        equipSlider.value = (float)(main.rsc.Value[(int)ResourceKind.equipSpace] / main.rsc.Max((int)ResourceKind.equipSpace));
        inventorySlider.value = (float)(main.rsc.Value[(int)ResourceKind.inventorySpace] / main.rsc.Max((int)ResourceKind.inventorySpace));
        //ipText1.text = "Item point1:" + main.rsc.Value[(int)ResourceKind.itemPoint1].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint1).ToString();
        //ipText2.text = "Item point2:" + main.rsc.Value[(int)ResourceKind.itemPoint2].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint2).ToString();
        //ipText3.text = "Item point3:" + main.rsc.Value[(int)ResourceKind.itemPoint3].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint3).ToString();
        //ipText4.text = "Item point4:" + main.rsc.Value[(int)ResourceKind.itemPoint4].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint4).ToString();
        //ipText5.text = "Item point5:" + main.rsc.Value[(int)ResourceKind.itemPoint5].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint5).ToString();

        //focus NOTE:なぜrelease functionを外してまで別の処理にするかは不明
        //if(main.focus.gameObject.activeSelf == false)
        //{
        //    main.focus.Activate();
        //}
    }
}
