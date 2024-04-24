using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

// Simply copies the given source text once for each state and prints the state name and founded date. Results are sorted by founding date by default.
namespace USA_Map
{
    public class StateListDebug : MonoBehaviour {

        [FormerlySerializedAs("SourceText")] [SerializeField] private TextMesh sourceText;

        // Use this for initialization
        private IEnumerator Start()
        {
            var states = FindObjectOfType<StatesInfo>();
            if (states == null)
                yield break;

            while (states.AllStates.Length <= 0) yield return null;

            states.SortByFounded();

            for (int i = 0; i < states.AllStates.Length; i++)
            {
                var newText = (Instantiate(sourceText.gameObject) as GameObject).GetComponent<TextMesh>();
                newText.text = states.AllStates[i].StateId + ": " + states.AllStates[i].StateName + " - " + states.AllStates[i].Founded.ToShortDateString();
                newText.transform.parent = sourceText.transform.parent;
                newText.transform.position = sourceText.transform.position + Vector3.down * i * newText.GetComponent<Renderer>().bounds.size.y * 0.84f;
                newText.gameObject.name = states.AllStates[i].StateName;
            }
            sourceText.gameObject.SetActive(false);
        }

    }
}
