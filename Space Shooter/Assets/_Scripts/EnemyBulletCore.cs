using UnityEngine;
using System.Collections;

public class EnemyBulletCore : MonoBehaviour
{
    public EnemyCore.ShotPathType shotType;
    public float speed;
    Rigidbody rb = new Rigidbody();
	float nextMove;

	void BulletPath(EnemyCore.ShotPathType n){
		shotType = n;
		nextMove = Time.time + .1f;
		if(shotType == EnemyCore.ShotPathType.Wiggle) speed/=3;
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
	}
	void FixedUpdate(){
		if (shotType == EnemyCore.ShotPathType.CurveLeft)
			rb.AddForce (-transform.right * Time.deltaTime * speed*20);
		if (shotType == EnemyCore.ShotPathType.CurveRight)
			rb.AddForce (transform.right * Time.deltaTime * speed*20);
		if (shotType == EnemyCore.ShotPathType.Wiggle)
			CommenceWiggle();
	}
	void CommenceWiggle(){
		if (Time.time > nextMove) {
			nextMove = Time.time + .1f;
			if(nextMove % .8f <= .3f){
				rb.velocity += transform.right * (speed/2);
				Debug.Log("going right");
			}
			else if (nextMove % .8f <= .4f){
				rb.velocity -= transform.right * (speed/2);
				Debug.Log("going forward");
			}
			else if (nextMove % .8f <= .7f){
				rb.velocity -= transform.right * (speed/2);
				Debug.Log("going left");
			}
			else {
				rb.velocity += transform.right * (speed/2);
				Debug.Log("going forward");
			}
		}
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
