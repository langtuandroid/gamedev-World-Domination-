using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapRankAnimator : MonoBehaviour
{
    public StatesInfo StateInfo;
    public TerritoryManager TerritoryMgr;

    public Text ModeDisplayText;
    public Button PlayPauseButton;
    public Sprite PlaySprite, PauseSprite;

    public float AnimationTime;

    public Color DefaultColor;

    public Gradient FoundedColors, PopulationColors, LandAreaColors;


    public void TogglePause() { SetPaused(!isPaused); }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;
        if (!isPaused && pos >= 1)
            Reset(mode);
    }

    public void Reset(int mode)
    {
        this.mode = mode;
        isPaused = false;
        pos = 0;
    }

    int mode = 0;
    bool isPaused = false;
    float pos = 0;

    void Awake()
    {
        Reset(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused) updatePlaying();
        PlayPauseButton.image.sprite = (!isPaused ? PauseSprite : PlaySprite);
    }

    void updatePlaying()
    {
        if (StateInfo.AllStates.Length < 1)
            return;

        Gradient g;

        if (mode == 0)
        {
            g = FoundedColors;
            ModeDisplayText.text = "Sorting by Founding Date";
            StateInfo.SortByFounded();
        }
        else if (mode == 1)
        {
            g = PopulationColors;
            ModeDisplayText.text = "Sorting by Population";
            StateInfo.SortByPopulation();
        }
        else if (mode == 2)
        {
            g = LandAreaColors;
            ModeDisplayText.text = "Sorting by Total Land Area";
            StateInfo.SortByLandArea();
        }
        else throw new UnityException("Unknown mode=" + mode);

        pos = Mathf.Min(1, pos + Time.deltaTime / AnimationTime);

        for (int i = 0, count = StateInfo.AllStates.Length; i < count; i++)
        {
            Color c = DefaultColor;
            float p = (i / (count - 1f));
            if (pos >= p)
                c = g.Evaluate(p);

            var stateData = TerritoryMgr.GetStateByShortId(StateInfo.AllStates[i].StateId);

            if(stateData.UiImage.color != c)
            {
                stateData.UiImage.color = c;
                if (c != DefaultColor)
                    StartCoroutine(scaleAnim(stateData.Object.transform));
            }
        }

        if (pos >= 1) isPaused = true;
    }

    IEnumerator scaleAnim(Transform t)
    {
        Vector3 scale = t.localScale;
        t.localScale *= 1.1f;
        yield return null;
        yield return null;
        yield return null;
        t.localScale = scale;
    }
}
