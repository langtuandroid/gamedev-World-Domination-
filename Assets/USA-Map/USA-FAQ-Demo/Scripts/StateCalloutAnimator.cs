using UnityEngine;
using System.Collections;

public class StateCalloutAnimator : MonoBehaviour {

    public TerritoryManager TerritoryMgr;

    public Transform CalloutParent;

    public string StateToCall;

    public Vector2 TargetSize;
    public Vector3 TargetPosition;
    public float AnimateTime;

    public GameObject[] ActivateDuringCallout;
    

    public void CancelCallout() { StateToCall = ""; }

    GameObject currentState;
    string currentStateId = "";
    Coroutine curAnim = null;

    // Update is called once per frame
    void Update()
    {
        if (TerritoryMgr == null || curAnim != null || (StateToCall = StateToCall.ToUpperInvariant()) == currentStateId)
            return;

        Debug.Log("StateToCall=" + StateToCall + ", curState=" + currentStateId + "");

        if (currentStateId != "")
        {
            currentStateId = "";
            if (currentState != null) Destroy(currentState);
            for (int i = 0; i < ActivateDuringCallout.Length; i++)
                ActivateDuringCallout[i].SetActive(false);
        }

        if (StateToCall == null || StateToCall.Length < 1)
            return;

        var stateData = TerritoryMgr.GetStateByShortId(StateToCall.ToUpperInvariant());
        if (stateData != null)
        {
            currentStateId = stateData.ShortId;
            curAnim = StartCoroutine(animatedCallout(stateData));
        }
    }

    IEnumerator animatedCallout(TerritoryManager.StateData stateData)
    {

        float scaleX = TargetSize.x, scaleY = TargetSize.y;
        if(stateData.Renderer != null)
        {
            scaleX = scaleX / stateData.Renderer.bounds.size.x;
            scaleY = scaleY / stateData.Renderer.bounds.size.y;
        }
        else if(stateData.UiImage != null)
        {
            scaleX = scaleX / stateData.UiImage.rectTransform.sizeDelta.x;
            scaleY = scaleY / stateData.UiImage.rectTransform.sizeDelta.y;
        }
        float scaleMin = Mathf.Min(scaleX, scaleY);
        Vector3 TargetScale = Vector3.one * scaleMin;
        Vector3 startScale = stateData.Object.transform.localScale;
        Vector3 startPos = stateData.Object.transform.position;
        float t = AnimateTime, tStart = t;
        currentState = Instantiate(stateData.Object);
        currentState.transform.SetParent(CalloutParent == null ? stateData.Object.transform.parent : CalloutParent, false);
        while ((t -= Time.deltaTime) > 0)
        {
            float p = Mathf.InverseLerp(tStart, 0, t);
            p = 1 - (1 - p) * (1 - p);
            currentState.transform.localScale = Vector3.Lerp(startScale, TargetScale, p);
            currentState.transform.position = Vector3.Lerp(startPos, TargetPosition, p);

            yield return null;
        }
        for (int i = 0; i < ActivateDuringCallout.Length; i++)
            ActivateDuringCallout[i].SetActive(true);

        curAnim = null;
    }
}
