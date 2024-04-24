using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Helper to convert SpriteRenderers to MeshRenderers
namespace USA_Map.Scripts_MapCreationHelpers
{
    [ExecuteInEditMode]
    public class SpriteToMeshRenderer : MonoBehaviour
    {
        [FormerlySerializedAs("CloneSource")] [SerializeField] private MeshRenderer cloneSource;
        [FormerlySerializedAs("SourceMaterials")] [SerializeField] private Material[] sourceMaterials;

        [FormerlySerializedAs("Scale")] [SerializeField] private float scale;
        [FormerlySerializedAs("DoSwap")] [SerializeField] private bool doSwap;

        private void Update()
        {
            if (!doSwap) return;
            doSwap = false;

            SwapToMesh();
        }


        private void SwapToMesh()
        {
            Sprite s = GetSprite();
            if (s == null)
                return;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null) DestroyImmediate(sr);

            GameObject newGob = Instantiate(cloneSource.gameObject) as GameObject;
            var mr = newGob.GetComponent<MeshRenderer>();

            newGob.transform.parent = cloneSource.transform.parent;
            newGob.transform.localPosition = transform.localPosition;
            newGob.transform.localScale = new Vector3(s.bounds.size.x, s.bounds.size.y, 1) * scale;
            newGob.name = gameObject.name;


            for (int i = 0; i < sourceMaterials.Length; i++)
            {
                if (sourceMaterials[i].name == gameObject.name)
                {
                    mr.sharedMaterial = sourceMaterials[i];
                    break;
                }
            }

            gameObject.SetActive(false);
        }


        private Sprite GetSprite()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null) return sr.sprite;

            Image uiImg = GetComponent<Image>();
            if (uiImg != null) return uiImg.sprite;

            return null;
        }
    }
}
