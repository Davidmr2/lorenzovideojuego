using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy2Controller : MonoBehaviour
{
    public float speed = 2f;
    private bool movingRight = true;

    public float leftLimit = -3f;
    public float rightLimit = 3f;

    private Animator animator;
    private bool attacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Si está atacando, deja de caminar
        if (attacking)
            return;

        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attacking = true;

            // Reproduce la animación Attack
            animator.SetTrigger("Attack");

            // Reinicia la escena después de 1 segundo
            StartCoroutine(RestartScene());
        }
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}