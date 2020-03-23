using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Hit");
            // FindObjectOfType<PlayerHealthController>().DealDamage(); //Méthode barbare pour trouver la vie du Player
            PlayerHealthController.instance.DealDamage(); //Va chercher la fonction Deal Damage grâce au public static
        }       
    }
}
