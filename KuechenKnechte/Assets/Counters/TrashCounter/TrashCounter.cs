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
            if (characterInteract.kitchenObject.IsPlate(out PlateKitchenObject plateKitchenObject))
            {
                plateKitchenObject.ClearPlate();
                return;
            }

            if (characterInteract.kitchenObject.IsPan(out PanKitchenObject panKitchenObject))
            {
                panKitchenObject.ClearPan();
                return;
            }
            characterInteract.kitchenObject.Delete();
        }

    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {

    }
}
