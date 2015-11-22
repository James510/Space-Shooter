using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class playerMovement : MonoBehaviour
{
    public int hp;
    public float speed;
    public float tilt;
    public float fireRate;
    private float nextFire;
    public GameObject shot;
    public GameObject gunSFX;
    public GameObject explosion;
    public Joystick jstick;
    public Boundary boundary;
    public Transform gun1,gun2;
    private Rigidbody rb = new Rigidbody();

    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Z) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, gun1.position, gun1.rotation);
            Instantiate(shot, gun2.position, gun2.rotation);
            gunSFX.GetComponent<AudioSource>().Play();
        }
        if(hp<1)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

	void FixedUpdate()
    {
        Vector3 movement = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxis("Vertical"))*speed;
        rb.velocity = movement;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 1.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        rb.rotation = Quaternion.Euler((rb.velocity.x * -tilt) + 90.0f, 270.0f, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Asteroid")
        {
            hp -= 1;
        }
        if (other.tag == "EnemyBullet")
        {
            hp -= 1;
        }
    }
}
