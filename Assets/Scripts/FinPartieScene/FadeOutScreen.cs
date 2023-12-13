using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeOutScreen : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image= GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        for (float alpha = 1.0f; alpha >= 0; alpha -= 0.01f)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;

            yield return new WaitForSeconds(0.01f);
        }
        StopCoroutine(FadeOut());
        canvas.SetActive(false);
    }
}
