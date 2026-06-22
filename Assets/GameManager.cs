using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public TextMeshProUGUI textoContador;
    private int puntos = 0;

    void Awake()
    {
        instancia = this;
    }

    public void SumarPunto()
    {
        puntos++;
        textoContador.text = puntos.ToString();
    }
}
