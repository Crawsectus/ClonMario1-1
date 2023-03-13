using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI vidas;
    public TextMeshProUGUI monedas;
    public TextMeshProUGUI puntos;
    // Start is called before the first frame update
    void Start()
    {
        int cantidadVidas = PlayerPrefs.GetInt("vidas");
        vidas.text = cantidadVidas.ToString();
        int cantidadMonedas = PlayerPrefs.GetInt("monedas");
        monedas.text = cantidadMonedas.ToString();
        int cantidadPuntos = PlayerPrefs.GetInt("puntos");
        string puntosFormateados = cantidadPuntos.ToString("D6");
        puntos.text = puntosFormateados;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
