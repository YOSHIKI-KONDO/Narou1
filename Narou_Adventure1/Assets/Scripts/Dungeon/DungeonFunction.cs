﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class DungeonFunction : OnlyAction {
    public void AwakeDungeon(Button button, string actionName, GameObject isOnObj)
    {
        AwakeOnlyAction(button, isOnObj, actionName, true);
    }
}
