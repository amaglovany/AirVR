using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanPassenger : MonoBehaviour
{
    [SerializeField] private List<BoxCollider> scanColliders;

    private void Start()
    {
    }

    public void DisableScanColliders()
    {
        foreach (var collider in scanColliders)
        {
            collider.gameObject.SetActive(false);
        }
    }

    private bool IsFirstInQueue()
    {
        return gameObject == PassengersGeneratorThree.Instance.ReturnFirstPassenger();
    }


    private void EnableScanCollidersOnFirst()
    {
        if (IsFirstInQueue())
        {
            foreach (var collider in scanColliders)
            {
                collider.gameObject.SetActive(true);
            }
        }
    }

    public void DeleteColliderFromList(BoxCollider boxCollider)
    {
        foreach (var collider in scanColliders)
        {
            if (collider == boxCollider)
            {
                scanColliders.Remove(collider);
            }
        }
    }

    public bool IsLast()
    {
        return true;
    }
}