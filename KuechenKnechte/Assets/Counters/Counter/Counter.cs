using UnityEngine;

public class Counter : MonoBehaviour, IInteractable, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectParentPoint;

    public KitchenObject kitchenObject { get; set; }

    public void Select()
    {}

    public void Unselect()
    {}

    public void Interact(CharacterInteract characterInteract)
    {
        if (kitchenObject == null && characterInteract.kitchenObject != null)
        {
            characterInteract.kitchenObject.ChangeParent(this);
            return;
        }

        if (kitchenObject != null && characterInteract.kitchenObject == null)
        {
            kitchenObject.ChangeParent(characterInteract);
            return;
        }

        if (kitchenObject != null && characterInteract.kitchenObject != null)
        {
            PlateKitchenObject plate;
            if (characterInteract.kitchenObject.IsPlate(out plate))
            {
                if (plate.TryAddIngredient(kitchenObject.GetKitchenObjectSO())){
                    kitchenObject.Delete();
                    return;
                }
            }

            if (kitchenObject.IsPlate(out plate))
            {
                if (plate.TryAddIngredient(characterInteract.kitchenObject.GetKitchenObjectSO()))
                {
                    characterInteract.kitchenObject.Delete();
                    return;
                }
            }
        }
    }

    public Transform GetKitchenObjectParentPoint()
    {
        return kitchenObjectParentPoint;
    }

    public void InteractAlternate(CharacterInteract characterInteract)
    {
       
    }
}