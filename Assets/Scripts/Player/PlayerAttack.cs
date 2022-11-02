using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float atackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private Movement playerMovement;
    private float cooldownTimer = Mathf.Infinity;



    private void Awake() 
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Movement>();
    }



    private void Update() 
    {
        if(Input.GetMouseButton(0) && cooldownTimer > atackCooldown && playerMovement.CanAttack())
          Attack();
        
        cooldownTimer += Time.deltaTime; 
        
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if(fireballs[i].activeInHierarchy)
                return i;
        }
        
        
        return 0;
    }










}
