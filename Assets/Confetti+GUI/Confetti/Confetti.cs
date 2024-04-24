using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public Transform right;
    public Transform left;

    // Start is called before the first frame update
    void Start()
    {
        //Launch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Launch ()
    {
        float xPos = Mathf.Abs(right.transform.localPosition.x);

        if ((16f / 9f) > ((float)Screen.height / (float)Screen.width))
            xPos = (xPos / (((float)Screen.height / (float)Screen.width)) * (16f / 9f));

        right.transform.localPosition = new Vector3(xPos, right.transform.localPosition.y, right.transform.localPosition.z);
        left.transform.localPosition = new Vector3(-xPos, left.transform.localPosition.y, left.transform.localPosition.z);

        right.gameObject.SetActive(true);
        left.gameObject.SetActive(true);
        //confetti1.parent = null;
        //confetti2.parent = null;
    }
}
