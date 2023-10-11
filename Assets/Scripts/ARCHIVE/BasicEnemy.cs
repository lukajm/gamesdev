using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private float range;
    public Transform target;
    private bool targetCollision = false;
    private float minDistance = 5.0f;
    private float speed = 3.0f;
    private float thrust = 1.5f;
    public int health = 5;
    public GameObject player;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //vector of distance between enemy transform and its targets transform (transform = position basically)
        range = Vector2.Distance(transform.position, target.position);
        if (range < minDistance)
        {
            if (!targetCollision)
            {
                //gets the position of the target
                transform.LookAt(target.position);

                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
        }
        transform.rotation = Quaternion.identity;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //detect if collision is with the player
        if (collision.gameObject.CompareTag("Player") && !targetCollision)
        {
            //getting the point of contact and the center of player
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            targetCollision = true;

            //determining which side the enemy is colliding with the player
            bool right = contactPoint.x > center.x;
            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            bool bottom = contactPoint.y < center.y;

            if (right) GetComponent<Rigidbody2D>().AddForce(transform.right * thrust, ForceMode2D.Impulse);
            if (left) GetComponent<Rigidbody2D>().AddForce(-transform.right * thrust, ForceMode2D.Impulse);
            if (top) GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);
            if (bottom) GetComponent<Rigidbody2D>().AddForce(-transform.up * thrust, ForceMode2D.Impulse);
            Invoke("FalseCollision", 0.5f);
        }




    }

    void FalseCollision()
    {
        targetCollision = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        transform.GetChild(0).gameObject.SetActive(true);
        Invoke("HideBlood", 0.25f);
    }

    void HideBlood()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
