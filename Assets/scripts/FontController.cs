using UnityEngine;
using TMPro;

// Este script permite controlar las fuentes (tamaño y tipo) de un texto TMP
// mediante botones. Colócalo en un GameObject y arrastra el texto que quieres
// controlar al campo "Target Text".
public class FontController : MonoBehaviour
{
    [Header("Texto a controlar")]
    public TMP_Text targetText;

    [Header("Fuentes disponibles (opcional)")]
    public TMP_FontAsset[] fuentesDisponibles;
    private int indiceFuenteActual = 0;

    [Header("Configuración de tamaño")]
    public float pasoTamano = 4f;
    public float tamanoMinimo = 12f;
    public float tamanoMaximo = 72f;

    // Botón "A+": aumenta el tamaño de la fuente
    public void AumentarTamano()
    {
        float nuevoTamano = targetText.fontSize + pasoTamano;
        targetText.fontSize = Mathf.Min(nuevoTamano, tamanoMaximo);
    }

    // Botón "A-": disminuye el tamaño de la fuente
    public void DisminuirTamano()
    {
        float nuevoTamano = targetText.fontSize - pasoTamano;
        targetText.fontSize = Mathf.Max(nuevoTamano, tamanoMinimo);
    }

    // Botón "Cambiar fuente": pasa a la siguiente fuente de la lista
    public void CambiarFuente()
    {
        if (fuentesDisponibles == null || fuentesDisponibles.Length == 0) return;

        indiceFuenteActual = (indiceFuenteActual + 1) % fuentesDisponibles.Length;
        targetText.font = fuentesDisponibles[indiceFuenteActual];
    }

    // Botón "Negrita": activa/desactiva negrita
    public void ToggleNegrita()
    {
        if (targetText.fontStyle == FontStyles.Bold)
            targetText.fontStyle = FontStyles.Normal;
        else
            targetText.fontStyle = FontStyles.Bold;
    }
}
