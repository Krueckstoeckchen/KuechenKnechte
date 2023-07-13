using UnityEngine;

public class DeliveryCounter : MonoBehaviour, IInteractable
{
    public void Select()
    {}

    public void Unselect()
    {}
    public void Interact(CharacterInteract characterInteract)
    {
        if (characterInteract.kitchenObject != null)
        {
            if (characterInteract.kitchenObject.IsPlate(out PlateKitchenObject plateKitchenObject))
            {
                characterInteract.kitchenObject.Delete();
            }
        }
    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {
        
    }
}
