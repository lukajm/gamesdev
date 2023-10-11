using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the arrow collided with a specific object (e.g. an enemy)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(2);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Objects"))
        {
          
            Destroy(gameObject);
        }

      
        
    }
}
