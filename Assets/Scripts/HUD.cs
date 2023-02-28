using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI vidas;
    // Start is called before the first frame update
    void Start()
    {
        int cantidadVidas = PlayerPrefs.GetInt("vidas");
        vidas.text = cantidadVidas.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
