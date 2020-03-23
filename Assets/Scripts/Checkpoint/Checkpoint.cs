using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer SR;
    public Sprite checkOn, checkOff;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) // TO DO ajouter une lumière sur les le sprite de checkpoint
    {
        if (other.CompareTag("Player"))
        {
            CheckpointController.instance.DeactivateCheckpoints();

            SR.sprite = checkOn; //change le visiuel du sprite

            CheckpointController.instance.SetSpawnPoint(transform.position); //active la postion pour le spawn du player
        }
    }
    public void ResetCheckpoint() //va permettre de mettre le sprite par default TO DO deactivé la lumière 
    {
        SR.sprite = checkOff;
    }
}
