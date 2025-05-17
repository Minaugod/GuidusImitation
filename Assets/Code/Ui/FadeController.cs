using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FadeController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup fadePanel;

    public TMP_Text stageNameText;

    public IEnumerator StageName(string floor)
    {
        stageNameText.text = "B" + floor + "F";

        float t = 0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / 1f;
            stageNameText.color = new Color(255, 255, 255, t);

            yield return null;

        }

        while (t > 0.0f)
        {
            t -= Time.deltaTime / 1f;
            stageNameText.color = new Color(255, 255, 255, t);

            yield return null;

        }
    }

    public IEnumerator FadeIn()
    {
        float fadeTime = 1f;
        float accumTime = 0f;

        gameObject.SetActive(true);
        while (accumTime < fadeTime)
        {
            fadePanel.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            accumTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float fadeTime = 0.5f;
        float accumTime = 0f;

        while (accumTime < fadeTime)
        {
            fadePanel.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            accumTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);

    }
}
