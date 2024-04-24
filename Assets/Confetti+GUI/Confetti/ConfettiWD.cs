using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ConfettiWD : MonoBehaviour
{
    [SerializeField] private Transform rightT;
    [SerializeField] private Transform leftT;
    
    public void LaunchH ()
    {
        float xPos = Mathf.Abs(rightT.transform.localPosition.x);

        if ((16f / 9f) > ((float)Screen.height / (float)Screen.width))
            xPos = (xPos / (((float)Screen.height / (float)Screen.width)) * (16f / 9f));

        rightT.transform.localPosition = new Vector3(xPos, rightT.transform.localPosition.y, rightT.transform.localPosition.z);
        leftT.transform.localPosition = new Vector3(-xPos, leftT.transform.localPosition.y, leftT.transform.localPosition.z);

        rightT.gameObject.SetActive(true);
        leftT.gameObject.SetActive(true);
    }
}
