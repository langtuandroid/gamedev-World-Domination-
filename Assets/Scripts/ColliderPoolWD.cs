using UnityEngine;

public class ColliderPoolWD : MonoBehaviour
{
    [SerializeField] private Transform colL;
 
    private void Start()
    {
        GenerateCollidersS();
    }

    private void GenerateCollidersS() {
        for (int i = 0; i < 256; i++) {
            Transform c = Instantiate(colL, this.transform);
            c.gameObject.SetActive(false);
        }
    }


    public Transform GetColliderR(Transform p) {
        Transform t = this.transform.GetChild(0);
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(0).parent = p;
        return t;


    }
    public void RemoveColliderR(Transform c)
    {
        c.parent = this.transform;
        c.gameObject.SetActive(false);
    }
}
