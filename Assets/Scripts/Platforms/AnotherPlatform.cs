using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlatform : MonoBehaviour
{
    
    private Rigidbody2D rb;
    Vector2 currentPosition;
    bool movingBack;
    [SerializeField] float time;
    [SerializeField] float backTime;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPosition = transform.position;
    }


    private void OnCollisionEnter2D(Collision2D coll) 
    {
       if(coll.gameObject.name.Equals("Player") && movingBack == false)
        {
            Invoke("FallPlatform", time);
            
        } 
    }

    void FallPlatform()
    {
        rb.isKinematic = false;
        Invoke("BackPlatform", backTime);
    }

    
    void BackPlatform()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        movingBack = true;
    }

 void Update() 
{
   if(movingBack == true)
   {
    transform.position = Vector2.MoveTowards(transform.position, currentPosition, 10f * Time.deltaTime);
   } 

   if(transform.position.y == currentPosition.y)
   {
    movingBack = false;
   }

}
   




}
