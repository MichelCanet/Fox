using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; //permet d'avoir accès depuis les autres scripts

    public float moveSpeed; //Vitesse de déplacement
    public Rigidbody2D RB; //Réf au Player
    public float jumpForce; //force du saut

    private bool isGrounded; //touche le sol ou non
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer SR;

    public float knockBackLength, knockBackForce; //variable utilisé pour le knockback
    private float knockBackCounter;

    public float bounceForce; //le rebond après avoir tué un Enemy

    public bool stopInput; // variable qui aide pour la fin de niveau pour déactiver les mouvements de Player

    private void Awake()
    {
        instance = this; //permet d'avoir accès depuis les autres scripts
    }

    void Start()
    {
        anim = GetComponent<Animator>(); //cherche le component "Animator"
        SR = GetComponent<SpriteRenderer>(); //cherche le component "Sprite Renderer" du player
    }


    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput) //si la pause N'EST PAS activé ET les mouvements du Player sont activé ALORS
        {
            if (knockBackCounter <= 0) //déactive les controls pendant le knockBack
            {
                RB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), RB.velocity.y); //déplacement Horizontal //ajouter Raw à GetAxisRaw pour supp le "momento du mouvement"

                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround); //détection du sol

                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded) //vérifications si on touche le sol
                    {
                        RB.velocity = new Vector2(RB.velocity.x, jumpForce);

                        AudioManager.instance.PlaySFX(10); //joue le son 10 de la liste d'audio du manager
                    }
                    else
                    {
                        if (canDoubleJump) //double saut
                        {
                            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
                            canDoubleJump = false;

                            AudioManager.instance.PlaySFX(10); //joue le son 10 de la liste d'audio du manager
                        }
                    }
                }

                if (RB.velocity.x < 0) //permet le flip du "Sprite Renderer" pour l'animation
                {
                    SR.flipX = true;
                }
                else if (RB.velocity.x > 0)
                {
                    SR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime; //compte à rebours qui déactive le déplacements du Player

                if (SR.flipX == false) //aplique la force vers l'opposé du danger ici gauche
                {
                    RB.velocity = new Vector2(-knockBackForce, RB.velocity.y);
                }
                else //ici droite
                {
                    RB.velocity = new Vector2(knockBackForce, RB.velocity.y);
                }
            }
        }

        
        
        anim.SetFloat("moveSpeed", Mathf.Abs(RB.velocity.x)); //Recherche les variable sur l'Animator du Player pour le déplacement
        anim.SetBool("isGrounded", isGrounded); //Recherche les variable sur l'Animator du Player pour le saut

    }

    public void KnockBack() //fonction pour réalisé le knockBack
    {
        knockBackCounter = knockBackLength;
        RB.velocity = new Vector2(0f, knockBackForce);

        anim.SetTrigger("hurt"); //active le trigger anim et lance l'animation de "hurt"
    }

    public void Bounce() //rebond après avoir tué un enemy
    {
        RB.velocity = new Vector2(RB.velocity.x, bounceForce);

        AudioManager.instance.PlaySFX(10); //joue le son 10 de la liste d'audio du manager
    }
}
