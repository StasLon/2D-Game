using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
   [SerializeField] private float startingHealth;
   public float currentHealth { get; private set;}
   private Animator anim; 
   private bool dead;
   [SerializeField] private AudioClip deathSound;
   [SerializeField] private AudioClip HurtSound;


   [Header ("iFrames")]
   [SerializeField] private float iFramesDuration;
   [SerializeField] private int numberOfFlashes;
   private SpriteRenderer spriteRend;

   [Header ("Components")]

   [SerializeField] private Behaviour[] components; 


    private void Awake() 
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }


    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if(currentHealth > 0)
        {
            
            
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(HurtSound);
        }
        else
        {
            if(!dead)
            {
                //вимикає все, що померло
                foreach (Behaviour component in components)
                    component.enabled = false;
              
                    
                        anim.SetBool("grounded", true);
                        anim.SetTrigger("die");
                    

                SoundManager.instance.PlaySound(deathSound);
                dead = true;
                

                
            }
            
        }
    }


  public void AddHealth(float _value)
  {
     currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
  }


  private IEnumerator Invunerability()
  {
     Physics2D.IgnoreLayerCollision(7,8, true);

    for (int i = 0; i < numberOfFlashes; i++)
    {
        spriteRend.color = new Color(1, 0, 0, 0.5f);
        yield return new  WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        spriteRend.color = Color.white;
        yield return new  WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
    }
     Physics2D.IgnoreLayerCollision(7,8, false);
  }


 private void Deactivate()
 {
    gameObject.SetActive(false);
 }


 public void Respawn()
 {
    dead = false;
    AddHealth(startingHealth);
       
    anim.ResetTrigger("die");
    anim.Play("idle");
    
    
    StartCoroutine(Invunerability());

    //все вмикає
    foreach (Behaviour component in components)
        component.enabled = true;


 }









  
}
