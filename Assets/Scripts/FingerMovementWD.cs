using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FingerMovementWD : MonoBehaviour
{
    private Vector3 startPosS;
    public Vector3 targetPosS;
    public static bool disableTutT = false;
    
    private void Start()
    {
        startPosS = transform.localPosition;
        StartCoroutine(MoveFingerR());
    }
    
    private IEnumerator MoveFingerR()
    {
        Image SR = transform.GetComponent<Image>();
        while (!disableTutT)
        {
            float k = 0f;
            while (k < 1f)
            {
                k += Time.deltaTime * 0.75f;

                if (k < 0.2f)
                    SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, k * 5f);

                if (k > 0.8f)
                    SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, (1f - k) / 0.2f);

                if (k > 1f)
                    k = 1f;
                transform.localPosition = Vector3.Lerp(startPosS, targetPosS, k);
                
                yield return 0;
            }

            if (disableTutT) {
                gameObject.SetActive(false);
            }
        }
        yield break;
    }
}
