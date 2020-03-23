using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint; //deux position qui vont permettre d'établir une zone le "patrouille"

    public float moveTime, waitTime; //valeurs pour faire la pause dans le mouvenent
    private float moveCount, waitCount; //valeurs pour faire la pause dans le mouvenent

    private bool movingRight;

    public SpriteRenderer theSR; //réf au sprite de l'enemy
    private Rigidbody2D theRB;
    private Animator anim;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
        
    }
    void Update()
    {
        if (moveCount > 0) //s'il lui reste du temps pour se déplacer
        {
            moveCount -= Time.deltaTime;

            if (movingRight) //si on se déplace vers la droite
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y); //on se déplace

                theSR.flipX = true; //le sprite regarde à droite

                if (transform.position.x > rightPoint.position.x) //on arrive à l'extrémité de la "patrouille" à DROITE donc
                {
                    movingRight = false; //on avance plus vers la droite
                }
            }
            else //donc
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y); //on se déplace vers la gauche

                theSR.flipX = false; //le sprite regarde à gauche

                if (transform.position.x < leftPoint.position.x) ////on arrive à l'extrémité de la "patrouille" à GAUCHE donc
                {
                    movingRight = true; //on avance plus vers la gauche
                }
            }

            if (moveCount <= 0) //s'il ne lui reste pas de temps de déplacement
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f); //fait du pause de temps aléatoire entre
            }

            anim.SetBool("isMoving", true); //active le bool qui lance l'animation de déplacement
        }

        else if (waitTime > 0) //s'il NE lui reste PAS de temps pour se déplacer alors
        {
            waitCount -= Time.deltaTime; //fait une pause
            theRB.velocity = new Vector2(0f, theRB.velocity.y); //annule la vitesse de déplacement

            if (waitCount <= 0) //s'il ne lui reste pas de temps de pause 
            {
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 0.75f); //relance le temps de déplacement
            }

            anim.SetBool("isMoving", false); //active le bool qui lance l'animation de Idle
        }        
    }
}
