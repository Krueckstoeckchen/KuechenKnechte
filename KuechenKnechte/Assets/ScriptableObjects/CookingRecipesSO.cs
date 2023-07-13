using UnityEngine;

[CreateAssetMenu()]
public class CookingRecipesSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float cookingTime;
}
