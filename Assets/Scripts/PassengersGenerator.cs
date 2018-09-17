using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersGenerator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private GameObject defaultPassengerPrefab;
    [SerializeField] private GameObject coatPassengerPrefab;
    [SerializeField] private GameObject itemsPassengerPrefab;
    [SerializeField] private GameObject ultimatePassengerPrefab;

    [SerializeField] private int maxPassengersOnScreenCount = 10;
    [SerializeField] private List<GameObject> passengersOnScreen;
    [SerializeField] private List<Vector3> passengersSpawnPointsPositions;
    private Vector3 firstPassengerPosition;

    private void Start()
    {
        GeneratePassengersSpawnTransforms();

        GeneratePassengersQueue(defaultPassengerPrefab);
    }

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
            defaultPassengerPrefab.transform.position.y, playerTransform.position.z - 5);

        passengersSpawnPointsPositions.Add(firstPassengerPosition);

        for (int i = 1; i < maxPassengersOnScreenCount; i++)
        {
            int last = passengersSpawnPointsPositions.Count - 1;

            passengersSpawnPointsPositions.Add(new Vector3(passengersSpawnPointsPositions[last].x,
                passengersSpawnPointsPositions[last].y, passengersSpawnPointsPositions[last].z - 2));
        }
    }
}