using System;
using UnityEngine;

public class CuttingCounter : MonoBehaviour, IInteractable, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectParentPoint;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    public KitchenObject kitchenObject { get; set; }

    private int cuttingProgress = 0;

    public event Action OnInteractAlternate;
    public event Action<float> OnProgressChange;

    public void Select()
    { }

    public void Unselect()
    { }

    public void Interact(CharacterInteract characterInteract)
    {
        if (kitchenObject == null && characterInteract.kitchenObject != null && isInput(characterInteract.kitchenObject.GetKitchenObjectSO()))
        {
            characterInteract.kitchenObject.ChangeParent(this);
            cuttingProgress = 0;
            OnProgressChange?.Invoke(0f);
            return;
        }

        if (kitchenObject != null && characterInteract.kitchenObject == null)
        {
            kitchenObject.ChangeParent(characterInteract);
            cuttingProgress = 0;
            OnProgressChange?.Invoke(0f);
            return;
        }

        if (kitchenObject != null && characterInteract.kitchenObject != null)
        {
            PlateKitchenObject plate;
            if (characterInteract.kitchenObject.IsPlate(out plate))
            {
                if (plate.TryAddIngredient(kitchenObject.GetKitchenObjectSO()))
                {
                    kitchenObject.Delete();
                    return;
                }
            }
        }
    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {
        if (kitchenObject == null) return;
        if (!isInput(kitchenObject.GetKitchenObjectSO())) return;
        cuttingProgress++;
        OnInteractAlternate?.Invoke();
        OnProgressChange?.Invoke((float)cuttingProgress/GetCuttingRecipeSO(kitchenObject.GetKitchenObjectSO()).cuts);
        if (cuttingProgress >= GetCuttingRecipeSO(kitchenObject.GetKitchenObjectSO()).cuts)
        {
            KitchenObjectSO output = GetCuttingRecipeSO(kitchenObject.GetKitchenObjectSO()).output;
            kitchenObject.Delete();
            Instantiate(output.prefab, kitchenObjectParentPoint);
            cuttingProgress = 0;
        }
    }

    public Transform GetKitchenObjectParentPoint()
    {
        return kitchenObjectParentPoint;
    }

    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (input == recipe.input) return recipe;
        }
        return null;
    }

    private KitchenObjectSO GetRecipeOutput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (input == recipe.input) return recipe.output;
        }
        return null;
    }

    private bool isInput(KitchenObjectSO input)
    {
        if (GetRecipeOutput(input) == null) return false;
        return true;
    }
}