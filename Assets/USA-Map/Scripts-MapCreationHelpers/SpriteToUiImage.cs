using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Helper to convert SpriteRenderers to Canvas Images
[ExecuteInEditMode]
public class SpriteToUiImage : MonoBehaviour
{
    public float Scale;
    public bool DoSwap;

    void Update()
    {
        if (!DoSwap) return;
        DoSwap = false;

        swapToUiImage();
    }


    void swapToUiImage()
    {
        Sprite s = getSprite();
        if (s == null)
            return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) DestroyImmediate(sr);
        if (gameObject.GetComponent<CanvasRenderer>() == null)
            gameObject.AddComponent<CanvasRenderer>();
        Image img = gameObject.GetComponent<Image>();
        if (img == null) img = gameObject.AddComponent<Image>();
        img.sprite = s;
        RectTransform rt = gameObject.transform as RectTransform;
        if (rt != null) rt.sizeDelta = new Vector2(s.bounds.size.x, s.bounds.size.y) * Scale;
        else gameObject.transform.localScale = new Vector2(s.textureRect.width, s.textureRect.height) * s.pixelsPerUnit * Scale; 
    }


    Sprite getSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) return sr.sprite;

        Image uiImg = GetComponent<Image>();
        if (uiImg != null) return uiImg.sprite;

        return null;
    }
}
