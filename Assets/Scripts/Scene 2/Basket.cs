using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    #region Singleton

    public static Basket Instance;

    private void Awake()
    {
        Instance = this;

        defaultBasketPosition = gameObject.transform.position;
        foreach (var collider in GetComponentsInChildren<BoxCollider>())
        {
            Physics.IgnoreCollision(playerController, collider);
        }
    }

    #endregion

    [SerializeField] private CharacterController playerController;
    [SerializeField] private BoxCollider gatesCheckTrigger;
    [SerializeField] private BoxCollider tableStabilizerCollider;
    [SerializeField] private Vector3 defaultBasketPosition;
    [SerializeField] private List<string> contentList;

    public void Add(string item)
    {
        contentList.Add(item);
    }

    public void Clear()
    {
        contentList.Clear();
    }

    public bool isTaken;
    public bool beenChecked;

    private void OnTriggerEnter(Collider other)
    {
        if (other == gatesCheckTrigger && !beenChecked)
        {
            GameplayUI.Instance.DisplayGatesMessage("Success!");
            GameplayUI.Instance.DisplayMessage("Success! Put the Basket on the desk, and check next passenger.", 26,
                Color.green);
            beenChecked = true;
            PassengersCounter.Counter++;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider == tableStabilizerCollider)
        {
            if (!isTaken)
            {
                // Reset
                gameObject.transform.position = defaultBasketPosition;
                if (beenChecked)
                {
                    Clear();
                    beenChecked = false;

                    StartCoroutine(PassengersGeneratorTwo.Instance.MoveToPlayerRoutine());
                    GameplayUI.Instance.DisplayGatesMessage("Waiting...");
                }
            }
        }
    }
}