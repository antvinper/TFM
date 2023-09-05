using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    TextMeshProUGUI blinkingText;
    public float blinkFadeIn = 0.6f;
    public float blinkStay = 0.7f;
    public float blinkFadeOut = 0.6f;
    float timeChecker = 0;
    Color color;

    void Start()
    {
        blinkingText = this.GetComponent<TextMeshProUGUI>();
        color = blinkingText.color;
    }

    void Update()
    {
        timeChecker += Time.deltaTime;
        if (timeChecker < blinkFadeIn)
        {
            blinkingText.color = new Color(color.r, color.g, color.b, timeChecker / blinkFadeIn);
        }
        else if (timeChecker < blinkFadeIn + blinkStay)
        {
            blinkingText.color = new Color(color.r, color.g, color.b, 1);
        }
        else if (timeChecker < blinkFadeIn + blinkStay + blinkFadeOut)
        {
            blinkingText.color = new Color(color.r, color.g, color.b, 1 - (timeChecker - (blinkFadeIn + blinkStay)) / blinkFadeOut);
        }
        else
        {
            timeChecker = 0;
        }
    }
}
