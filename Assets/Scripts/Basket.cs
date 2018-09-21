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
    }

    #endregion

    [SerializeField] private Transform playerViewFront;
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

    private void Update()
    {
        if (isTaken)
        {
            
        }
    }
}