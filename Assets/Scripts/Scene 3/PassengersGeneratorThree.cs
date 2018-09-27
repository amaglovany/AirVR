using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersGeneratorThree : MonoBehaviour
{
    #region Singleton

    public static PassengersGeneratorThree Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private Transform playerTransform;

    public GameObject scanPassengerPrefab;

    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material yellowMaterial;

    private void Start()
    {
        GeneratePassengersSpawnTransforms();
        GeneratePassengersQueue();

        StartCoroutine(MoveToPlayerRoutine());
    }

    public IEnumerator MoveToPlayerRoutine()
    {
        Destroy(passengersOnScreen[0]);
        passengersOnScreen.RemoveAt(0);

        foreach (var passenger in passengersOnScreen)
        {
            passenger.GetComponent<Animator>().Play("ScanPassengerMoveAnimation");
        }

        yield return new WaitForSeconds(1f);

        var newPassenger = Instantiate(PassengerWithRandomMaterial(), gameObject.transform);
        newPassenger.transform.position = passengersSpawnPointsPositions[passengersSpawnPointsPositions.Count - 1];
        passengersOnScreen.Add(newPassenger);
    }

    public GameObject ReturnFirstPassenger()
    {
        return passengersOnScreen[0];
    }

    #region Starting Generating and Spawning

    [SerializeField] private int maxPassengersOnScreenCount = 10;
    [SerializeField] private List<GameObject> passengersOnScreen;
    [SerializeField] private List<Vector3> passengersSpawnPointsPositions;
    private Vector3 firstPassengerPosition;

    private void GeneratePassengersQueue()
    {
        for (int i = 0; i < maxPassengersOnScreenCount; i++)
        {
            var addedPassenger = Instantiate(PassengerWithRandomMaterial(), gameObject.transform);

            addedPassenger.transform.position = passengersSpawnPointsPositions[i];
            passengersOnScreen.Add(addedPassenger);
        }
    }

    private int globalSeed = 0;

    private GameObject PassengerWithRandomMaterial()
    {
        GameObject passengerGO = scanPassengerPrefab;
        Material appliedMaterial = null;

        int localSeed = 0;

        // To prevent same prefabs in queue (Higher variety)
        do
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    appliedMaterial = redMaterial;
                    localSeed = 1;
                    break;
                case 2:
                    appliedMaterial = blueMaterial;
                    localSeed = 2;
                    break;
                case 3:
                    appliedMaterial = greenMaterial;
                    localSeed = 3;
                    break;
                case 4:
                    appliedMaterial = yellowMaterial;
                    localSeed = 4;
                    break;
                default:
                    Debug.Log("Check random generated numbers");
                    break;
            }
        } while (localSeed == globalSeed);

        globalSeed = localSeed;
        if (appliedMaterial != null)
        {
            passengerGO.GetComponent<MeshRenderer>().material = appliedMaterial;
        }

        return passengerGO;
    }

    private void GeneratePassengersSpawnTransforms()
    {
        firstPassengerPosition = new Vector3(playerTransform.position.x,
            scanPassengerPrefab.transform.position.y, playerTransform.position.z - 1.6f);

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