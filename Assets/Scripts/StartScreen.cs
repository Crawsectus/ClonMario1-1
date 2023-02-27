using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreen : MonoBehaviour
{
    public string gameSceneName; // El nombre de la escena del juego

    void Start()
    {
        // Ocultamos el cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // Si se oprime la tecla Enter o Return, iniciamos el juego
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        // Cargamos la escena del juego
        SceneManager.LoadScene(gameSceneName);
    }
}
