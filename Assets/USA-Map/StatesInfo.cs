using UnityEngine;
using System.Collections.Generic;

public class StatesInfo : MonoBehaviour
{
    public class StateInfo
    {
        public StateInfo() { }

        public StateInfo(string tabSeparatedString)
        {
            string[] parts = tabSeparatedString.Split('\t');
            if (parts.Length < 6) throw new UnityException("Couldn't parse line=" + tabSeparatedString);

            if (!System.DateTime.TryParse(parts[3], out Founded)) throw new UnityException("Unable to parse date string[2]=" + parts[2]);
            if (!long.TryParse(parts[2].Replace(",", ""), out Population)) throw new UnityException("Unable to parse numeric string[1]=" + parts[1]);
            if (!long.TryParse(parts[5].Replace(",", ""), out TotalArea)) throw new UnityException("Unable to parse numeric string[4]=" + parts[4]);
            if (!long.TryParse(parts[6].Replace(",", ""), out LandArea)) throw new UnityException("Unable to parse numeric string[5]=" + parts[5]);

            if ((StateName = parts[0].Trim()).Length == 0) throw new UnityException("StateName is empty!");
            if ((StateId = parts[1].Trim()).Length == 0) throw new UnityException("StateId is empty!");
            if ((Capitol = parts[4].Trim()).Length == 0) throw new UnityException("Capitol is empty!");
        }

        public string StateName;
        public string StateId;
        public string Capitol;

        public System.DateTime Founded;
        public long Population;
        public long TotalArea;
        public long LandArea;

        public float WaterPercent { get { return 1 - LandArea / TotalArea; } }
    }

    public string StateInfoResourceFile;

    public StateInfo[] AllStates;

    // Use this for initialization
    void Awake()
    {
        List<StateInfo> info = new List<StateInfo>();
        TextAsset ta = Resources.Load<TextAsset>(StateInfoResourceFile);
        string[] lines = ta.text.Split('\r', '\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (line.Length > 0)
                try { info.Add(new StateInfo(line)); }
                catch (System.Exception ex)
                {
                    Debug.LogError("Failed to parse line " + i + ", ex=" + ex);
                }

        }
        AllStates = info.ToArray();


        SortByFounded();
        for(int i = 0; i < AllStates.Length; i++)
        {
            Debug.Log(AllStates[i].StateName + " founded " + AllStates[i].Founded.ToShortDateString());
        }
    }

    public void SortByFounded()
    {
        System.Array.Sort(AllStates, delegate (StateInfo s1, StateInfo s2) { return (s1.Founded == s2.Founded ? s1.StateName.CompareTo(s2.StateName) : s1.Founded.CompareTo(s2.Founded)); });
    }

    public void SortByPopulation()
    {
        System.Array.Sort(AllStates, delegate (StateInfo s1, StateInfo s2) { return s1.Population.CompareTo(s2.Population); });
    }

    public void SortByLandArea()
    {
        System.Array.Sort(AllStates, delegate (StateInfo s1, StateInfo s2) { return s1.TotalArea.CompareTo(s2.TotalArea); });
    }
}

