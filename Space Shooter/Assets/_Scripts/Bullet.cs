using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;
    private GameObject gameController;
    Rigidbody rb = new Rigidbody();
    void Start()
    {
        gameController = GameObject.Find("GameController");
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            Destroy(gameObject);
            gameController.SendMessage("Score", 5);
        }
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
            gameController.SendMessage("Score", 50);
        }
    }
}
