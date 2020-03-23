﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; //permet d'avoir accès depuis les autres scripts

    public float waitToRespawn; //temps avant de réaparaitre dans le niveau

    public int gemsCollected; //permet de savoir combien de Gem le joueur a

    public string levelToLoad;

    private void Awake()
    {
        instance = this; //permet d'avoir accès depuis les autres scripts
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo() //Coroutine
    {
        PlayerController.instance.gameObject.SetActive(false); //mort

        AudioManager.instance.PlaySFX(8); //joue le son 8 de la liste d'audio du manager

        yield return new WaitForSeconds(waitToRespawn - (1f / UiControler.instance.fadeSpeed)); //attendre x temps pour reapawn le joueur et affiche le 

        UiControler.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UiControler.instance.fadeSpeed) + 0.2f);

        UiControler.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true); //respawn

        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint; //spawn au dernier checkpoint

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth; //retour des points de vies
        UiControler.instance.UpdateHealthDisplay(); //mettre à jour l'UI
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        PlayerController.instance.stopInput = true; //déactiver les mouvement du Player dans PlayerController

        CameraControler.instance.stopFollow = true; //déactive le suivi de la cam du Player

        UiControler.instance.levelCompleteText.SetActive(true); //affiche level complete

        yield return new WaitForSeconds(1.5f);

        UiControler.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UiControler.instance.fadeSpeed) + 0.25f);

        SceneManager.LoadScene(levelToLoad);
    }
}