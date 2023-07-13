using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisuals : MonoBehaviour
{

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjects;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private GameObject iconCanvas;

    private List<GameObject> icons;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += AddIngredient;
        plateKitchenObject.OnClear += ClearIngredients;
        icons = new List<GameObject>();
        ClearIngredients();
    }

    private void AddIngredient(KitchenObjectSO addedKitchenObjectSO)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == addedKitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
        GameObject newIcon = Instantiate(iconPrefab, iconCanvas.transform);
        newIcon.GetComponent<Icon>().SetKitchenObjectSO(addedKitchenObjectSO);
        icons.Add(newIcon);
    }

    private void ClearIngredients()
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }

        foreach (GameObject icon in icons)
        {
            Destroy(icon);
        }
        icons.Clear();
    }
}
