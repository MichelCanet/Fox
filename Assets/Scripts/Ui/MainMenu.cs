using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(startScene); //lance la scene que l'on a règlé dans la "string"
    }
    public void QuitGame()
    {
        Application.Quit(); //ferme le jeu
        Debug.Log("Jeu Quitter");
    }
}
