using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] private float speed;
    private float direction;
    private float lifetime;

    private bool hit;


    private BoxCollider2D boxcollider;
    private Animator anim;


    private void Awake() 
    {
        boxcollider = GetComponent<BoxCollider2D>();  
        anim = GetComponent<Animator>();    
    }

    private void Update() 
    {
        if(hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D coll) 
    {
        hit = true;
        boxcollider.enabled = false;
        anim.SetTrigger("explode");

        if(coll.tag == "Enemy")
            coll.GetComponent<Health>().TakeDamage(1);
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxcollider.enabled = true;

        float LocalscaleX = transform.localScale.x;
        if(Mathf.Sign(LocalscaleX) != _direction)
            LocalscaleX = -LocalscaleX;


        transform.localScale = new Vector3(LocalscaleX, transform.localScale.y, transform.localScale.z);    
    }



    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

















}
