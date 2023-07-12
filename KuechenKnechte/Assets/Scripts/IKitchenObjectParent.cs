using UnityEngine;

public interface IKitchenObjectParent
{
    public KitchenObject kitchenObject { get; set; }
    public Transform GetKitchenObjectParentPoint();
}
