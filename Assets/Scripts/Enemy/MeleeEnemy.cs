using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    
[SerializeField] private float attackCooldown;
[SerializeField] private float range;
[SerializeField] private float colliderDistance;
[SerializeField] private int damage;
[SerializeField] private BoxCollider2D boxcoll;
[SerializeField] private LayerMask playerLayer;
[SerializeField] private AudioClip swordSound;
private float cooldownTimer = Mathf.Infinity;
private Animator anim;
private Health playerHealth;
private EnemyPatrol enemyPatrol;


private void Awake() 
{
    anim = GetComponent<Animator>();
    enemyPatrol = GetComponentInParent<EnemyPatrol>();
}


private void Update() 
{
    cooldownTimer += Time.deltaTime;

    if(PlayerInSight())
    {
        if(cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
        {
            cooldownTimer = 0;
            anim.SetTrigger("meleeAttack");
            SoundManager.instance.PlaySound(swordSound);

        }

    }

    if(enemyPatrol != null)
        enemyPatrol.enabled = !PlayerInSight();
}
    


private bool PlayerInSight()
{
    RaycastHit2D hit = Physics2D.BoxCast(boxcoll.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
    new Vector3(boxcoll.bounds.size.x * range, boxcoll.bounds.size.y, boxcoll.bounds.size.z),
     0, Vector2.left, 0, playerLayer);

    if(hit.collider != null)
        playerHealth = hit.transform.GetComponent<Health>();


    return hit.collider != null;
}
private void OnDrawGizmos() 
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(boxcoll.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
    new Vector3(boxcoll.bounds.size.x * range, boxcoll.bounds.size.y, boxcoll.bounds.size.z));    
}



private void DamagePlayer()
{
    if(PlayerInSight())
    {
        playerHealth.TakeDamage(damage);
    }
}



}
