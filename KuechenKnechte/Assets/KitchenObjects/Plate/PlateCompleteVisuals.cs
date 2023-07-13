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

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += AddIngredient;
        plateKitchenObject.OnClear += ClearIngredients;
        ClearIngredients();
    }

    private void AddIngredient(KitchenObjectSO addedKitchenObjectSO)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == addedKitchenObjectSO) kitchenObjectSO_GameObject.gameObject.SetActive(true);
        }
    }

    private void ClearIngredients()
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }
}
