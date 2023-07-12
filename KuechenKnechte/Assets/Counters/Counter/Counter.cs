using UnityEngine;

public class Counter : MonoBehaviour, IInteractable, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectParentPoint;

    public KitchenObject kitchenObject { get; set; }

    public KitchenObjectSO kitchenObjectSO;

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
        
    }

    public Transform GetKitchenObjectParentPoint()
    {
        return kitchenObjectParentPoint;
    }
}