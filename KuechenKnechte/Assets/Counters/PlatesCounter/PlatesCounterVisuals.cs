using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisuals : MonoBehaviour
{
    [SerializeField] private Transform visualsParentPoint;
    [SerializeField] private GameObject plateVisual;
    [SerializeField] private PlatesCounter platesCounter;

    private List<GameObject> plateVisuals;
    private float visualOffset = 0.1f;

    private void Awake()
    {
        plateVisuals = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateCountChanged += UpdateVisuals;
    }

    private void UpdateVisuals(int newPlateCount)
    {
        while (newPlateCount != plateVisuals.Count)
        {
            if (newPlateCount > plateVisuals.Count) SpawnPlate();
            if (newPlateCount < plateVisuals.Count) DeletePlate();
        }
    }

    private void SpawnPlate()
    {
        GameObject newPlate = Instantiate(plateVisual, visualsParentPoint);
        newPlate.transform.localPosition = new Vector3(0f, plateVisuals.Count * visualOffset, 0f);
        plateVisuals.Add(newPlate);
    }

    private void DeletePlate()
    {
        GameObject plate = plateVisuals[plateVisuals.Count - 1];
        plateVisuals.Remove(plate);
        Destroy(plate);
    }
}
