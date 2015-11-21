using UnityEngine;
using System.Collections;

public class EnemyBulletCore : MonoBehaviour
{
    public int shotType;
    public float speed;
    Rigidbody rb = new Rigidbody();

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
