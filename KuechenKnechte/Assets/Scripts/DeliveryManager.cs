using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;
    public event Action<RecipeSO> OnRecipeAdded;
    public event Action<int> OnRecipeDeleted;

    [SerializeField] private List<RecipeSO> validRecipes;
    [SerializeField] int maxWaitingRecipes;
    [SerializeField] float maxRecipeSpawnTime;
    [SerializeField] float minRecipeSpawnTime;

    private List<RecipeSO> waitingRecipes;

    private float recipeSpawnTimer;
    private float recipeSpawnTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        else Instance = this;
    }

    private void Start()
    {
        waitingRecipes = new List<RecipeSO>();
        recipeSpawnTimer = 0;
        recipeSpawnTime = GetRecipeSpawnTime();
    }

    private void Update()
    {
        if (waitingRecipes.Count < maxWaitingRecipes)
        {
            recipeSpawnTimer += Time.deltaTime;
        }

        if (recipeSpawnTimer > recipeSpawnTime)
        {
            recipeSpawnTimer = 0;
            recipeSpawnTime = GetRecipeSpawnTime();
            SpawnNewRecipe();

        }
    }

    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO recipe in waitingRecipes)
        {
            if (KitchenObjectListEqual(recipe.ingredientsList, plateKitchenObject.GetIngredients()))
            {
                int index = waitingRecipes.IndexOf(recipe);
                waitingRecipes.Remove(recipe);
                OnRecipeDeleted?.Invoke(index);
                return true;
            }
        }

        return false;
    }

    private float GetRecipeSpawnTime()
    {
        return UnityEngine.Random.Range(minRecipeSpawnTime, maxRecipeSpawnTime);
    }

    private void SpawnNewRecipe() 
    {
        RecipeSO newRecipe = validRecipes[UnityEngine.Random.Range(0, validRecipes.Count - 1)];
        waitingRecipes.Add(newRecipe);
        OnRecipeAdded?.Invoke(newRecipe);
    }

    private bool KitchenObjectListEqual(List<KitchenObjectSO> list1, List<KitchenObjectSO> list2)
    {
        if (list1.Count != list2.Count) return false;
        foreach (KitchenObjectSO list1Ingredient in list1)
        {
            if (!list2.Contains(list1Ingredient))
            {
                return false;
            }
        }
        return true;
    }
}
