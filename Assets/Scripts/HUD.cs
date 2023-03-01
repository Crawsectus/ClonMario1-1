using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI vidas;
    public TextMeshProUGUI monedas;
    // Start is called before the first frame update
    void Start()
    {
        int cantidadVidas = PlayerPrefs.GetInt("vidas");
        vidas.text = cantidadVidas.ToString();
        int cantidadMonedas = PlayerPrefs.GetInt("monedas");
        monedas.text = cantidadMonedas.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
