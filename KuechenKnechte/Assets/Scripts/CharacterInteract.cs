using System;
using UnityEngine;

public class CharacterInteract : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectParentPoint;
    [SerializeField] private float selectingDistance = 2f;
    private IInteractable selectedObject = null;
    private PlayerInput playerInput;

    public KitchenObject kitchenObject { get; set; }

    void Start()
    {
        playerInput = this.gameObject.GetComponent<PlayerInput>();
        playerInput.OnInteract += OnInteract;
        playerInput.OnInteractAlternate += OnInteractAlternate;
    }

    private void Update()
    {
        Vector3 moveRayPositionUp = new Vector3(0f, 0.5f, 0f);
        if (Physics.Raycast(transform.position + moveRayPositionUp, transform.forward, out RaycastHit raycastHit, selectingDistance))
        {
            GameObject hittedGameObject = raycastHit.transform.gameObject;
            if (hittedGameObject.TryGetComponent<IInteractable>(out IInteractable selectableObject))
            {
                if (selectableObject != selectedObject) ChangeSelectedObject(selectableObject);
            }
            else
            {
                ChangeSelectedObject(null);
            }
        }
        else
        {
            if(selectedObject != null) ChangeSelectedObject(null);
        }
    }

    private void ChangeSelectedObject(IInteractable newSelectedObject)
    {
        if (selectedObject != null) selectedObject.Unselect();
        selectedObject = newSelectedObject;
        if (selectedObject != null) selectedObject.Select();
    }

    private void OnInteract()
    {
        if (selectedObject != null) selectedObject.Interact(this);
    }

    private void OnInteractAlternate()
    {
        if (selectedObject != null) selectedObject.InteractAlternate(this);
    }

    public Transform GetKitchenObjectParentPoint()
    {
        return kitchenObjectParentPoint;
    }
}
