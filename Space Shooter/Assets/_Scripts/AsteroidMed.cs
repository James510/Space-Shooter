using UnityEngine;
using System.Collections;

public class AsteroidMed : MonoBehaviour
{
    public float rot;
    public float hp;
    public float speed;
    public GameObject explosion;
    public GameObject asteroidSmall;
    Rigidbody rb = new Rigidbody(); 
    void Start()
    {
        rot=0;
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(40, 60);
        rb.AddForce(-transform.forward * speed);
        
    }
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
            return;
        if (other.tag == "Asteroid")
            return;
        if(other.tag == "Bullet")
            hp--;
        if (other.tag == "Player")
            hp = 0;
    }
    void Update()
    {
        if (hp < 1)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(asteroidSmall,transform.position, Quaternion.Euler(0.0f, Random.Range(-45.0f, 45.0f), 0.0f));
            Instantiate(asteroidSmall, transform.position, Quaternion.Euler(0.0f, Random.Range(-45.0f, 45.0f), 0.0f));
            Instantiate(asteroidSmall, transform.position, Quaternion.Euler(0.0f, Random.Range(-45.0f, 45.0f), 0.0f));
            Destroy(gameObject);
        }
    }
	void FixedUpdate ()
    {
        rb.rotation = Quaternion.Euler(90.0f, 0.0f, rot++);
    }
}
