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
        defaultScannerRotation = gameObject.transform.rotation;
        foreach (var collider in GetComponentsInChildren<BoxCollider>())
        {
            Physics.IgnoreCollision(playerController, collider);
        }
    }

    #endregion

    [SerializeField] private CharacterController playerController;
    [SerializeField] private BoxCollider tableStabilizerCollider;
    [SerializeField] private Vector3 defaultScannerPosition;
    [SerializeField] private Quaternion defaultScannerRotation;

    public bool isTaken;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider == tableStabilizerCollider)
        {
            if (!isTaken)
            {
                GameplayUI.Instance.DisplayMessage("Take the Scanner from the table on the right.", 26,
                    Color.cyan);
            }
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.collider == tableStabilizerCollider)
        {
            if (!isTaken)
            {
                // Reset
                gameObject.transform.position = defaultScannerPosition;
                gameObject.transform.rotation = defaultScannerRotation;
            }
        }
    }
}