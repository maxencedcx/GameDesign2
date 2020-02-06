using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BlackScreenFadeOut : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private float lerpDuration;
    private float lerpStart;
    private Image thisImage;

    private void Start()
    {
        lerpStart = Time.time;
        thisImage = gameObject.GetComponent<Image>();
        thisImage.color = baseColor;
    }

    private void Update()
    {
        float lerpProgress = (Time.time - lerpStart) / lerpDuration;
        thisImage.color = Color.Lerp(baseColor, targetColor, lerpProgress);
        if (lerpProgress >= 1)
            Destroy(gameObject);
    }
}
