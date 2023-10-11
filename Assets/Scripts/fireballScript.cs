using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>().TakeDamage(4);
            Destroy(gameObject);
        }

        if (other.CompareTag("Objects") || other.CompareTag("Enemy"))
        {
          
            Destroy(gameObject);
        }

      
        
    }
}