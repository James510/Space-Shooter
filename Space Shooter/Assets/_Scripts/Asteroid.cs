using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    public float rot;
    public float hp;
    public float speed;
    public GameObject explosion;
    Rigidbody rb = new Rigidbody(); 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(60, 100);
        rb.AddForce(-transform.forward*speed);    
    }
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
            return;
        if (other.tag == "Asteroid")
            return;
        if (other.tag == "Bullet")
            hp--;
        if (other.tag == "Player")
            hp = 0;
    }
    void Update()
    {
        if (hp < 1)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
            
    }
	void FixedUpdate ()
    {
        rb.rotation = Quaternion.Euler(90.0f, 0.0f, rot++);
    }
}
