using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;
using VRTK.Examples;

public class PlayerControlHandlerOne : MonoBehaviour
{
    [SerializeField] private PassengersGeneratorOne _passengersGeneratorOne;

    private void Awake()
    {
        _passengersGeneratorOne = gameObject.transform.parent.gameObject.GetComponent<PassengersGeneratorOne>();
    }

    private void EnablePlayerControl()
    {
        _passengersGeneratorOne.playerCanConfirm = true;
        GameplayUI.Instance.DisplayMessage("Press any Trigger to confirm the passenger", 0.2f, Color.white);
    }

    private void DisablePlayerControl()
    {
        _passengersGeneratorOne.playerCanConfirm = false;
        GameplayUI.Instance.DisplayMessage(string.Empty);
    }
}