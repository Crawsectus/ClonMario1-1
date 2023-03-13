using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntosFlotantes : MonoBehaviour
{
    public TextMeshProUGUI textoPuntos;
    public float duracion = 1f;
    public float velocidadMovimiento = 1f;
    private Vector3 posicionInicial;

    private void Start()
    {
        posicionInicial = transform.position;
    }

    public void MostrarPuntos(int puntos)
    {
        textoPuntos.text = puntos.ToString();
        StartCoroutine(AnimarPuntos());
    }

    IEnumerator AnimarPuntos()
    {
        float tiempoPasado = 0f;
        while (tiempoPasado < duracion)
        {
            transform.position += Vector3.up * velocidadMovimiento * Time.deltaTime;
            tiempoPasado += Time.deltaTime;
            yield return null;
        }
        transform.position = posicionInicial;
        gameObject.SetActive(false);
    }
}

