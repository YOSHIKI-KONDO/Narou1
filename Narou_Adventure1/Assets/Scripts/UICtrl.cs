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
    public Text ipText1, ipText2, ipText3, ipText4, ipText5;
    public Toggle mainToggle, abilityToggle, itemToggle, skillToggle, dungeonToggle, statusToggle;
    public GameObject skill_menu;

	// Use this for initialization
	void Awake () {
		StartBASE();

        mainToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(mainToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.main], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.main], x),
            (x) => { return false; },
            null, null);

        abilityToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(abilityToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.ability], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.ability], x),
            (x) => { return false; },
            null, null);

        itemToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(itemToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.item], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.item], x),
            (x) => { return false; },
            null, null);

        skillToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(skillToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.skill], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.skill], x),
            (x) => { return false; },
            null, null);

        dungeonToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(dungeonToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.dungeon], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.dungeon], x),
            (x) => { return false; },
            null, null);

        statusToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(statusToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.status], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.status], x),
            (x) => { return false; },
            null, null);


        skill_menu.AddComponent<ReleaseFunction>().StartFunction(skill_menu,
            x => Sync(ref main.SR.released_element[(int)ElementKind.skill], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.skill], x),
            (x) => { return false; },
            null, null);
    }

    private void FixedUpdate()
    {
        apText.text = "AP : " + tDigit(main.rsc.Value[(int)ResourceKind.ap], 2);
        equipText.text = "Equipment:" + main.rsc.Value[(int)ResourceKind.equipSpace].ToString() + " / " + main.rsc.Max((int)ResourceKind.equipSpace).ToString();
        inventoryText.text = "Inventory:" + main.rsc.Value[(int)ResourceKind.inventorySpace].ToString() + " / " + main.rsc.Max((int)ResourceKind.inventorySpace).ToString();
        ipText1.text = "Item point1:" + main.rsc.Value[(int)ResourceKind.itemPoint1].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint1).ToString();
        ipText2.text = "Item point2:" + main.rsc.Value[(int)ResourceKind.itemPoint2].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint2).ToString();
        ipText3.text = "Item point3:" + main.rsc.Value[(int)ResourceKind.itemPoint3].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint3).ToString();
        ipText4.text = "Item point4:" + main.rsc.Value[(int)ResourceKind.itemPoint4].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint4).ToString();
        ipText5.text = "Item point5:" + main.rsc.Value[(int)ResourceKind.itemPoint5].ToString() + " / " + main.rsc.Max((int)ResourceKind.itemPoint5).ToString();
    }
}
