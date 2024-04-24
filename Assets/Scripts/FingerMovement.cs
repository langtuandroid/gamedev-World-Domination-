using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerMovement : MonoBehaviour
{
    Vector3 startPos;
    public Vector3 targetPos;
    public static bool disableTut = false;
    // Start is called before the first frame update
    void Start()
    {
      //  if (SceneController.level != 1)
      //      gameObject.SetActive(false);
      //  else
      //  {
            startPos = transform.localPosition;
            StartCoroutine(MoveFinger());
      //  }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MoveFinger()
    {
        Image SR = transform.GetComponent<Image>();
        while (!disableTut)
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
                transform.localPosition = Vector3.Lerp(startPos, targetPos, k);
                
                yield return 0;
            }

            if (disableTut) {
                gameObject.SetActive(false);
            }
        }
        yield break;
    }
}
