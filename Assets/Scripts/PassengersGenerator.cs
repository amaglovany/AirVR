using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PassengersGenerator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private GameObject defaultPassengerPrefab;
    [SerializeField] private GameObject coatPassengerPrefab;
    [SerializeField] private GameObject itemsPassengerPrefab;
    [SerializeField] private GameObject ultimatePassengerPrefab;

    private void Start()
    {
        GeneratePassengersSpawnTransforms();
        GeneratePassengersQueue(defaultPassengerPrefab);

        StartCoroutine(MoveToPlayerRoutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerCanPass)
        {
            PassengersCounter.Counter++;
            StartCoroutine(MoveToPlayerRoutine());
        }
    }

    public bool playerCanPass;

    private IEnumerator MoveToPlayerRoutine()
    {
        Destroy(passengersOnScreen[0]);
        passengersOnScreen.RemoveAt(0);

        foreach (var passenger in passengersOnScreen)
        {
            passenger.GetComponent<Animator>().Play("PassengerMoveAnimation");
        }

        yield return new WaitForSeconds(0.5f);

        var newPassenger = Instantiate(defaultPassengerPrefab, gameObject.transform);
        newPassenger.transform.position = passengersSpawnPointsPositions[passengersSpawnPointsPositions.Count - 1];
        passengersOnScreen.Add(newPassenger);
    }

    #region Starting Generating and Spawning

    [SerializeField] private int maxPassengersOnScreenCount = 10;
    [SerializeField] private List<GameObject> passengersOnScreen;
    [SerializeField] private List<Vector3> passengersSpawnPointsPositions;
    private Vector3 firstPassengerPosition;

    private void GeneratePassengersQueue(GameObject passengerGO)
    {
        for (int i = 0; i < maxPassengersOnScreenCount; i++)
        {
            var addedPassenger = Instantiate(passengerGO, gameObject.transform);

            addedPassenger.transform.position = passengersSpawnPointsPositions[i];
            passengersOnScreen.Add(addedPassenger);
        }
    }

    private void GeneratePassengersSpawnTransforms()
    {
        firstPassengerPosition = new Vector3(playerTransform.position.x,
            defaultPassengerPrefab.transform.position.y, playerTransform.position.z - 2.5f);

        passengersSpawnPointsPositions.Add(firstPassengerPosition);

        for (int i = 1; i < maxPassengersOnScreenCount; i++)
        {
            int last = passengersSpawnPointsPositions.Count - 1;

            passengersSpawnPointsPositions.Add(new Vector3(passengersSpawnPointsPositions[last].x,
                passengersSpawnPointsPositions[last].y, passengersSpawnPointsPositions[last].z - 2.5f));
        }
    }

    #endregion
}