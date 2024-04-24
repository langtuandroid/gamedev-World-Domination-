using UnityEngine;

// Helper to set a RectTransform's scale to 1,1,1 while increasing the sizeDelta to match
[ExecuteInEditMode]
public class NormalizeUiScale : MonoBehaviour {

	// Use this for initialization
	private void Start () {

        RectTransform rt = transform as RectTransform;
        if(rt != null)
        {
            rt.sizeDelta = Vector2.Scale(rt.sizeDelta, rt.localScale);
            rt.localScale = Vector3.one;
        }
	}
}
