using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PassengersCounter
{
    private static int counter;

    public static int Counter
    {
        get { return counter; } 
        set
        {
            counter = value;
            GameplayUI.Instance.UpdateCounterText();
        }
    }
}