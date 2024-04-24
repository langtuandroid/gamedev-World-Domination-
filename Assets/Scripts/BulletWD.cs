using UnityEngine;
using UnityEngine.Serialization;

public class BulletWD : MonoBehaviour
{
    [FormerlySerializedAs("target")] public Transform targetT;
    private readonly float speed = 5f;
    private Vector3 dir = Vector3.zero;
    private float timer = 5f;
    
    private void Update()
    {
        if (targetT != null)
        {
            dir = (targetT.position +Vector3.up*.5f- this.transform.position).normalized;
            transform.position += dir * (speed * Time.deltaTime);
        }
        else {
            transform.position += dir * (speed * Time.deltaTime);
        }
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) {
            other.transform.GetComponent<Stickman>().Hit();
            Destroy(transform.gameObject);
        }
    }
}
