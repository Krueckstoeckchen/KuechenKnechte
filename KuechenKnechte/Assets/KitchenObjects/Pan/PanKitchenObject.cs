using System;
using System.Collections.Generic;
using UnityEngine;

public class PanKitchenObject : KitchenObject
{
    public event Action<KitchenObjectSO> OnAdded;
    public event Action OnClear;

    private KitchenObjectSO ingredient;

    [SerializeField] private List<KitchenObjectSO> validIngredients;

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validIngredients.Contains(kitchenObjectSO)) return false;
        if (ingredient != null) return false;
        ingredient = kitchenObjectSO;
        OnAdded.Invoke(kitchenObjectSO);
        return true;
    }

    override public bool IsPan(out PanKitchenObject plateKitchenObject)
    {
        plateKitchenObject = this;
        return true;
    }

    public void ClearPan()
    {
        ingredient = null;
        OnClear?.Invoke();
    }

    public KitchenObjectSO GetIngredient()
    {
        return ingredient;
    }
}
