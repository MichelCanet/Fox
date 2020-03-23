using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    public static UiControler instance; //permet d'avoir accès depuis les autres scripts

    public Image heart01, heart02, heart03; //ref au 3 coeur de l'écran
    public Sprite heartFull, heartEmpty; //stocke les image de coeur plein et vide

    public Text gemText;

    public Image fadeScreen; //écran à la mort du Player
    public float fadeSpeed; //vitesse pour afficher le Fade
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelCompleteText;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        UpdateGemCount();

        FadeFromBlack();
    }
    void Update()
    {
        if (shouldFadeToBlack) //fondu au noir
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime)); //help
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)//fondu inverse du noir
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime)); //help
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }
    public void UpdateHealthDisplay()
    {
        switch (PlayerHealthController.instance.currentHealth) //permet de faire les différentes possibilitées de vie du player
        {
            case 3: //Player 3 vie
                heart01.sprite = heartFull;
                heart02.sprite = heartFull;
                heart03.sprite = heartFull;
                break;
            case 2: //Player 2 vie
                heart01.sprite = heartFull;
                heart02.sprite = heartFull;
                heart03.sprite = heartEmpty;
                break;
            case 1: //Player 1 vie
                heart01.sprite = heartFull;
                heart02.sprite = heartEmpty;
                heart03.sprite = heartEmpty;
                break;
            case 0: //Player plus de vie
                heart01.sprite = heartEmpty;
                heart02.sprite = heartEmpty;
                heart03.sprite = heartEmpty;
                break;
            default: //Une sécurité, si jamais la vie tomb en négatif, évite les erreurs
                heart01.sprite = heartEmpty;
                heart02.sprite = heartEmpty;
                heart03.sprite = heartEmpty;
                break;
        }
    }

    public void UpdateGemCount()
    {
        gemText.text = LevelManager.instance.gemsCollected.ToString(); //transforme la variable gemsCollected en texte
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
