using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hud_game : MonoBehaviour
{
    public TextMeshProUGUI monedas;
    public TextMeshProUGUI puntos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int cantidadMonedas = PlayerPrefs.GetInt("monedas");
        monedas.text = cantidadMonedas.ToString();
        int cantidadPuntos = PlayerPrefs.GetInt("puntos");
        string puntosFormateados = cantidadPuntos.ToString("D6");
        puntos.text = puntosFormateados;
    }
}
