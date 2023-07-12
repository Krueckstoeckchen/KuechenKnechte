using UnityEngine;

public class TrashCounter : MonoBehaviour, IInteractable
{
    public KitchenObjectSO kitchenObjectSO;

    public void Select()
    { }

    public void Unselect()
    { }

    public void Interact(CharacterInteract characterInteract)
    {
        if (characterInteract.kitchenObject != null)
        {
            characterInteract.kitchenObject.Delete();
        }

    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {
        
    }
}
