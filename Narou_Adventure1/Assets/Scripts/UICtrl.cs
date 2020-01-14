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
    public GameObject new_main, new_ability, new_item, new_skill, new_dungeon, new_status, new_option;
    public GameObject skill_menu;

    public GameObject thankPanel;

	// Use this for initialization
	void Awake () {
		StartBASE();

        mainToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(mainToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.main], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.main], x),
            (x) => { return false; },
            null, null, "");

        abilityToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(abilityToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.ability], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.ability], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.ability], x),
            new_ability, "Ability");

        itemToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(itemToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.item], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.item], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.item], x),
            new_item, "Item");

        skillToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(skillToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.skill], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.skill], x),
            (x) => { return false; },
            (x) => Sync(ref main.SR.watched_element[(int)ElementKind.skill], x),
            new_skill, "Skill");

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
    }

    private void FixedUpdate()
    {
        apText.text = "AP : " + tDigit(main.rsc.Value[(int)ResourceKind.ap], 2);
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
