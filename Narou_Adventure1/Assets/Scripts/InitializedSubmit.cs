using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializedSubmit : MonoBehaviour
{
    public void Submit()
    {
        Application.ExternalCall("kongregate.stats.submit", "initialized", 1);
    }
}
