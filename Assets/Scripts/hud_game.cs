using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hud_game : MonoBehaviour
{
    public TextMeshProUGUI monedas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int cantidadMonedas = PlayerPrefs.GetInt("monedas");
        monedas.text = cantidadMonedas.ToString();
    }
}
