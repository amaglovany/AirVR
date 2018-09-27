using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanColliderDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Scanner" || other.gameObject.name == "Holder")
        {
            var scanPassenger = GetComponentInParent<Transform>().gameObject.GetComponentInParent<ScanPassenger>();
            //scanPassenger.DeleteColliderFromList(GetComponent<BoxCollider>());
            //ToDo: Dance here
            //if (scanPassenger.IsLast())
            Destroy(gameObject);
        }
    }
}