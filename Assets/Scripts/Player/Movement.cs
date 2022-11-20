using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Layers")]
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private LayerMask wallLayer;

   [Header("Movement Parameters")]
   [SerializeField] private float speed;
   [SerializeField] private float jumpPower;

   [Header("Sounds")]
   [SerializeField] private AudioClip jumpSound;
   [SerializeField] private AudioClip powerUpSound;
   [SerializeField] private AudioClip moneySound;
   [SerializeField] private AudioClip speedSound;

   [Header("Coyote Time")]
   [SerializeField] private float coyoteTime;
   private float coyoteCounter; 

   [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpX; 
    [SerializeField] private float wallJumpY;

   private BoxCollider2D boxcoll;
   private Rigidbody2D rb;
   private Animator anim;

   [Header("Money")]
   [SerializeField] public int money = 0;
   [SerializeField] public Text moneyText;

   [Header("Wall Jump")]
   private float wallJumpcooldown;
   private float horizontalInput;
   

   private void Awake() 
   {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcoll = GetComponent<BoxCollider2D>();
        PlayerPrefs.Save();
   }




    private void Update() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
           

        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;

        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    


        
        

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());


       
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();


        //контролюємий стрибок

        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);

        if(OnWall())
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
        else
        {
             rb.gravityScale = 5;
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            if(IsGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
                coyoteCounter -= Time.deltaTime;

            
        }

    }


    private void Jump()
    {
        if(coyoteCounter < 0 && !OnWall() && jumpCounter <= 0) return;
        SoundManager.instance.PlaySound(jumpSound);


       if(OnWall())
            wallJump();
        
        else
        {
            if(IsGrounded())
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            else
            {
                if(coyoteCounter > 0)
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                
                else
                {
                    if(jumpCounter > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
  
            }

            coyoteCounter = 0;
        }
        
    }

    private void wallJump()
    {
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpcooldown = 0;
    }



    
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcoll.bounds.center, boxcoll.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcoll.bounds.center, boxcoll.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }



    public bool CanAttack()
    {
      return horizontalInput == 0 && IsGrounded() && !OnWall();
    }



    private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.tag == "PowerUp")
            {
                speed += 5;
                SoundManager.instance.PlaySound(speedSound);
                Destroy(coll.gameObject);
                StartCoroutine(ResetPower());

            }
        if(coll.tag == "Money")
            {
                money += 1;   
                SoundManager.instance.PlaySound(moneySound);         
                Destroy(coll.gameObject);
                moneyText.text = money.ToString();
                int totalCoins = PlayerPrefs.GetInt("Money");
                PlayerPrefs.SetInt("Money", totalCoins + money);
            }    
        if(coll.tag == "son")
            {
                anim.SetTrigger("congrats");
                StartCoroutine(Endgame());
            }
    }


    private IEnumerator ResetPower()
        {
            yield return new WaitForSeconds(10);
            speed = 6;
        }

       
    private IEnumerator Endgame()
        {
            
            yield return new WaitForSeconds(5);
            UnityEngine.SceneManagement.SceneManager.LoadScene(12);
            
        }

}
