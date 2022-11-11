using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetLvl7 : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.gameObject.tag == "Player")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(7);
            }
    }







}
