using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutObject : MonoBehaviour
{
    public float fadeSeed, fadeAmount;
    private float original;
    private SpriteRenderer sprite;
    public bool doFade = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        original = sprite.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (doFade) Fade();
        else ResetFade();
    }

    private void Fade()
    {
        Color currentC = sprite.color;
        Color smoothC = new Color(currentC.r, currentC.g, currentC.b, Mathf.Lerp(currentC.a, fadeAmount, fadeSeed));
        sprite.color = smoothC;
    }

    private void ResetFade()
    {
        Color currentC = sprite.color;
        Color smoothC = new Color(currentC.r, currentC.g, currentC.b, Mathf.Lerp(currentC.a, original, fadeSeed));
        sprite.color = smoothC;
    }
}
