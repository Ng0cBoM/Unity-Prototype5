using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEffectController : MonoBehaviour
{

    private float fadeDuration = 1.5f;

    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            transform.position += new Vector3(0 , 0.01f , 0);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}
