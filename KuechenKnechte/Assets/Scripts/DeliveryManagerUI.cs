using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject recipeUIprefab;

    private List<GameObject> recipeUIs;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeAdded += AddRecipe;
        DeliveryManager.Instance.OnRecipeDeleted += DeleteRecipe;
        recipeUIs = new List<GameObject>();
    }

    private void DeleteRecipe(int index)
    {
        Destroy(recipeUIs[index]);
        recipeUIs.RemoveAt(index);
    }

    private void AddRecipe(RecipeSO recipeSO)
    {
        GameObject newRecipeUI = Instantiate(recipeUIprefab, transform);
        newRecipeUI.GetComponent<RecipeUI>().SetRecipeSO(recipeSO);
        recipeUIs.Add(newRecipeUI);
    }
}
