using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersGeneratorTwo : MonoBehaviour
{
    #region Singleton

    public static PassengersGeneratorTwo Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private Transform playerTransform;

    public GameObject emptyPassengerPrefab;
    public GameObject coatPassengerPrefab;
    public GameObject itemsPassengerPrefab;
    public GameObject itemsAndCoatPassengerPrefab;

    private void Start()
    {
        GeneratePassengersSpawnTransforms();
        GeneratePassengersQueue();

        StartCoroutine(MoveToPlayerRoutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerCanConfirm)
        {
            // "Everything Okay" Logic
            PassengersCounter.Counter++;
            StartCoroutine(MoveToPlayerRoutine());
        }
    }

    public bool playerCanConfirm;

    private IEnumerator MoveToPlayerRoutine()
    {
        Destroy(passengersOnScreen[0]);
        passengersOnScreen.RemoveAt(0);

        foreach (var passenger in passengersOnScreen)
        {
            passenger.GetComponent<Animator>().Play("RandomPassengerMoveAnimation");
            // ToDO: Add new anims to them
        }

        yield return new WaitForSeconds(1f);

        var newPassenger = Instantiate(RandomPassenger(), gameObject.transform);
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
            var addedPassenger = Instantiate(RandomPassenger(), gameObject.transform);

            addedPassenger.transform.position = passengersSpawnPointsPositions[i];
            passengersOnScreen.Add(addedPassenger);
        }
    }

    private int globalSeed = 0;

    private GameObject RandomPassenger()
    {
        GameObject passengerGO = null;

        int localSeed = 0;

        // To prevent same prefabs in queue (Higher variety)
        do
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    passengerGO = coatPassengerPrefab;
                    localSeed = 1;
                    break;
                case 2:
                    passengerGO = itemsPassengerPrefab;
                    localSeed = 2;
                    break;
                case 3:
                    passengerGO = itemsAndCoatPassengerPrefab;
                    localSeed = 3;
                    break;
                default:
                    Debug.Log("Check random generated numbers");
                    break;
            }
        } while (localSeed == globalSeed);

        globalSeed = localSeed;

        return passengerGO;
    }

    private void GeneratePassengersSpawnTransforms()
    {
        firstPassengerPosition = new Vector3(playerTransform.position.x,
            emptyPassengerPrefab.transform.position.y, playerTransform.position.z - 3f);

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