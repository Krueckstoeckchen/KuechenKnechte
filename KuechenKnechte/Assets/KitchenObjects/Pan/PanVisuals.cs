using System;
using System.Collections.Generic;
using UnityEngine;

public class PanVisuals : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PanKitchenObject panKitchenObject;
    [SerializeField] private GameObject iconCanvas;
    [SerializeField] private GameObject icon;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjects;

    private void Start()
    {
        panKitchenObject.OnAdded += Add;
        panKitchenObject.OnClear += Clear;
        Clear();
    }

    private void Add(KitchenObjectSO addedKitchenObjectSO)
    {
        iconCanvas.SetActive(true);
        icon.GetComponent<Icon>().SetKitchenObjectSO(addedKitchenObjectSO);

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == addedKitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
            else{
                kitchenObjectSO_GameObject.gameObject.SetActive(false);
            }
        }
    }

    private void Clear()
    {
        iconCanvas.SetActive(false);
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }
}
