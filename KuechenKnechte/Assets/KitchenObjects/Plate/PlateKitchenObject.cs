using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event Action<KitchenObjectSO> OnIngredientAdded;
    public event Action OnClear;

    [SerializeField] private List<KitchenObjectSO> validIngredients;

    private List<KitchenObjectSO> ingredients;

    private void Awake()
    {
        ingredients = new List<KitchenObjectSO>();
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validIngredients.Contains(kitchenObjectSO)) return false;
        if (ingredients.Contains(kitchenObjectSO)) return false;
        ingredients.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(kitchenObjectSO);
        return true;
    }

    override public bool IsPlate(out PlateKitchenObject plateKitchenObject)
    {
        plateKitchenObject = this;
        return true;
    }

    public void ClearPlate()
    {
        ingredients.Clear();
        OnClear?.Invoke();
    }

    public List<KitchenObjectSO> GetIngredients()
    {
        return ingredients;
    }
}
