using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlphaHitTestButton : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 pos, Camera cam)
    {
        pos = cam.ScreenToWorldPoint((Vector3)pos + Vector3.forward * (canv == null ? 1 : canv.planeDistance));

        (transform as RectTransform).GetWorldCorners(worldCorners);

        float minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
        for (int i = 0; i < 4; i++)
        {
            minX = Mathf.Min(worldCorners[i].x, minX);
            maxX = Mathf.Max(worldCorners[i].x, maxX);
            minY = Mathf.Min(worldCorners[i].y, minY);
            maxY = Mathf.Max(worldCorners[i].y, maxY);
        }

        pos.x = (pos.x - minX) / (maxX - minX);
        pos.y = (pos.y - minY) / (maxY - minY);

        if (!(pos.x > 0 && pos.x < 1 && pos.y > 0 && pos.y < 1)) return false;

        int x = (int)(imgCopy.width * pos.x);
        int y = (int)(imgCopy.height * pos.y);

        Color pixel = imgCopy.GetPixel(x, y);

        pixel.r *= 0.8f;
        pixel.g *= 0.8f;
        pixel.b *= 0.8f;

        imgCopy.SetPixel(x, y, pixel);

        return pixel.a > 0.5f;
    }


    // Use this for initialization
    IEnumerator Start()
    {
        btn = GetComponent<Button>();
        if (btn == null) throw new UnityException("No button found in AlphaHitTestButton, gob=" + gameObject.name);
        btn.onClick.AddListener(onClickHandler);

        img = GetComponent<Image>();
        if (img == null) throw new UnityException("No button found in AlphaHitTestButton, gob=" + gameObject.name);

        canv = GetComponentInParent<Canvas>();


        yield return new WaitForEndOfFrame();

        imgCopy = copyTexture(img.sprite.texture);

        Sprite s = Sprite.Create(imgCopy, new Rect(0, 0, imgCopy.width, imgCopy.height), Vector2.one * 0.5f);
        img.sprite = s;
    }

    Button btn;
    Image img;
    Canvas canv;
    Vector3[] worldCorners = new Vector3[4];
    Texture2D imgCopy;

    void onClickHandler()
    {
        Debug.Log("Clicked " + gameObject.name);

        var callout = FindObjectOfType<StateCalloutAnimator>();
        if (callout != null) callout.StateToCall = gameObject.name.Split('-')[0];
    }




    // Since we dont set the states as Read/Write, use this to get a copy for alpha hit tests
    // code is from https://support.unity3d.com/hc/en-us/articles/206486626-How-can-I-get-pixels-from-unreadable-textures
    Texture2D copyTexture(Texture2D texture)
    {
        int w = texture.width, h = texture.height;

        // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary(w, h, 0);

        // Blit the pixels on texture to the RenderTexture
        Graphics.Blit(texture, tmp);

        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;

        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;

        // Create a new readable Texture2D to copy the pixels to it
        Texture2D myTexture2D = new Texture2D(w, h, TextureFormat.ARGB32, true);
        myTexture2D.wrapMode = TextureWrapMode.Clamp;

        // Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        myTexture2D.Apply();

        // Reset the active RenderTexture
        RenderTexture.active = previous;

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

        // "myTexture2D" now has the same pixels from "texture" and it's readable.
        return myTexture2D;
    }
}
