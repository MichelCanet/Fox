using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance; //permet d'avoir accès depuis les autres scripts

    private Checkpoint[] checkpoints; //tableau qui regroupe les checkpoints du niveau

    public Vector3 spawnPoint; //une position dans le niveau

    private void Awake()
    {
        instance = this; //permet d'avoir accès depuis les autres scripts
    }
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>(); //va cherhcer tout les objets de type Checkpoints dans la scene du niveau par ordre

        spawnPoint = PlayerController.instance.transform.position; // attribution du premier checkpoint à la potition inicial du joueur dans le niveau
    }
    void Update()
    {
        
    }
    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
