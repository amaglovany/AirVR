using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanPassenger : MonoBehaviour
{
    [SerializeField] private BoxCollider[] checkColliders;

    private void Start()
    {
        SetCheckColliders();
    }

    public void SetCheckColliders()
    {
    }

    private bool IsFirstInQueue()
    {
        return gameObject == PassengersGeneratorThree.Instance.ReturnFirstPassenger();
    }


    private void PutEverythingIntoBasket()
    {
        if (IsFirstInQueue())
        {
        }
    }
}