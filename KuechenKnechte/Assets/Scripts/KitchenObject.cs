using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
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

    public void Delete()
    {
        parent.kitchenObject = null;
        Destroy(gameObject);
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    virtual public bool IsPlate(out PlateKitchenObject plateKitchenObject)
    {
        plateKitchenObject = null;
        return false;
    }

    virtual public bool IsPan(out PanKitchenObject panKitchenObject)
    {
        panKitchenObject = null;
        return false;
    }
}
