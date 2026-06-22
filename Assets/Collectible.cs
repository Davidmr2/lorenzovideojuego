using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instancia != null)
            {
                GameManager.instancia.SumarPunto();
            }

            Destroy(gameObject);
        }
    }
}