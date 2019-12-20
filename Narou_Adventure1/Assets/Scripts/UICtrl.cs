using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class UICtrl : BASE {
    public Text apText;
    public Text inventoryText;
    public Toggle mainToggle, abilityToggle, itemToggle, skillToggle, dungeonToggle, statusToggle;
    public GameObject skill_menu;

	// Use this for initialization
	void Awake () {
		StartBASE();

        mainToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(mainToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.main], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.main], x),
            (x) => { return false; });

        abilityToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(abilityToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.ability], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.ability], x),
            (x) => { return false; });

        itemToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(itemToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.item], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.item], x),
            (x) => { return false; });

        skillToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(skillToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.skill], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.skill], x),
            (x) => { return false; });

        dungeonToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(dungeonToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.dungeon], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.dungeon], x),
            (x) => { return false; });

        statusToggle.gameObject.AddComponent<ReleaseFunction>().StartFunction(statusToggle.gameObject,
            x => Sync(ref main.SR.released_element[(int)ElementKind.status], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.status], x),
            (x) => { return false; });


        skill_menu.AddComponent<ReleaseFunction>().StartFunction(skill_menu,
            x => Sync(ref main.SR.released_element[(int)ElementKind.skill], x),
            x => Sync(ref main.SR.completed_element[(int)ElementKind.skill], x),
            (x) => { return false; });
    }

    private void FixedUpdate()
    {
        apText.text = "AP : " + tDigit(main.rsc.Value[(int)ResourceKind.ap], 2);
        inventoryText.text = "Equipment:" + main.rsc.Value[(int)ResourceKind.equipSpace].ToString() + " / " + main.rsc.Max((int)ResourceKind.equipSpace).ToString() +
            ",  Inventory:" + main.rsc.Value[(int)ResourceKind.inventorySpace].ToString() + " / " + main.rsc.Max((int)ResourceKind.inventorySpace).ToString();
    }
}
