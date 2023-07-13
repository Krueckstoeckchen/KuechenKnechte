using UnityEngine;
using UnityEngine.UI;

public class StoveCounterAnimation : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Image bar;
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private GameObject glowingEffect;


    private void Start()
    {
        stoveCounter.OnProgressChange += UpdateProgressBar;
        stoveCounter.OnCooking += OnCooking;
        bar.fillAmount = 0;
        progressBar.SetActive(false);
        particleEffect.SetActive(false);
        glowingEffect.SetActive(false);
    }

    private void UpdateProgressBar(float progress)
    {
        bar.fillAmount = progress;
        if (progress >= 1 || progress == 0)
        {
            progressBar.SetActive(false);
        }
        else
        {
            progressBar.SetActive(true);
        }
    }

    private void OnCooking(float progress)
    {
        if (progress >= 1 || progress == 0)
        {
            particleEffect.SetActive(false);
            glowingEffect.SetActive(false);
        }
        else
        {
            particleEffect.SetActive(true);
            glowingEffect.SetActive(true);
        }
    }
}
