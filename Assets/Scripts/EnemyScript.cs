using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyScript : MonoBehaviour
{

    public EnemyType enemyType;
    private LevelSystem levelSystem;
    private float range;
    public Transform target;
    private bool targetCollision = false;
    private float minDistance = 5.0f;
    private float minAttack = 2.0f;
    private float thrust = 10.5f;

    private Vector3 spawnPoint;
    private float maxDistanceFromSpawn = 15.0f;
    private bool returningToSpawn = false;
    
    public float speed;
    public float health;
    public float damage;
    public int xp;
    
    public GameObject player;
    private Rigidbody2D rb2d;
    Animator animator;
    private bool isDead = false;


    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    private bool isMovingForward = true;
    float suctionSpeed = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        levelSystem = FindObjectOfType<LevelSystem>();

        
        spawnPoint = transform.position;
        
        
        target = player.transform;
        
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = 5.0f;

        currentPatrolIndex = 0;


        if (enemyType != null)
        {
            speed = enemyType.speed;
            health = enemyType.health;
            damage = enemyType.damage;
            xp = enemyType.xp;    
        }
    }

    void FixedUpdate()
    {
        player = GameObject.FindWithTag("Player");
        target = player.transform;
        range = Vector2.Distance(transform.position, target.position);


       ///////// MOVEMENT TYPE CODE: BOSS /////////

        if (enemyType.typeName == "BOSS") {
            float distanceFromSpawn = Vector3.Distance(transform.position, spawnPoint);
            if (distanceFromSpawn > maxDistanceFromSpawn)
            {
                returningToSpawn = true;
                transform.position = spawnPoint;
                health = enemyType.health;
            }
            else
            {
                returningToSpawn = false;
            }


            if (range < minDistance)
            {
                if (isDead)
                {
                    animator.Play("Death");
                    Invoke("DestroyObject", 0.5f);
                    return;
                }

                else {
                    animator.enabled = true;
                }

                if (health <= 0)
                {
                    isDead = true;
                    Invoke("DestroyObject", 1f);
                    
                }

                if (!targetCollision && returningToSpawn == false)
                {
                    transform.LookAt(target.position);
                    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    
                    Vector3 directionToEnemy = transform.position - player.transform.position;
                    directionToEnemy.Normalize();

                    Vector3 newPosition = player.transform.position + directionToEnemy * speed * Time.deltaTime;
                    player.transform.position = newPosition;
                }
            }

            else {
                animator.Play("Idle");
            }

            if (animator != null)
            {
                animator.SetFloat("Horizontal", transform.right.x);
                animator.SetFloat("Vertical", transform.right.y);
                

                if (transform.right.x < 0)
                {

                    if (range < minAttack) {
                        animator.Play("AttackL");
                        transform.LookAt(target.position);
                        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    }

                    else {
                        animator.Play("Left");
                    }
                }

                else if (transform.right.x > 0)
                {

                    if (range < minAttack) {
                        animator.Play("AttackR");
                        transform.LookAt(target.position);
                        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    }                
                    else {
                        animator.Play("Right");
                    }
                }

                
            }
            transform.rotation = Quaternion.identity;
        }
     


        ///////// MOVEMENT TYPE CODE: FOLLOW ENEMIES /////////
        if (enemyType.typeName == "Wolf" || enemyType.typeName == "Dwarf Miner" || enemyType.typeName == "Skunk" || enemyType.typeName == "Kobold" || enemyType.typeName == "Dragon"|| enemyType.typeName == "Flower") {
            float distanceFromSpawn = Vector3.Distance(transform.position, spawnPoint);
            if (distanceFromSpawn > maxDistanceFromSpawn)
            {
                returningToSpawn = true;
                transform.position = spawnPoint;
                health = enemyType.health;
            }
            else
            {
                returningToSpawn = false;
            }

            if (range < minDistance)
            {
                if (isDead)
                {
                    animator.Play("Death");
                    Invoke("DestroyObject", 0.5f);
                    return;
                }

                else {
                    animator.enabled = true;
                }

                if (health <= 0)
                {
                    isDead = true;
                    Invoke("DestroyObject", 1f);
                    
                }

                if (!targetCollision && returningToSpawn == false)
                {
                    transform.LookAt(target.position);
                    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                }          
            }

            

            else {
                animator.Play("Idle");
            }

            if (animator != null)
            {
                animator.SetFloat("Horizontal", transform.right.x);
                animator.SetFloat("Vertical", transform.right.y);
                

                if (transform.right.x < 0)
                {

                    if (range < minAttack) {
                        animator.Play("AttackL");
                    }

                    else {
                        animator.Play("Left");
                    }
                }

                else if (transform.right.x > 0)
                {

                    if (range < minAttack) {
                        animator.Play("AttackR");
                    }                
                    else {
                        animator.Play("Right");
                    }
                }

                
            }

            transform.rotation = Quaternion.identity;

        }


        ///////// MOVEMENT TYPE CODE: SUCK PLAYER IN /////////
        if (enemyType.typeName == "Fish Priest" || enemyType.typeName == "Armadillo" || enemyType.typeName == "Dwarf Korred") {
            float distanceFromSpawn = Vector3.Distance(transform.position, spawnPoint);
            if (distanceFromSpawn > maxDistanceFromSpawn)
            {
                returningToSpawn = true;
                transform.position = spawnPoint;
                health = enemyType.health;
            }
            else
            {
                returningToSpawn = false;
            }


            if (enemyType.typeName == "Dwarf Korred") {
                if (transform.right.x < 0)
                {
                    animator.Play("AttackL");
                }

                else if (transform.right.x > 0)
                {
                    animator.Play("AttackR");
                }

                if (range < minDistance)
                {
                    Vector3 directionToEnemy = transform.position - player.transform.position;
                    directionToEnemy.Normalize();

                    Vector3 newPosition = player.transform.position + directionToEnemy * suctionSpeed * Time.deltaTime;
                    player.transform.position = newPosition;
                            
                }

                if (isDead)
                {
                    animator.Play("Death");
                    Invoke("DestroyObject", 0.5f);
                    return;
                }

                else {
                    animator.enabled = true;
                }

                if (health <= 0)
                {
                    isDead = true;
                    Invoke("DestroyObject", 1f);
                    
                }                        

            }


            if (enemyType.typeName == "Armadillo")
            {
                animator.Play("Hide");

                if (range < minDistance)
                {
                    Vector3 directionToEnemy = transform.position - player.transform.position;
                    directionToEnemy.Normalize();

                    Vector3 newPosition = player.transform.position + directionToEnemy * suctionSpeed * Time.deltaTime;
                    player.transform.position = newPosition;

                    player.GetComponent<player>().TakeDamage(0.1f);
                    
                    if (isDead)
                    {
                        animator.Play("Death");
                        Invoke("DestroyObject", 0.5f);
                        return;
                    }

                    else {
                        animator.enabled = true;
                    }

                    if (health <= 0)
                    {
                        isDead = true;
                        Invoke("DestroyObject", 1f);
                        
                    }
                }
            }


            if (range < minDistance)
            {
                if (isDead)
                {
                    animator.Play("Death");
                    Invoke("DestroyObject", 0.5f);
                    return;
                }

                else {
                    animator.enabled = true;
                }

                if (health <= 0)
                {
                    isDead = true;
                    Invoke("DestroyObject", 1f);
                    
                }

                if (!targetCollision && returningToSpawn == false)
                {
                    Vector3 directionToEnemy = transform.position - player.transform.position;
                    directionToEnemy.Normalize();

                    Vector3 newPosition = player.transform.position + directionToEnemy * speed * Time.deltaTime;
                    player.transform.position = newPosition;
                }
            }

            else {
                animator.Play("Idle");
            }

            if (animator != null)
            {
                animator.SetFloat("Horizontal", transform.right.x);
                animator.SetFloat("Vertical", transform.right.y);
                

                if (transform.right.x < 0)
                {

                    if (range < minAttack) {
                        animator.Play("AttackL");
                        transform.LookAt(target.position);
                        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    }

                    else {
                        animator.Play("Left");
                    }
                }

                else if (transform.right.x > 0)
                {

                    if (range < minAttack) {
                        animator.Play("AttackR");
                        transform.LookAt(target.position);
                        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    }                
                    else {
                        animator.Play("Right");
                    }
                }

                
            }
            transform.rotation = Quaternion.identity;
        }
     

        ///////// MOVEMENT TYPE CODE: CIRCULAR MOVEMENT /////////
        if (enemyType.typeName == "Dwarf Worker" || enemyType.typeName == "Fish Knight") {
            float distanceFromSpawn = Vector3.Distance(transform.position, spawnPoint);
            if (distanceFromSpawn > maxDistanceFromSpawn)
            {
                returningToSpawn = true;
                transform.position = spawnPoint;
                health = enemyType.health;
            }
            else
            {
                returningToSpawn = false;
            }

            if (range < minDistance)
            {
                if (isDead)
                {
                    animator.Play("Death");
                    Invoke("DestroyObject", 0.5f);
                    return;
                }

                else {
                    animator.enabled = true;
                }

                if (health <= 0)
                {
                    isDead = true;
                    Invoke("DestroyObject", 1f);
                    
                }

                if (!targetCollision && returningToSpawn == false)
                {
                    Vector3 circleCenter = target.position;

                    float circleRadius = 3f;
                    float circleSpeed = 2f;

                    float angle = Time.time * circleSpeed;

                    Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * circleRadius;
                    Vector3 circlePosition = circleCenter + offset;

                    Vector3 direction = circlePosition - transform.position;
                    direction.Normalize();
                    Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
                    transform.position = newPosition;

                    transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                }   
            }

            else {
                animator.Play("Idle");
            }

            if (animator != null)
            {
                animator.SetFloat("Horizontal", transform.right.x);
                animator.SetFloat("Vertical", transform.right.y);
                

                if (transform.right.x < 0)
                {

                    if (range < minAttack) {
                        animator.Play("AttackL");
                    }

                    else {
                        animator.Play("Left");
                    }
                }

                else if (transform.right.x > 0)
                {

                    if (range < minAttack) {
                        animator.Play("AttackR");
                    }                
                    else {
                        animator.Play("Right");
                    }
                }

                
            }
            transform.rotation = Quaternion.identity;
        }

        ///////// MOVEMENT TYPE CODE: SCARED MOVEMENT /////////
        if (enemyType.typeName == "Racoon") {

            float distanceFromSpawn = Vector3.Distance(transform.position, spawnPoint);

            if (distanceFromSpawn > maxDistanceFromSpawn)
            {
                returningToSpawn = true;
                transform.position = spawnPoint;
                health = enemyType.health;
            }

            else
            {
                returningToSpawn = false;
            }

            if (range < minDistance)
            {
                if (isDead)
                {
                    animator.Play("Death");
                    Invoke("DestroyObject", 0.5f);
                    return;
                }

                else {
                    animator.enabled = true;
                }

                if (health <= 0)
                {
                    isDead = true;
                    Invoke("DestroyObject", 1f);
                    
                }

                //Zigzag movement that makes the enemies appear scared.
                if (!targetCollision && returningToSpawn == false)
                {
                    bool zigzag = true;

                    if (zigzag) {
                        float zigzagSpeed = 3f;
                        float zigzagDistance = 3f;

                        float zigzagDirectionX = Mathf.PerlinNoise(Time.time * zigzagSpeed, 0f) * zigzagDistance * 2f - zigzagDistance;
                        float zigzagDirectionY = Mathf.PerlinNoise(Time.time * zigzagSpeed, 100f) * zigzagDistance * 2f - zigzagDistance;
                        
                        Vector3 zigzagOffset = new Vector3(zigzagDirectionX, zigzagDirectionY, 0f);
                        Vector3 newPosition = transform.position + zigzagOffset * speed * Time.deltaTime;
                        transform.position = newPosition;

                        transform.LookAt(target.position);
                        transform.Rotate(new Vector3(0f, -90f, 0f), Space.Self);                     
                    }
                    

                }   
            }

            else {
                animator.Play("Idle");
            }

            if (animator != null)
            {
                animator.SetFloat("Horizontal", transform.right.x);
                animator.SetFloat("Vertical", transform.right.y);
                

                if (transform.right.x < 0)
                {
                    animator.Play("Left");
                }

                else if (transform.right.x > 0)
                {
                    animator.Play("Right");
                }               
            }
            transform.rotation = Quaternion.identity;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !targetCollision)
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            targetCollision = true;
            print(targetCollision);

            bool right = contactPoint.x > center.x;
            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            bool bottom = contactPoint.y < center.y;

            if (right) rb2d.AddForce(transform.right * thrust, ForceMode2D.Impulse);
            if (left) rb2d.AddForce(-transform.right * thrust, ForceMode2D.Impulse);
            if (top) rb2d.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            if (bottom) rb2d.AddForce(-transform.up * thrust, ForceMode2D.Impulse);

            Invoke("ResetCollision", 0.5f);
            rb2d.velocity = Vector3.zero;
        }
    }

    void ResetCollision()
    {
        targetCollision = false;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        transform.GetChild(0).gameObject.SetActive(true);
        Invoke("HideBlood", 0.25f);
        if (health <= 0)
        {
            isDead = true;
            Invoke("DestroyObject", 1f);

        }
    }

    private void DestroyObject()
    {
        if(enemyType != null && levelSystem != null)
        {
            levelSystem.GainExperienceFlatRate(enemyType.xp);
        }
        Destroy(gameObject);
    }

    void HideBlood()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
