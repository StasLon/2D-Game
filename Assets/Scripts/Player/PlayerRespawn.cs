using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;



    private void Awake() 
    {
        playerHealth = GetComponent<Health>();  
        uiManager = FindObjectOfType<UIManager>();
    }



    public void CheckRespawn()
    {
        if(currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }


        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
    }


    private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.transform.tag == "Checkpoint")
        {
            currentCheckpoint = coll.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            coll.GetComponent<Collider2D>().enabled = false;
            coll.GetComponent<Animator>().SetTrigger("appear"); 
        }
    }











}
