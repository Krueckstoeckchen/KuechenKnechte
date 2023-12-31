using UnityEngine;

public class ContainerCounterAnimation : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnInteract += PlayAnimation;
    }

    private void PlayAnimation()
    {
        animator.SetTrigger("OpenClose");
    }
}
