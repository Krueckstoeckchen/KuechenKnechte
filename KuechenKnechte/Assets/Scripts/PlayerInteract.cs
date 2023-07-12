using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float selectingDistance = 2f;
    private IInteractable selectedObject = null;

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
}
