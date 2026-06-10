using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 8f;

    private Rigidbody2D rb;
    private bool enSuelo = true;

    private Vector3 escalaOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Guarda la escala original del personaje
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        float movimiento = Input.GetAxis("Horizontal");

        // Movimiento izquierda y derecha
        transform.Translate(
            Vector2.right *
            movimiento *
            velocidad *
            Time.deltaTime
        );

        // Girar personaje sin cambiar tamaño
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

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                fuerzaSalto
            );

            enSuelo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Suelo")
        {
            enSuelo = true;
        }
    }
}

