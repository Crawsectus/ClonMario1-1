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
        int cantidadMonedas = PlayerPrefs.GetInt("monedas");
        monedas.text = cantidadMonedas.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
