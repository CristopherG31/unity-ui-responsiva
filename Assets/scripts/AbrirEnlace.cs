using UnityEngine;

public class AbrirEnlace : MonoBehaviour
{
    public string url;
    public void Abrir() => Application.OpenURL(url);
}