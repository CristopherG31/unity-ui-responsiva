using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles del menú")]
    public GameObject panelOpciones;
    public GameObject panelAyuda;

    [Header("Escena de juego")]
    public string nombreEscenaJuego = "Jugar";

    // Botón "Jugar": cambia por completo a la escena del juego
    public void PlayGame()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    // Botón "Opciones": muestra el panel de opciones encima del menú
    public void OpenOptions()
    {
        panelOpciones.SetActive(true);
    }

    // Botón "Cerrar" dentro del panel de Opciones
    public void CloseOptions()
    {
        panelOpciones.SetActive(false);
    }

    // Botón "Ayuda": muestra el panel de ayuda encima del menú
    public void OpenHelp()
    {
        panelAyuda.SetActive(true);
    }

    // Botón "Cerrar" dentro del panel de Ayuda
    public void CloseHelp()
    {
        panelAyuda.SetActive(false);
    }

    // Botón "Volver al Menú" (usado dentro de la escena de juego)
    public void BackToMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
}