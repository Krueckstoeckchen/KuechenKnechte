using System;
using UnityEngine;

public class StoveCounter : MonoBehaviour, IInteractable, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO startingKitchenObjectSO;
    [SerializeField] private Transform kitchenObjectParentPoint;

    [SerializeField] private CookingRecipesSO[] cookingRecipes;
    [SerializeField] private BurningRecipeSO[] burningRecipes;

    public KitchenObject kitchenObject { get; set; }

    private float progress = 0;

    public event Action<float> OnProgressChange;
    public event Action<float> OnCooking;

    public void Start()
    {
        if (startingKitchenObjectSO != null) Instantiate(startingKitchenObjectSO.prefab, kitchenObjectParentPoint);
    }

    public void Select()
    { }

    public void Unselect()
    { }

    public void Interact(CharacterInteract characterInteract)
    {
        PanKitchenObject panKitchenObject;
        if (kitchenObject == null && characterInteract.kitchenObject != null && characterInteract.kitchenObject.IsPan(out panKitchenObject))
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
                if (kitchenObject.IsPan(out panKitchenObject))
                {
                    if (panKitchenObject.GetIngredient() != null)
                    {
                        if (plate.TryAddIngredient(panKitchenObject.GetIngredient()))
                        {
                            panKitchenObject.ClearPan();
                            OnProgressChange?.Invoke(0f);
                            OnCooking?.Invoke(0f);
                            return;
                        }
                    }
                }
            }
            else
            {
                
                if (kitchenObject.IsPan(out panKitchenObject))
                {
                    
                    if (panKitchenObject.GetIngredient() == null)
                    {
                        if (panKitchenObject.TryAddIngredient(characterInteract.kitchenObject.GetKitchenObjectSO()))
                        {
                            characterInteract.kitchenObject.Delete();
                            return;
                        }
                    }
                }
            }
        }
    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {

    }

    private void Update()
    {
        if (kitchenObject == null || !(kitchenObject.IsPan(out PanKitchenObject panKitchenObject))) return;

        KitchenObjectSO panIngredient = panKitchenObject.GetIngredient();

        if (panIngredient == null || !IsInput(panIngredient)) return;
        progress += Time.deltaTime;
        if (GetCookingRecipeSO(panIngredient) != null)
        {
            OnProgressChange?.Invoke(progress / GetCookingRecipeSO(panIngredient).cookingTime);
            OnCooking?.Invoke(progress / GetCookingRecipeSO(panIngredient).cookingTime);
            if (progress >= GetCookingRecipeSO(panIngredient).cookingTime)
            {
                KitchenObjectSO output = GetCookingRecipeSO(panIngredient).output;
                panKitchenObject.ClearPan();
                panKitchenObject.TryAddIngredient(output);
                progress = 0;
                return;
            }
        }

        if (GetBurningRecipeSO(panIngredient) != null)
        {
            OnCooking?.Invoke(progress / GetBurningRecipeSO(panIngredient).burningTime);
            if (progress >= GetBurningRecipeSO(panIngredient).burningTime)
            {
                KitchenObjectSO output = GetBurningRecipeSO(panIngredient).output;
                panKitchenObject.ClearPan();
                panKitchenObject.TryAddIngredient(output);
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