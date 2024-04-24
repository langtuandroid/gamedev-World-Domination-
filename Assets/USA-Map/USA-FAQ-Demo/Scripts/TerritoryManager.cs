using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TerritoryManager : MonoBehaviour
{
    [System.Serializable]
    public class StateData : IComparable
    {
        public readonly GameObject Object;
        public readonly Renderer Renderer;
        public readonly Image UiImage;
        public readonly string ShortId;
        public readonly string FullName;

#if UNITY_EDITOR
        // vars to view in Unity Editor for debugging
        [SerializeField]
        private GameObject ViewOnlyObject;
        [SerializeField]
        private Renderer ViewOnlyRenderer;
        [SerializeField]
        private string ViewOnlyShortId;
        [SerializeField]
        private string ViewOnlyFullName;
#endif

        public const char Separator = '-';

        public StateData(GameObject stateObject)
        {
            if ((Object = stateObject) == null) throw new ArgumentNullException("stateObject");
            Renderer = Object.GetComponent<Renderer>();
            UiImage = Object.GetComponent<Image>();
            string[] name = Object.name.Split(Separator);
            ShortId = name[0].Trim().ToUpperInvariant();
            if (name.Length == 1) FullName = name[0].Trim();
            else
            {
                FullName = name[1].Trim();
                for (int i = 2; i < name.Length; i++) FullName += ' ' + name[i];
            }

#if UNITY_EDITOR
            ViewOnlyObject = Object;
            ViewOnlyRenderer = Renderer;
            ViewOnlyShortId = ShortId;
            ViewOnlyFullName = FullName;
            (ViewOnlyFullName + ViewOnlyObject + ViewOnlyRenderer + ViewOnlyShortId).ToString(); //suppress unused var warning :P
#endif
        }

        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            StateData other = obj as StateData;
            if (other == null) throw new InvalidCastException("Couldnt cast to StateData; obj=" + obj);
            return ShortId.CompareTo(other.ShortId);
        }
    }

    [Header("Automatically Populated in Awake()")]
    public StateData[] States;// { get; private set; }

    public bool StatesInitialized { get; private set; }

    public StateData GetStateByShortId(string shortId)
    {
        int index;
        if (!stateIndices.TryGetValue(shortId, out index))
            return null;
        return States[index];
    }


    readonly Dictionary<string, int> stateIndices = new Dictionary<string, int>();

    void Awake()
    {
        Init();
    }

    // Look at all child transforms and create a StateData for each one.
    public void Init()
    {
        List<StateData> stateData = new List<StateData>();
        foreach (Transform t in transform)
            stateData.Add(new StateData(t.gameObject));
        States = stateData.ToArray();
        Array.Sort(States);
        for (int i = 0; i < States.Length; i++)
            stateIndices.Add(States[i].ShortId, i);
        StatesInitialized = true;
    }

}
