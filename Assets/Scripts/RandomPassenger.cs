using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPassenger : MonoBehaviour
{
    public enum ContentType
    {
        Empty,
        Coat,
        Items,
        ItemsAndCoat
    }

    public ContentType content = ContentType.Empty;

    private void Start()
    {
        SetContent();
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
            content = ContentType.Items;
        }

        if (passengerName == PassengersGeneratorTwo.Instance.itemsAndCoatPassengerPrefab.name)
        {
            content = ContentType.ItemsAndCoat;
        }
    }

    private bool IsFirstInQueue()
    {
        return gameObject == PassengersGeneratorTwo.Instance.ReturnFirstPassenger();
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
        }
    }
}