using UnityEngine;

// Helper to sort children by their game object name. Except for states I wanted to sort by the second part of a split operation, for Names instead of IDs
namespace USA_Map.Scripts_MapCreationHelpers
{
    [ExecuteInEditMode]
    public class SortChildren : MonoBehaviour
    {
        private void Start()
        {
            Transform[] children = new Transform[transform.childCount];
            for (int i = 0; i < children.Length; i++)
                children[i] = transform.GetChild(i);

            System.Array.Sort(children, delegate (Transform t1, Transform t2) {
                string[] s1 = t1.name.Split('-');
                string[] s2 = t2.name.Split('-');
                return s1[Mathf.Min(s1.Length - 1, 1)].CompareTo(s2[Mathf.Min(s2.Length - 1, 1)]);
            });

            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetSiblingIndex(i);
            }
        }



    }
}
