using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToColor : MonoBehaviour
{
    public static FadeToColor instance;
    
    public CanvasGroup fadeCanvasGroup;
    public Image fadeImage;
    
    public float defaultDuration = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public IEnumerator FadeOut()
    {
        yield return Fade(defaultDuration, Color.black, AnimationCurve.EaseInOut(0, 0, 1, 1));
    }
    
    public IEnumerator FadeIn()
    {
        yield return Fade(defaultDuration, Color.black, AnimationCurve.EaseInOut(1, 1, 0, 0));
    }
    
    public IEnumerator Fade(float duration, Color color, AnimationCurve curve = null)
    {
        
        if (curve == null) curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        fadeImage.color = color;
        float elapsedTime = 0f;
        
        fadeCanvasGroup.alpha = curve.Evaluate(0f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = curve.Evaluate(Mathf.Clamp01(elapsedTime / duration));
            yield return null;
        }
        fadeCanvasGroup.alpha = curve.Evaluate(1f);
    }
}