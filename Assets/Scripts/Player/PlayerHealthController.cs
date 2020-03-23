using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; //permet d'avoir accès depuis les autres scripts

    public int currentHealth, maxHealth;

    public float invincibleLenght; //combien de temps le joueur est immortel
    private float invincibleCounter; //conte à rebours de l'immortalité

    private SpriteRenderer SR; //display Immortalité

    public GameObject deathEffect; //l'effet de mort

    private void Awake()
    {
        instance = this; //permet d'avoir accès depuis les autres scripts
    }

    void Start()
    {
        currentHealth = maxHealth;

        SR = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (invincibleCounter > 0) //compteà rebours du temps d'immortalité
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0) //system d'immortalité
        {
            currentHealth -= 1; //si dégat on retire 1 à la vie

            if (currentHealth <= 0) //system de mort
            {
                currentHealth = 0;

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer(); //active le respawn sur le script Level Manager
            }
            else
            {
                invincibleCounter = invincibleLenght;
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.5f);//modification de l'alpha pour la transparence de l'immortalité

                PlayerController.instance.KnockBack();

                AudioManager.instance.PlaySFX(9); //joue le son 9 de la liste d'audio du manager
            }

            UiControler.instance.UpdateHealthDisplay(); // active la fonction dans le script UIControler
        }       
    }

    public void HealPlayer()
    {
        currentHealth++; //On peut faire aussi currentHealth = maxHealth

        if (currentHealth > maxHealth) //Sécurité
        {
            currentHealth = maxHealth;
        }

        UiControler.instance.UpdateHealthDisplay(); //mise à jour de UI
    }
}
