using UnityEngine;
using System.Collections;
 
public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
 
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private AudioClip fireSound;
    private Animator anim;
    private SpriteRenderer spriteRend;
 
    private bool triggered; 
    private bool active; 
 
    private Health player;
 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());
 
            player = collision.GetComponent<Health>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }
 
    private void Update()
    {
        if (active && player != null)
            player.TakeDamage(damage);
            player = null;
    }
 
    private IEnumerator ActivateFiretrap()
    {
        
        triggered = true;
        spriteRend.color = Color.red; 
 
        
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(fireSound);
        spriteRend.color = Color.white; 
        active = true;
        anim.SetBool("activated", true);   
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}













