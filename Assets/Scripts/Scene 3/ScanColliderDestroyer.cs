using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanColliderDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ScanHead")
        {
            var scanPassenger = GetComponentInParent<Transform>().gameObject.GetComponentInParent<ScanPassenger>();
            scanPassenger.DeleteColliderFromList(GetComponent<BoxCollider>());

            // Final for current passenger
            if (scanPassenger.IsNoColliders())
            {
                GameplayUI.Instance.DisplayMessage("Success!", 26,
                    Color.green);
                GameplayUI.Instance.DisplayGatesMessage("Success!");
                PassengersCounter.Counter++;
                StartCoroutine(PassengersGeneratorThree.Instance.MoveToPlayerRoutine());
            }

            gameObject.SetActive(false);
        }
    }
}