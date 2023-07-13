using System;
using UnityEngine;

public class PlatesCounter : MonoBehaviour, IInteractable
{
    public event Action<int> OnPlateCountChanged;

    [SerializeField] private KitchenObjectSO plate;
    [SerializeField] int maxPlates = 4;

    private int plates = 4;

    private void Start()
    {
        OnPlateCountChanged?.Invoke(plates);
    }

    public void Select()
    { }

    public void Unselect()
    { }

    public void Interact(CharacterInteract characterInteract)
    {
        if (characterInteract.kitchenObject == null && plates >= 1)
        {
            plates--;
            OnPlateCountChanged?.Invoke(plates);
            Instantiate(plate.prefab, characterInteract.GetKitchenObjectParentPoint());
        }

    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {

    }

    public void addPlate()
    {
        if (plates < maxPlates)
        {
            plates++;
            OnPlateCountChanged?.Invoke(plates);
        }
    }
}