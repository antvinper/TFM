using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] GameObject fadeInPanel;
    [SerializeField] GameObject fadeOutPanel;
    [SerializeField] GameObject fadePanel;

    [SerializeField] bool startsInActive;

    [SerializeField] Animator fadeInAnimator;
    [SerializeField] Animator fadeOutAnimator;

    private float fadeInDuration;
    private float fadeOutDuration;

    private void Start()
    {
        if (startsInActive)
        {
            fadePanel.SetActive(true);
        }

        fadeInDuration = fadeInAnimator.runtimeAnimatorController.animationClips[0].length;
        fadeOutDuration = fadeOutAnimator.runtimeAnimatorController.animationClips[0].length;
    }

    public async Task FadeIn()
    {
        fadeInPanel.SetActive(true);
        await new WaitForSeconds(fadeInDuration);
        fadeInPanel.SetActive(false);
        fadePanel.SetActive(true);
    }
    public async Task FadeOut()
    {
        fadeOutPanel.SetActive(true);
        fadePanel.SetActive(false);
        await new WaitForSeconds(fadeOutDuration);
        fadeOutPanel.SetActive(false);

    }
}
