using UnityEngine;
using System.Collections;

public class EnemyCore : MonoBehaviour
{
    private int hp;
    private int movementPath;
    private int speed;
    private int movementSeq;
    private float fireRate;
    private float nextFire;
    private float nextMove;
    private string enemyType;
    public GameObject shot;
    public GameObject gunSFX;
    public GameObject explosion;
    private Rigidbody rb = new Rigidbody();
    public Transform gun1, gun2, gun3, gun4;
	public enum ShotPathType {Forward,CurveLeft,CurveRight,Wiggle};

    void Start ()
    {
        enemyType = this.gameObject.name;
        rb = GetComponent<Rigidbody>();
        if (enemyType == "BasicEnemyShip1")
        {
            rb.rotation = Quaternion.Euler(-90.0f, 90.0f, 0.0f);
            nextMove = Time.time + 1;
            fireRate = 1;
            hp = 5;
            movementPath = 1;
            speed = 50;
            movementSeq = 1;
        }
        
        //Debug.Log(enemyType);
    }
	
	void Update ()
    {
        if (Time.time > nextFire)
        {
			ShotPathType shotPathType = (ShotPathType)0;
			if(Random.Range (0,3)==0)
				shotPathType = (ShotPathType)Random.Range (1,System.Enum.GetNames(typeof(ShotPathType)).Length);
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(shot, gun1.position, gun1.rotation) as GameObject;
			bullet.SendMessage ("BulletPath",shotPathType);
			gunSFX.GetComponent<AudioSource>().Play();
        }
        if (hp < 1)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        //Vector3 movement = new Vector3(0.0f, 0.0f, -speed*1.0f);
        //rb.velocity = movement;
        
        if (movementPath == 1)
        { 
            if (Time.time > nextMove)
            {
                nextMove = Time.time + 1;
                if (movementSeq == 1)
                {
                    rb.AddForce(transform.right * speed);
                    movementSeq = 2;
                }
                else if (movementSeq == 2)
                {
                    rb.AddForce(transform.up * speed);
                    movementSeq = 3;
                }
                else if (movementSeq == 3)
                {
                    rb.AddForce(-transform.up * speed);
                    movementSeq = 4;
                }
                else if (movementSeq == 4)
                {
                    rb.AddForce(-transform.up * speed);
                    movementSeq = 5;
                }
                else if (movementSeq == 5)
                {
                    rb.AddForce(transform.up * speed);
                    movementSeq = 2;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hp -= 1;
        }
    }
    IEnumerator waitForSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}