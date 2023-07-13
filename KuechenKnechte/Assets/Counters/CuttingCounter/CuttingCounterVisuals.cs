using UnityEngine;

public class CuttingCounterAnimation : MonoBehaviour
{
    [SerializeField] private CuttingCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnInteractAlternate += PlayAnimation;
    }

    private void PlayAnimation()
    {
        animator.SetTrigger("Cut");
    }
}
