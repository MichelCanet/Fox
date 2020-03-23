using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{
    public GameObject deathEffect;

    public GameObject collectibles; // un pickup
    [Range(0, 100)]public float chanceToDrop; //proba du drop de pickup inicial

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Enemy") //si on rentre en contact avec l'enemy
        {
            Debug.Log("Enemy Toucher");

            other.transform.parent.gameObject.SetActive(false); //mort de l'enemy

            Instantiate(deathEffect, other.transform.position, other.transform.rotation); //fait apparaitre l'effet de mort

            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100f); //choix de la proba de Drop

            if (dropSelect <= chanceToDrop)// si la proba de drop est inférieur à la valeur inicial alors drop
            {
                Instantiate(collectibles, other.transform.position, other.transform.rotation);
            }

            AudioManager.instance.PlaySFX(3); //joue le son 3 de la liste d'audio du manager
        }
    }
}
