using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private float fadeInTime = 0.5f;
    private SpriteRenderer sr;
    private Color hiddenColor;
    Coroutine currentCoroutine;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        hiddenColor = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentCoroutine = StartCoroutine(Fade(true));
        }
    }
    private IEnumerator Fade(bool fadeout)
    {
        Color startColor = sr.color;
        Color targetColor = hiddenColor;
        
        if (fadeout)
        {
            targetColor.a = 0;
        }
        float time = 0;
        while (time < fadeInTime)
        {
            sr.color = Color.Lerp(startColor, targetColor, time / fadeInTime);
            time += Time.deltaTime;
            yield return null;
        }
        sr.color = targetColor;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentCoroutine = StartCoroutine(Fade(false));
        }
    }
}
