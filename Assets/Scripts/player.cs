using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Inventory.Model;

public class player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    Rigidbody2D rb;
    Animator animator;
    public int xp;
    public GameObject RestartButton;
    public GameObject Player;
    public AnimationClip equipAnimation;
    public EquipItemSO equipItemSO;
    public TextMeshProUGUI healthText;
    private Rigidbody2D playerRigidbody;
    

    //lights
    private Transform LeftSpotlight;
    private Transform RightSpotlight;
    private Transform TopSpotlight;
    private Transform BottomSpotlight;
    private Transform TopLeftSpotlight;
    private Transform TopRightSpotlight;
    private Transform BottomLeftSpotlight;
    private Transform BottomRightSpotlight;
    private Transform PlayerLight;

    

    //stats
    public float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public int points;
    //public static int health = 100;
    //public static int maxHealth = 100;
    //public static int Health { get => health; set => health = value; }
    //public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public Text CurrentItem;
    public Text CurrentItemDesc;

    public float range;
    public float strength;
    public float speed = 4.0f;

    public LayerMask enemyLayer;
    public GameObject arrowPrefab, stonePrefab, firePrefab, magicPrefab;

    //public int health = 100;
    [SerializeField]
    public Image healthFill;
    private float healthWidth;

    private bool isInvincible = false; // flag for invincibility frames
    private float invincibilityTime = 1.0f; // duration of invincibility in seconds
    public bool lookLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LeftSpotlight = transform.Find("LeftSpotlight");
        RightSpotlight = transform.Find("RightSpotlight");
        TopSpotlight = transform.Find("TopSpotlight");
        BottomSpotlight = transform.Find("BottomSpotlight");

        //diagonal lights
        TopLeftSpotlight = transform.Find("TopLeftSpotlight");
        TopRightSpotlight = transform.Find("TopRightSpotlight");
        BottomLeftSpotlight = transform.Find("BottomLeftSpotlight");
        BottomRightSpotlight = transform.Find("BottomRightSpotlight");
        //arrowkey lights
      

        PlayerLight = transform.Find("PlayerLight");

       
        
        if (health > 0) // only allow movement if the player is alive
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }

            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("RightSword") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Axe") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Mace")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Stick") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Staff") && !animator.GetCurrentAnimatorStateInfo(0).IsName("fire") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch")) {

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
            lookLeft = false;

            if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
            {
                if (vertical > 0)
                {
                    GetComponent<Animator>().Play("Up");
                    TopSpotlight.gameObject.SetActive(true);
                    BottomSpotlight.gameObject.SetActive(false);
                    LeftSpotlight.gameObject.SetActive(false);
                    RightSpotlight.gameObject.SetActive(false);
                    TopLeftSpotlight.gameObject.SetActive(false);
                    TopRightSpotlight.gameObject.SetActive(false);
                    BottomLeftSpotlight.gameObject.SetActive(false);
                    BottomRightSpotlight.gameObject.SetActive(false);
                    PlayerLight.gameObject.SetActive(false);
                }
                else if (vertical < 0)
                {
                    GetComponent<Animator>().Play("Down");
                    BottomSpotlight.gameObject.SetActive(true);
                    TopSpotlight.gameObject.SetActive(false);
                    LeftSpotlight.gameObject.SetActive(false);
                    RightSpotlight.gameObject.SetActive(false);
                    TopLeftSpotlight.gameObject.SetActive(false);
                    TopRightSpotlight.gameObject.SetActive(false);
                    BottomLeftSpotlight.gameObject.SetActive(false);
                    BottomRightSpotlight.gameObject.SetActive(false);
                    PlayerLight.gameObject.SetActive(false);
                }
            }

            else if (Mathf.Abs(vertical) < Mathf.Abs(horizontal))
            {
                if (horizontal > 0)
                {
                    GetComponent<Animator>().Play("Right");
                    RightSpotlight.gameObject.SetActive(true);
                    TopSpotlight.gameObject.SetActive(false);
                    BottomSpotlight.gameObject.SetActive(false);
                    LeftSpotlight.gameObject.SetActive(false);
                    TopLeftSpotlight.gameObject.SetActive(false);
                    TopRightSpotlight.gameObject.SetActive(false);
                    BottomLeftSpotlight.gameObject.SetActive(false);
                    BottomRightSpotlight.gameObject.SetActive(false);
                    PlayerLight.gameObject.SetActive(false);
                
                }
                else if (horizontal < 0)
                {
                    GetComponent<Animator>().Play("Left");
                    LeftSpotlight.gameObject.SetActive(true);
                    TopSpotlight.gameObject.SetActive(false);
                    BottomSpotlight.gameObject.SetActive(false);
                    RightSpotlight.gameObject.SetActive(false);
                    TopLeftSpotlight.gameObject.SetActive(false);
                    TopRightSpotlight.gameObject.SetActive(false);
                    BottomLeftSpotlight.gameObject.SetActive(false);
                    BottomRightSpotlight.gameObject.SetActive(false);
                    PlayerLight.gameObject.SetActive(false);
                    lookLeft = true;
                }
            }

            else
            {
                if (vertical == 0 && horizontal == 0)
                {
                    GetComponent<Animator>().Play("Idle");
                    PlayerLight.gameObject.SetActive(true);
                    LeftSpotlight.gameObject.SetActive(false);
                    TopSpotlight.gameObject.SetActive(false);
                    BottomSpotlight.gameObject.SetActive(false);
                    RightSpotlight.gameObject.SetActive(false);
                    TopLeftSpotlight.gameObject.SetActive(false);
                    TopRightSpotlight.gameObject.SetActive(false);
                    BottomLeftSpotlight.gameObject.SetActive(false);
                    BottomRightSpotlight.gameObject.SetActive(false);
                }
                else if (vertical > 0 && horizontal > 0)
                {
                    GetComponent<Animator>().Play("UpperRight");
                    TopRightSpotlight.gameObject.SetActive(true);
                    LeftSpotlight.gameObject.SetActive(false);
                    TopSpotlight.gameObject.SetActive(false);
                    BottomSpotlight.gameObject.SetActive(false);
                    RightSpotlight.gameObject.SetActive(false);
                    TopLeftSpotlight.gameObject.SetActive(false);
                    BottomLeftSpotlight.gameObject.SetActive(false);
                    BottomRightSpotlight.gameObject.SetActive(false);
                    PlayerLight.gameObject.SetActive(false);
                }
                else if (vertical < 0 && horizontal > 0)
                {
                    GetComponent<Animator>().Play("LowerRight");
                    BottomRightSpotlight.gameObject.SetActive(true);
                    TopRightSpotlight.gameObject.SetActive(false);
                    LeftSpotlight.gameObject.SetActive(false);
                    TopSpotlight.gameObject.SetActive(false);
                    BottomSpotlight.gameObject.SetActive(false);
                    RightSpotlight.gameObject.SetActive(false);
                    TopLeftSpotlight.gameObject.SetActive(false);
                    BottomLeftSpotlight.gameObject.SetActive(false);
                    PlayerLight.gameObject.SetActive(false);
                    
                }
                else if (vertical > 0 && horizontal < 0)
                {
                    GetComponent<Animator>().Play("UpperLeft");
                    TopLeftSpotlight.gameObject.SetActive(true);
                    BottomRightSpotlight.gameObject.SetActive(false);
                    TopRightSpotlight.gameObject.SetActive(false);
                    LeftSpotlight.gameObject.SetActive(false);
                    TopSpotlight.gameObject.SetActive(false);
                    BottomSpotlight.gameObject.SetActive(false);
                    RightSpotlight.gameObject.SetActive(false);
                    BottomLeftSpotlight.gameObject.SetActive(false);
                    PlayerLight.gameObject.SetActive(false);
                }
                else if (vertical < 0 && horizontal < 0)
                {
                    GetComponent<Animator>().Play("LowerLeft");
                    BottomLeftSpotlight.gameObject.SetActive(true);
                    TopLeftSpotlight.gameObject.SetActive(false);
                    BottomRightSpotlight.gameObject.SetActive(false);
                    TopRightSpotlight.gameObject.SetActive(false);
                    LeftSpotlight.gameObject.SetActive(false);
                    TopSpotlight.gameObject.SetActive(false);
                    BottomSpotlight.gameObject.SetActive(false);
                    RightSpotlight.gameObject.SetActive(false);
                    PlayerLight.gameObject.SetActive(false);
                    
                }
            }
            }
        }
        
        else // disable movement if the player is dead
        {
            rb.velocity = Vector2.zero;
            
        }

        if(health <= 0)
        {
            RestartButton.SetActive(true);
        }

        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        

        healthText.text = Mathf.Round(health) + "/" + Mathf.Round(maxHealth);
    }




    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if(fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    void Die(int health)
    {
        if(health <= 0)
        {
            RestartButton.SetActive(true);
        }
    }


    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (!isInvincible && collision.gameObject.CompareTag("Enemy"))
    //     {
    //         EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
    //         if (enemy != null)
    //         {
    //             TakeDamage(enemy.enemyType.damage);
    //         }
            
            // isInvincible = true;
            // Invoke("DisableInvincibility", invincibilityTime);
            // transform.GetChild(0).gameObject.SetActive(true);
            // Invoke("HidePlayerBlood", 0.25f);
    //     }
    // }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isInvincible)
            {
                // Player is invincible, no further action is needed
                return;
            }

            Rigidbody2D enemyRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (enemyRigidbody != null)
            {
                EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
                Vector2 pushDirection = collision.contacts[0].point - (Vector2)transform.position;
                pushDirection = pushDirection.normalized;

                float pushForce = 3f;
                TakeDamage(enemy.enemyType.damage);

                enemyRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
            isInvincible = true;
            Invoke("DisableInvincibility", invincibilityTime);
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("HidePlayerBlood", 0.25f);
        }
    }

    void HidePlayerBlood()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        
        if (health <= 0)
        {
            animator.Play("Death");
            Invoke("DestroyObject", 1f);
            //Player.SetActive(false);
            //RestartButton.SetActive(true);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    void DisableInvincibility()
    {
        isInvincible = false;
    }

     // ITEM SECTION //

    public void SetHealth(float amount) {
        health = amount;
    }

    public void FullHP()
    {
        SetHealth(maxHealth);
        lerpTimer = 0f;
    }

    public void IncreaseMaxHealth()
    {
        maxHealth *= 1.05f;
    }

    public void DecreaseMaxHealth(int value) {
        maxHealth *= 0.05f;
    }

    public void SetSpeed(float amount) {
        speed = amount;
    }    

    public void AddHealth(float amount) {
        health += amount;
        lerpTimer = 0f;
    }

    public void AddStrength(float amount) {
        strength += amount;
    }

    public void AddRange(float amount) {
        range += amount;
    }

    public void AddSpeed(float amount) {
        speed += amount;
    }
    public void AddPoint()
    {
        points += 1;
    }

    public void starAction(int value)
    {
        int randomStrength = Random.Range(-20, 20);
        float randomRange = Random.Range(-0.5f, 0.5f);
        float randomSpeed = Random.Range(-3f, 3f);

        strength += randomStrength;
        range += randomRange;
        speed += randomSpeed;
    }    

    public void invisibleCrystal(int value) {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }    

    public void crystalPrismAction(int value) {
        float percent = strength * 2.0f;
        AddStrength((int)percent);
        StartCoroutine(Invincibility(10f));
    }

    private IEnumerator Invincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        DisableInvincibility();
    }

    public void ringAction(int value) {
        AddHealth(value);
        StartCoroutine(Regen());
    }
  
    private IEnumerator Regen()
    {
        float duration = 60f;
        int rate = 2;
        float time = 0f;

        while (time < duration) {
            yield return new WaitForSeconds(1f);
            time += 1f;

            health += rate;
        }
    } 

    public void candleAction(int value) {
        StartCoroutine(Prayer());
    }

    private IEnumerator Prayer()
    {
        while (true) {
            SetSpeed(7);
            yield return new WaitForSeconds(2f);

            SetSpeed(2);
            yield return new WaitForSeconds(2f);

        }
    }

    // EQUIPPABLE ITEMS //   
    void Attack()
    {
        AgentWeapon agentWeapon = GetComponent<AgentWeapon>();

        if (agentWeapon != null)
        {
            EquipItemSO equippedWeapon = agentWeapon.GetEquippedWeapon();

            if (equippedWeapon != null && equippedWeapon.name == "Punch")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(PunchAction(equippedWeapon));
            }     

            if (equippedWeapon != null && equippedWeapon.name == "Sword")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(SwordAction(equippedWeapon));
            }     

            if (equippedWeapon != null && equippedWeapon.name == "Bow")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(BowAction());
            }

            if (equippedWeapon != null && equippedWeapon.name == "Axe")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(AxeAction(equippedWeapon));
            }

            if (equippedWeapon != null && equippedWeapon.name == "Stick")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(StickAction(equippedWeapon));
            }

            if (equippedWeapon != null && equippedWeapon.name == "Mace")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(MaceAction(equippedWeapon));
            }

            if (equippedWeapon != null && equippedWeapon.name == "Staff")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(StaffAction());
            }

            if (equippedWeapon != null && equippedWeapon.name == "Fireball")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(FireballAction());
            }

            if (equippedWeapon != null && equippedWeapon.name == "Slingshot")
            {
                equipItemSO = equippedWeapon;
                StartCoroutine(SlingshotAction());
            }                                                                                              
        }
    }

    IEnumerator PunchAction(EquipItemSO equippedWeapon)
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);


        strength = equippedWeapon.strength;
        range = equippedWeapon.range;

        if (equippedWeapon.speed > speed)
        {
            speed = equippedWeapon.speed;
        }
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(strength);
        }

        
        yield return new WaitForSeconds(0.1f);
        animator.Play("Idle");
    }

    IEnumerator SwordAction(EquipItemSO equippedWeapon)
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);


        if (equippedWeapon.strength > strength)
        {
            strength = equippedWeapon.strength;
        }

        if (equippedWeapon.speed > speed)
        {
            speed = equippedWeapon.speed;
        }

        range = equippedWeapon.range;
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(strength);
        }

        
        yield return new WaitForSeconds(0.1f);
        animator.Play("Idle");
    }

    IEnumerator AxeAction(EquipItemSO equippedWeapon)
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);


        if (equippedWeapon.strength > strength)
        {
            strength = equippedWeapon.strength;
        }

        range = equippedWeapon.range;
        speed = equippedWeapon.speed;
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(strength);
        }

        
        yield return new WaitForSeconds(0.1f);
        animator.Play("Idle");
    }

    IEnumerator StickAction(EquipItemSO equippedWeapon)
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);



        strength = equippedWeapon.strength;
        range = equippedWeapon.range;

        if (equippedWeapon.speed > speed)
        {
            speed = equippedWeapon.speed;
        }
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(strength);
        }

        
        yield return new WaitForSeconds(0.25f);
        animator.Play("Idle");
    }

    IEnumerator MaceAction(EquipItemSO equippedWeapon)
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);



        if (equippedWeapon.strength > strength)
        {
            strength = equippedWeapon.strength;
        }

        if (equippedWeapon.speed > speed)
        {
            speed = equippedWeapon.speed;
        }

        range = equippedWeapon.range;
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(strength);
        }

        
        yield return new WaitForSeconds(0.1f);
        animator.Play("Idle");
    }    


    IEnumerator BowAction() {

        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //no movement no shoot
        if (horizontalInput != 0 || verticalInput != 0) {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.AddComponent<arrowScript>();
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalInput * 5.0f, verticalInput * 5.0f);

            

            if (horizontalInput > 0)
            {
                arrow.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (horizontalInput < 0)
            {
                arrow.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (verticalInput > 0)
            {
                arrow.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (verticalInput < 0)
            {
                arrow.transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else if (verticalInput == 0)
            {
                arrow.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            
        }

        yield return new WaitForSeconds(0.0f);
        animator.Play("Idle");
    }

     IEnumerator SlingshotAction() {

        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        //no movement no shoot
        if (horizontalInput != 0) {
        GameObject stone = Instantiate(stonePrefab, transform.position, Quaternion.identity);
        stone.AddComponent<stoneScript>();
        stone.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalInput * 5.0f, 0f);      
        }

        yield return new WaitForSeconds(0.0f);
        animator.Play("Idle");
    }

    IEnumerator FireballAction() {

        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2[] velocities = new Vector2[]
        {
            new Vector2(horizontalInput * 5.0f, 5.0f),
            new Vector2(horizontalInput * 5.0f, -5.0f),
            new Vector2(-horizontalInput * 5.0f, 5.0f),
            new Vector2(-horizontalInput * 5.0f, -5.0f)
        };

        //no movement no shoot
        foreach (Vector2 velocity in velocities) {
            GameObject fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            fire.AddComponent<fireballScript>();
            fire.GetComponent<Rigidbody2D>().velocity = velocity;
        }      
        yield return new WaitForSeconds(0.2f);
        animator.Play("Idle");
    }

    IEnumerator StaffAction() {

        Animator animator = GetComponent<Animator>();
        animator.Play(equipItemSO.equipAnimation.name);

        GameObject magic = Instantiate(magicPrefab, transform.position, Quaternion.identity);
        magic.AddComponent<magicScript>();

        float speed = 8f;
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        Vector2 velocity = new Vector2(randomX, randomY).normalized * speed;

        magic.GetComponent<Rigidbody2D>().velocity = velocity;        
        
        yield return new WaitForSeconds(0.2f);
        animator.Play("Idle");
    }

                   

}