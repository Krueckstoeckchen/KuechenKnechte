using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    public void Select()
    {
        Debug.Log("Selected " + gameObject.name);
    }

    public void Unselect()
    {
        Debug.Log("Unselected " + gameObject.name);
    }

}
