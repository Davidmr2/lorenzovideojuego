using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float distancia = 3f;

    private bool moviendoDerecha = false;
    private bool contarDistancia = false;

    private Vector3 puntoInicio;

    void Update()
    {
        if (moviendoDerecha)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (contarDistancia)
            {
                if (Vector2.Distance(transform.position, puntoInicio) >= distancia)
                {
                    moviendoDerecha = false;
                    contarDistancia = false;

                    Vector3 escala = transform.localScale;
                    escala.x *= -1;
                    transform.localScale = escala;
                }
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            puntoInicio = transform.position;

            moviendoDerecha = true;
            contarDistancia = true;

            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}