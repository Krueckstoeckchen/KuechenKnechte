using UnityEngine;

public class ContainerCounter : MonoBehaviour, IInteractable
{
    public KitchenObjectSO kitchenObjectSO;

    public void Select()
    { }

    public void Unselect()
    { }

    public void Interact(CharacterInteract characterInteract)
    {
        if (characterInteract.kitchenObject == null)
        {
            Instantiate(kitchenObjectSO.prefab, characterInteract.GetKitchenObjectParentPoint());
        }

    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {
        
    }
}
