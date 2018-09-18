using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlHandler : MonoBehaviour
{
    [SerializeField] private PassengersGenerator _passengersGenerator;

    private void Awake()
    {
        _passengersGenerator = gameObject.transform.parent.gameObject.GetComponent<PassengersGenerator>();
    }

    private void EnablePlayerControl()
    {
        _passengersGenerator.playerCanPass = true;
        GameplayUI.Instance.DisplayMessage("Press \"SPACE\" to confirm the passenger", 26f, Color.white);
    }

    private void DisablePlayerControl()
    {
        _passengersGenerator.playerCanPass = false;
        GameplayUI.Instance.DisplayMessage(string.Empty);
    }
}