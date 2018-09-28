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

            if (!Scanner.Instance.isTaken)
            {
                GameplayUI.Instance.DisplayMessage("Take the Scanner from the table on the right.", 26,
                    Color.cyan);
            }
            else
            {
                GameplayUI.Instance.DisplayMessage("Scan the passenger in front.", 26,
                    Color.yellow);
            }

            GameplayUI.Instance.DisplayGatesMessage("Waiting...");
        }
    }

    public void DeleteColliderFromList(BoxCollider boxCollider)
    {
        scanColliders.RemoveAt(scanColliders.IndexOf(boxCollider));
    }

    public bool IsNoColliders()
    {
        int countInList = 0;
        foreach (var collider in scanColliders)
        {
            if (collider != null)
            {
                countInList++;
            }
        }

        return countInList == 0;
    }
}