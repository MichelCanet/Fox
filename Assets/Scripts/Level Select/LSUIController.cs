using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{
    public static LSUIController instance;

    public Image fadeScreen; //écran à la mort du Player
    public float fadeSpeed; //vitesse pour afficher le Fade
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;

    public Text levelName, gemsFound, gemsTarget, bestTime, timeTarget; //ref au different element de UI

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
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

    public void ShowInfo(MapPoint levelInfo) // affiche les infos du niveau, les infos se trouvent sur les Map points de niveau
    {
        levelName.text = levelInfo.levelName; //affiche le nom

        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

        timeTarget.text = "TARGET: " + levelInfo.targetTime + "s";
        if (levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST: ---";
        }
        else
        {
            bestTime.text = "BEST: " + levelInfo.bestTime.ToString("F2") + "s"; //F2 = deux décimal après le virtuge
        }

        levelInfoPanel.SetActive(true);
    }
    public void HideInfo() // cache les infos du niveau
    {
        levelInfoPanel.SetActive(false);
    }
}
