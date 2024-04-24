using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    float amplitude = .1f;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.unscaledDeltaTime;
        this.GetComponent<RectTransform>().localScale = Vector3.one + Vector3.one * Mathf.Sin(time * 3.5f) * amplitude;
    }
}