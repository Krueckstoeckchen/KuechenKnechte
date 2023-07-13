using UnityEngine;
using UnityEngine.UI;

public class CuttingCounterAnimation : MonoBehaviour
{
    [SerializeField] private CuttingCounter containerCounter;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Image bar;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnInteractAlternate += PlayAnimation;
        containerCounter.OnProgressChange += UpdateProgressBar;
        bar.fillAmount = 0;
        progressBar.SetActive(false);
    }

    private void PlayAnimation()
    {
        animator.SetTrigger("Cut");
    }

    private void UpdateProgressBar(float progress)
    {
        bar.fillAmount = progress;
        if(progress == 1 || progress == 0)
        {
            progressBar.SetActive(false);
        }
        else
        {
            progressBar.SetActive(true);
        }
    }
}
