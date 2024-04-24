using UnityEngine;
using UnityEngine.Serialization;

public class BulletWD : MonoBehaviour
{
    [FormerlySerializedAs("target")] public Transform targetT;
    private readonly float speedD = 5f;
    private Vector3 dirR = Vector3.zero;
    private float timerR = 5f;
    
    private void Update()
    {
        if (targetT != null)
        {
            dirR = (targetT.position +Vector3.up*.5f- this.transform.position).normalized;
            transform.position += dirR * (speedD * Time.deltaTime);
        }
        else {
            transform.position += dirR * (speedD * Time.deltaTime);
        }
        timerR -= Time.deltaTime;
        if (timerR <= 0f) {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) {
            other.transform.GetComponent<StickmanWD>().HitT();
            Destroy(transform.gameObject);
        }
    }
}
