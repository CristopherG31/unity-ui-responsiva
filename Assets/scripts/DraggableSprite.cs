using UnityEngine;
using UnityEngine.EventSystems;

// Permite arrastrar una Image (sprite) dentro del Canvas con el mouse.
// Requiere que el objeto tenga un componente Image con "Raycast Target" activado.
public class DraggableSprite : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Movemos el sprite según el desplazamiento del mouse,
        // dividido por la escala del Canvas para que funcione bien
        // sin importar la resolución (gracias al Canvas Scaler).
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}