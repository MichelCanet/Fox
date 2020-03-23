using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public static CameraControler instance;

    public Transform target; //Qui est la cible de la cam.

    public Transform farBackground, middleBackground; //deux Transform pour faire du Parallax
    // private float lastXPos; //variable qui aide le Parralax en X
    private Vector2 lasPos; //variables Parralax X et Y

    public float minHeight, maxHeight; //Cam max et min hauteur en y

    public bool stopFollow;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //lastXPos = transform.position.x;
        lasPos = transform.position;
    }


    void Update()
    {
        /*
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); //Pemet de suivre la cible
        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        */
        if (!stopFollow)
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z); //permet de suivre la cible mais aussi d'avoir une hauteur Y Min et Max

            // Parallax Horizontal et Vertical
            //float amountToMoveX = transform.position.x - lastXPos;
            Vector2 amountToMove = new Vector2(transform.position.x - lasPos.x, transform.position.y - lasPos.y);

            farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;
            //lastXPos = transform.position.x;
            lasPos = transform.position;
        }        
    }
}
