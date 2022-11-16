using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public ShowEndPanel endPanel;
     private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.gameObject.tag == "Player")
            {
                endPanel.CompleteLvl();
                MoneyText.instance.TextChange(coll.GetComponent<Movement>().money.ToString());
                
            }
    }







}
