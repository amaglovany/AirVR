using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    #region Singleton

    public static Scanner Instance;

    private void Awake()
    {
        Instance = this;

        defaultScannerPosition = gameObject.transform.position;
        foreach (var collider in GetComponentsInChildren<BoxCollider>())
        {
            Physics.IgnoreCollision(playerController, collider);
        }
    }

    #endregion

    [SerializeField] private CharacterController playerController;
    [SerializeField] private BoxCollider tableStabilizerCollider;
    [SerializeField] private Vector3 defaultScannerPosition;

    public bool isTaken;

    private void OnTriggerEnter(Collider other)
    {
    }

    private void OnCollisionStay(Collision other)
    {
    }
}