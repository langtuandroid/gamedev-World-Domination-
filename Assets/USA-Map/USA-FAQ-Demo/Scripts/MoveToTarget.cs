using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour
{
    public bool MoveX, MoveY;

    public void MoveTo(Transform target)
    {
        if (!MoveX && !MoveY)
        {
            Debug.LogError("MoveToTarget doesn't have MoveX or MoveY toggled, so nothing happens.");
            return;
        }

        Vector3 offset = target.position - transform.position;

        offset.z = 0;
        if (!MoveX) offset.x = 0;
        if (!MoveY) offset.y = 0;

        transform.position += offset;
    }
}
