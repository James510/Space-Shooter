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
    private float flash;
    private bool flashOn;
    private bool fire;
    private bool invincible;
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
        invincible = false;
        flashOn = true;
    }

    void Update()
    {
        if(fire && Time.time > nextFire)
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
            GetComponent<Renderer>().enabled = false;
            invincible = true;
            hp = 1;
            transform.position=new Vector3(0.0f,1.0f,0.0f);
            StartCoroutine("InvincibleWait", 5.0f);
        }
        if(invincible)
        {
            if (Time.time > flash)
            {
                flash = Time.time + 0.1f;
                if (flashOn)
                {
                    GetComponent<Renderer>().enabled = true;
                    flashOn = false;
                }
                else
                {
                    GetComponent<Renderer>().enabled = false;
                    flashOn = true;
                }
            }
        }
    }
    void Fire (bool shoot)
    {
        fire = shoot;
    }
	void FixedUpdate()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal * speed, 0.0f, vertical * speed);
        rb.velocity = movement;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 1.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        rb.rotation = Quaternion.Euler((rb.velocity.x * -tilt) + 90.0f, 270.0f, 0.0f);
    }

    IEnumerator InvincibleWait(float f)
    {
        yield return new WaitForSeconds(f);
        GetComponent<Renderer>().enabled = true;
        invincible = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(!invincible)
        {
            if (other.tag == "Asteroid")
            {
                hp -= 1;
            }
            if (other.tag == "EnemyBullet")
            {
                hp -= 1;
            }
        } 
    }
}
