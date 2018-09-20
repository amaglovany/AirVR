using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    #region Singleton

    public static GameplayUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private TextMeshProUGUI counterText;

    public void UpdateCounterText()
    {
        counterText.text = "Count: " + PassengersCounter.Counter;
    }

    public void DisplayMessage(string messageText)
    {
        hintText.text = messageText;
    }

    public void DisplayMessage(string messageText, float fontSize)
    {
        hintText.text = messageText;
        hintText.fontSize = fontSize;
    }

    public void DisplayMessage(string messageText, float fontSize, Color32 textColor)
    {
        hintText.text = messageText;
        hintText.fontSize = fontSize;
        hintText.color = textColor;
    }
}