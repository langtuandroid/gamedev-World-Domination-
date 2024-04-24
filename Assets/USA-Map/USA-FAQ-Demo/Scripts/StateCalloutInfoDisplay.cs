using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateCalloutInfoDisplay : MonoBehaviour
{
    public StatesInfo StateInfo;
    public StateCalloutAnimator CalloutAnim;

    public Text StateNameDisplay;
    public Text CapitolDisplay;
    public Text FoundedDateDisplay;
    public Text FoundedRank;
    public Text PopulationDisplay;
    public Text PopulationRank;
    public Text LandAreaDisplay;
    public Text LandAreaRank;

    void Update()
    {
        string state = CalloutAnim.StateToCall;
        if (string.IsNullOrEmpty(state))
            return;

        var info = getInfoForId(state);
        if (info == null)
            return;

        StateNameDisplay.text = info.StateName + " (" + info.StateId + ")";
        CapitolDisplay.text = info.Capitol;
        FoundedDateDisplay.text = info.Founded.ToShortDateString();
        PopulationDisplay.text = info.Population.ToString("N0");
        LandAreaDisplay.text = info.TotalArea.ToString("N0") + " sq/mi";

        StateInfo.SortByFounded();
        FoundedRank.text = getRankString(System.Array.IndexOf(StateInfo.AllStates, info) + 1);
        StateInfo.SortByPopulation();
        PopulationRank.text = getRankString(StateInfo.AllStates.Length - System.Array.IndexOf(StateInfo.AllStates, info));
        StateInfo.SortByLandArea();
        LandAreaRank.text = getRankString(StateInfo.AllStates.Length - System.Array.IndexOf(StateInfo.AllStates, info));
    }

    StatesInfo.StateInfo getInfoForId(string id)
    {
        for (int i = 0; i < StateInfo.AllStates.Length; i++)
            if (StateInfo.AllStates[i].StateId == id)
                return StateInfo.AllStates[i];

        return null;
    }

    string getRankString(int rank)
    {
        string end = "th";
        if (rank != 11 && rank % 10 == 1) end = "st";
        if (rank != 12 && rank % 10 == 2) end = "nd";
        if (rank != 13 && rank % 10 == 3) end = "rd";
        return string.Format("({0}{1})", rank, end);
    }
}
