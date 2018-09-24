using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameplayUI.Instance.DisplayMessage("Press \"SPACE\" to confirm the passenger", 26f, Color.white);
    }

    private void DisablePlayerControl()
    {
        _passengersGeneratorOne.playerCanConfirm = false;
        GameplayUI.Instance.DisplayMessage(string.Empty);
    }
}