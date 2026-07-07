using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy2Controller : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2f;
    public float leftLimit = -3f;
    public float rightLimit = 3f;


    [Header("Persecución")]
    public Transform player;
    public float detectionRange = 3f;
    public float chaseSpeed = 1.5f;
    public float samePlatformHeight = 0.35f;


    [Header("Ataque")]
    public float minAttackDelay = 4f;
    public float maxAttackDelay = 7f;
    public float attackDuration = 1f;
    public float attackRange = 1.5f;


    private bool movingRight = true;
    private bool attacking = false;
    private bool canAttack = true;


    private Animator animator;
    private SpriteRenderer spriteRenderer;



    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(AttackCycle());
    }



    void Update()
    {
        
        if(attacking)
            return;


        if(player != null)
        {
            float distance = Vector2.Distance(
                transform.position,
                player.position
            );


            float height = Mathf.Abs(
                transform.position.y - player.position.y
            );


            
            if(distance <= detectionRange && height <= samePlatformHeight)
            {
                FollowPlayer();
            }
            else
            {
                Patrol();
            }

        }
        else
        {
            Patrol();
        }
    }




    void Patrol()
    {
        if(movingRight)
        {
            transform.Translate(
                Vector2.right * speed * Time.deltaTime
            );


            spriteRenderer.flipX = false;


            if(transform.position.x >= rightLimit)
            {
                movingRight = false;
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            transform.Translate(
                Vector2.left * speed * Time.deltaTime
            );


            spriteRenderer.flipX = true;


            if(transform.position.x <= leftLimit)
            {
                movingRight = true;
                spriteRenderer.flipX = false;
            }
        }
    }





    void FollowPlayer()
    {
        
        if(player.position.x > transform.position.x)
        {
            transform.Translate(
                Vector2.right * chaseSpeed * Time.deltaTime
            );

            spriteRenderer.flipX = false;
        }
        else
        {
            transform.Translate(
                Vector2.left * chaseSpeed * Time.deltaTime
            );

            spriteRenderer.flipX = true;
        }


        
        float x = Mathf.Clamp(
            transform.position.x,
            leftLimit,
            rightLimit
        );


        transform.position = new Vector3(
            x,
            transform.position.y,
            transform.position.z
        );
    }





    IEnumerator AttackCycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);


            if(player != null && canAttack)
            {
                float distance = Vector2.Distance(
                    transform.position,
                    player.position
                );


                float height = Mathf.Abs(
                    transform.position.y - player.position.y
                );


              
                if(distance <= attackRange && height <= samePlatformHeight)
                {
                    canAttack = false;
                    attacking = true;


                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Attack");


                    yield return new WaitForSeconds(
                        attackDuration
                    );


                    attacking = false;


                    yield return new WaitForSeconds(1f);


                    canAttack = true;
                }
            }
        }
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}