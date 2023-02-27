using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaAutomatica : MonoBehaviour
{
    public float tiempoEspera = 3.0f; // Tiempo de espera antes de cargar la siguiente escena
    public string siguienteEscena = "1-1"; // Nombre de la siguiente escena

    void Start()
    {
        // Espera el tiempo especificado antes de cargar la siguiente escena
        StartCoroutine(CargarEscenaDespuesDeEspera(tiempoEspera));
    }

    IEnumerator CargarEscenaDespuesDeEspera(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        SceneManager.LoadScene(siguienteEscena);
    }
}
