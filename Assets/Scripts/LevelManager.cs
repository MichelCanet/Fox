using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; //permet d'avoir accès depuis les autres scripts

    public float waitToRespawn; //temps avant de réaparaitre dans le niveau

    public int gemsCollected; //permet de savoir combien de Gem le joueur a

    public string levelToLoad;

    public float timeInLevel;

    private void Awake()
    {
        instance = this; //permet d'avoir accès depuis les autres scripts
    }

    void Start()
    {
        timeInLevel = 0; //sécurité pour que le niveau soit tjr 0
    }

    void Update()
    {
        timeInLevel += Time.deltaTime;
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

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1); //permet de débloquer le niveau suivant à la fin d'un niveau

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems")) //Sécurité pour tjr avoir le nombre max de gems
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected); //permet de sauvegarder le nombre de gems que l'on a trouvé dans le niveau
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected); //permet de sauvegarder le nombre de gems que l'on a trouvé dans le niveau
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time")) //Sécurité pour tjr sauvegarder le meilleur temps
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel); //permet de sauvegarder le temps total pour faire le Level
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel); //permet de sauvegarder le temps total pour faire le Level
        }

        SceneManager.LoadScene(levelToLoad);
    }
}
