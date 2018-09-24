using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPassenger : MonoBehaviour
{
    private PlayerControlHandlerTwo _playerControlHandlerTwo;

    public enum ContentType
    {
        Empty,
        Coat,
        Stuff,
        StuffAndCoat
    }

    public ContentType content = ContentType.Empty;

    private void Start()
    {
        SetContent();
        _playerControlHandlerTwo = FindObjectOfType<PlayerControlHandlerTwo>();
    }

    public void SetContent()
    {
        string passengerName = gameObject.name.Replace("(Clone)", string.Empty);

        if (passengerName == PassengersGeneratorTwo.Instance.emptyPassengerPrefab.name)
        {
            content = ContentType.Empty;
        }

        if (passengerName == PassengersGeneratorTwo.Instance.coatPassengerPrefab.name)
        {
            content = ContentType.Coat;
        }

        if (passengerName == PassengersGeneratorTwo.Instance.itemsPassengerPrefab.name)
        {
            content = ContentType.Stuff;
        }

        if (passengerName == PassengersGeneratorTwo.Instance.itemsAndCoatPassengerPrefab.name)
        {
            content = ContentType.StuffAndCoat;
        }
    }

    private bool IsFirstInQueue()
    {
        return gameObject == PassengersGeneratorTwo.Instance.ReturnFirstPassenger();
    }

    private void DisableBasketTake()
    {
        _playerControlHandlerTwo.enabled = false;
    }

    private void PutEverythingIntoBasket()
    {
        if (IsFirstInQueue())
        {
            // "Undress" passenger
            // Visually
            gameObject.GetComponent<Renderer>().material = PassengersGeneratorTwo.Instance.emptyPassengerPrefab
                .GetComponent<Renderer>().sharedMaterial;

            // Logically: And put into basket
            // Working with simple data for now
            Basket.Instance.Add(content.ToString());

            GameplayUI.Instance.DisplayMessage("Item \"" + content + "\" is in the Basket. Press \'E\' to take.", 26,
                Color.cyan);
            GameplayUI.Instance.DisplayGatesMessage("Waiting...");

            _playerControlHandlerTwo.enabled = true;
        }
    }
}