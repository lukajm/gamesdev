using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>().TakeDamage(10);
            Destroy(gameObject);
        }

        if (other.CompareTag("Objects"))
        {
            Destroy(gameObject);
        }
    }
}
