using System;
using UnityEngine;

public class StoveCounter : MonoBehaviour, IInteractable, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectParentPoint;

    [SerializeField] private CookingRecipesSO[] cookingRecipes;
    [SerializeField] private BurningRecipeSO[] burningRecipes;

    public KitchenObject kitchenObject { get; set; }

    private float progress = 0;

    public event Action<float> OnProgressChange;
    public event Action<float> OnCooking;

    public void Select()
    { }

    public void Unselect()
    { }

    public void Interact(CharacterInteract characterInteract)
    {
        if (kitchenObject == null && characterInteract.kitchenObject != null && IsInput(characterInteract.kitchenObject.GetKitchenObjectSO()))
        {
            characterInteract.kitchenObject.ChangeParent(this);
            progress = 0;
            OnProgressChange?.Invoke(0f);
            OnCooking?.Invoke(0f);
            return;
        }

        if (kitchenObject != null && characterInteract.kitchenObject == null)
        {
            kitchenObject.ChangeParent(characterInteract);
            progress = 0;
            OnProgressChange?.Invoke(0f);
            OnCooking?.Invoke(0f);
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
       
    }

    private void Update()
    {
        if (kitchenObject == null) return;
        if (!IsInput(kitchenObject.GetKitchenObjectSO())) return;
        progress += Time.deltaTime;
        if (GetCookingRecipeSO(kitchenObject.GetKitchenObjectSO()) != null)
        {
            OnProgressChange?.Invoke(progress / GetCookingRecipeSO(kitchenObject.GetKitchenObjectSO()).cookingTime);
            OnCooking?.Invoke(progress / GetCookingRecipeSO(kitchenObject.GetKitchenObjectSO()).cookingTime);
            if (progress >= GetCookingRecipeSO(kitchenObject.GetKitchenObjectSO()).cookingTime)
            {
                KitchenObjectSO output = GetCookingRecipeSO(kitchenObject.GetKitchenObjectSO()).output;
                kitchenObject.Delete();
                Instantiate(output.prefab, kitchenObjectParentPoint);
                progress = 0;
                return;
            }
        }

        if (GetBurningRecipeSO(kitchenObject.GetKitchenObjectSO()) != null)
        {
            OnCooking?.Invoke(progress / GetBurningRecipeSO(kitchenObject.GetKitchenObjectSO()).burningTime);
            if (progress >= GetBurningRecipeSO(kitchenObject.GetKitchenObjectSO()).burningTime)
            {
                KitchenObjectSO output = GetBurningRecipeSO(kitchenObject.GetKitchenObjectSO()).output;
                kitchenObject.Delete();
                Instantiate(output.prefab, kitchenObjectParentPoint);
                progress = 0;
                return;
            }
        }
    }


    public Transform GetKitchenObjectParentPoint()
    {
        return kitchenObjectParentPoint;
    }

    private CookingRecipesSO GetCookingRecipeSO(KitchenObjectSO input)
    {
        foreach (CookingRecipesSO recipe in cookingRecipes)
        {
            if (input == recipe.input) return recipe;
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO input)
    {
        foreach (BurningRecipeSO recipe in burningRecipes)
        {
            if (input == recipe.input) return recipe;
        }
        return null;
    }

    private KitchenObjectSO GetCookingRecipeOutput(KitchenObjectSO input)
    {
        foreach (CookingRecipesSO recipe in cookingRecipes)
        {
            if (input == recipe.input) return recipe.output;
        }
        return null;
    }

    private KitchenObjectSO GetBurningRecipeOutput(KitchenObjectSO input)
    {
        foreach (BurningRecipeSO recipe in burningRecipes)
        {
            if (input == recipe.input) return recipe.output;
        }
        return null;
    }

    private bool IsInput(KitchenObjectSO input)
    {
        if (GetCookingRecipeOutput(input) == null && GetBurningRecipeOutput(input) == null) return false;
        return true;
    }
}