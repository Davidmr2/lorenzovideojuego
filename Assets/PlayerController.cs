using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 8f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool enSuelo = false;
    private Vector3 escalaOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        float movimiento = Input.GetAxis("Horizontal");

        // MOVIMIENTO
        transform.Translate(Vector2.right * movimiento * velocidad * Time.deltaTime);

        // ANIMACIÓN WALK / IDLE
        anim.SetFloat("Speed", Mathf.Abs(movimiento));

        // GIRAR PERSONAJE
        if (movimiento > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(escalaOriginal.x),
                escalaOriginal.y,
                escalaOriginal.z
            );
        }
        else if (movimiento < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(escalaOriginal.x),
                escalaOriginal.y,
                escalaOriginal.z
            );
        }

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            anim.SetTrigger("Jump");

            enSuelo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enSuelo = true; // 🔥 cualquier contacto = puede saltar

        // Si toca un enemigo, reinicia la escena
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enSuelo = false;
    }
}