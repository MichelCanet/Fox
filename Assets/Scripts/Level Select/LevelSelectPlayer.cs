using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour
{
    public MapPoint currentPoint; //ou se trouve le player

    public float moveSpeed = 10f;

    private bool levelLoading;

    public LSManager theManager;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f && !levelLoading) //Mouvement dans le Level Select
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f) //si le joueur appuie sur les touches "Horizontal droite" alors
            {
                if (currentPoint.right != null) //Si la "Right" est possible alors
                {
                    SetNextPoint(currentPoint.right);
                }
            }

            else if (Input.GetAxisRaw("Horizontal") < -0.5f) //si le joueur appuie sur les touches "Horizontal gauche" alors
            {
                if (currentPoint.left != null) //Si la "Left" possible alors
                {
                    SetNextPoint(currentPoint.left);
                }
            }

            else if (Input.GetAxisRaw("Vertical") > 0.5f) //si le joueur appuie sur les touches "Vertical up" alors
            {
                if (currentPoint.up != null) //Si la "Up" est possible alors
                {
                    SetNextPoint(currentPoint.up);
                }
            }

            else if (Input.GetAxisRaw("Vertical") < -0.5f) //si le joueur appuie sur les touches "Vertical down" alors
            {
                if (currentPoint.down != null) //Si la "Down" possible alors
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked) //si la position du player est un niveau ET dispose d'un niveau à charger ET n'est pas bloqué alors
            {
                LSUIController.instance.ShowInfo(currentPoint); //lance la fonction sur UI Controller

                if (Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;

                    theManager.LoadLevel();
                }
            }
        }       
    }

    public void SetNextPoint(MapPoint nextPoint) 
    {
        currentPoint = nextPoint;

        LSUIController.instance.HideInfo(); //lance la fonction sur UI Controller
    }
}
