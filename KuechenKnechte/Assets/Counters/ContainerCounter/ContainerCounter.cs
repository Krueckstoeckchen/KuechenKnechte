using System;
using UnityEngine;

public class ContainerCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event Action OnInteract;
 
    public void Select()
    { }

    public void Unselect()
    { }

    public void Interact(CharacterInteract characterInteract)
    {
        if (characterInteract.kitchenObject == null)
        {
            Instantiate(kitchenObjectSO.prefab, characterInteract.GetKitchenObjectParentPoint());
            OnInteract?.Invoke();
        }

    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {
        
    }
}
