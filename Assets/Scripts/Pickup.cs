using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal;

    private bool isCollected;

    public GameObject pickupEffect;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") & isCollected == false) //si le joueur prend la Gem/Vie et elle n'est pas encore prise alors
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++; //ajoute 1 quand Joueur prend une Gem

                isCollected = true; //Gem collecté

                AudioManager.instance.PlaySFX(6); //joue le son 6 de la liste d'audio du manager

                Destroy(gameObject); //disparition Gem

                Instantiate(pickupEffect, transform.position, transform.rotation); //l'effet est créé à l'endroit du pickUp

                UiControler.instance.UpdateGemCount(); //mise à jour de UI
            }

            if (isHeal)
            {
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth) //si le Joueur a perdus de la vie alors
                {
                    PlayerHealthController.instance.HealPlayer();
                    isCollected = true; //Heal collecté

                    AudioManager.instance.PlaySFX(7); //joue le son 7 de la liste d'audio du manager

                    Destroy(gameObject); //disparition Heal

                    Instantiate(pickupEffect, transform.position, transform.rotation); //l'effet est créé à l'endroit du pickUp
                }
            }
        }
    }
}
