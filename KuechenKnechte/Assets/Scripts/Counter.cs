using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    public void Select()
    {}

    public void Unselect()
    {}

    public void Interact()
    {
        Debug.Log("Interact: " + gameObject);
    }
}
