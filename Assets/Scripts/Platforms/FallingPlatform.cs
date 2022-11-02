using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    
    private Rigidbody2D rb;
    [SerializeField] float time;
    [SerializeField] float falltime;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


     void OnCollisionEnter2D(Collision2D coll) 
    {
       if(coll.gameObject.name.Equals("Player"))
        {
            Invoke("Fallplatform", time);
            Destroy(gameObject, falltime);
        } 
    }



    void FallPlatform()
    {
        rb.isKinematic = false;
    }


   
}
