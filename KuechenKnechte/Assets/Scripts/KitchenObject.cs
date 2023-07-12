using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    private IKitchenObjectParent parent;

    private void Start()
    {
        parent = gameObject.GetComponentInParent<IKitchenObjectParent>();
        parent.kitchenObject = this;
    }

    public void ChangeParent(IKitchenObjectParent newParent)
    {
        parent.kitchenObject = null;
        newParent.kitchenObject = this;
        parent = newParent;
        transform.parent = newParent.GetKitchenObjectParentPoint();
        transform.localPosition = Vector3.zero;
    }
}
