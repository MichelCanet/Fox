using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance; //permet d'avoir accès depuis les autres scripts
    public string levelSelect, mainMenu;

    public GameObject pauseScreen; //réf. au panel pause
    public bool isPaused; //vérif. si la pause est activée


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }
    public void PauseUnPause()
    {
        if (isPaused) //si la pause activé
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f; //reprend le temps
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f; //stop le temps
        }
    }
    public void LevelSelect()
    {
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(levelSelect); // retour à la selection des niveaux
        Time.timeScale = 1f; // sécu pour ne pas load un niveau avec la pause en activé
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu); // permet de revenir au main menu
        Time.timeScale = 1f; // sécu pour ne pas load un niveau avec la pause en activé
    }
}
