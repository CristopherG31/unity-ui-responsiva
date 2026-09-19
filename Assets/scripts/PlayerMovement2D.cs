using UnityEngine;

// Controla el movimiento del personaje, su animación de caminar/idle
// en 4 direcciones, y detecta colisiones (cambia de color al chocar).
// Requiere: SpriteRenderer, Rigidbody2D (Gravity Scale 0, Freeze Rotation Z),
// y un Collider2D ajustado al tamaño del sprite.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    [Header("Animación - frames de caminar (arrastra los sprites recortados)")]
    public Sprite[] caminarAbajo;
    public Sprite[] caminarArriba;
    public Sprite[] caminarIzquierda;
    public Sprite[] caminarDerecha;

    [Header("Animación - frame de reposo (idle) por dirección")]
    public Sprite idleAbajo;
    public Sprite idleArriba;
    public Sprite idleIzquierda;
    public Sprite idleDerecha;

    [Header("Velocidad de animación")]
    public float tiempoPorFrame = 0.12f;

    [Header("Cambio de estado al chocar")]
    public Color colorNormal = Color.white;
    public Color colorAlChocar = Color.red;
    public float duracionFlash = 0.15f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float temporizadorFlash = 0f;
    private float temporizadorFrame = 0f;
    private int indiceFrame = 0;

    // 0 = abajo, 1 = arriba, 2 = izquierda, 3 = derecha
    private int direccionActual = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = colorNormal;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 direccion = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = direccion * velocidad;

        bool seEstaMoviendo = direccion.sqrMagnitude > 0.01f;

        if (seEstaMoviendo)
        {
            // Determina la dirección dominante (prioriza el eje con mayor movimiento)
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
            {
                direccionActual = moveX > 0 ? 3 : 2; // derecha : izquierda
            }
            else
            {
                direccionActual = moveY > 0 ? 1 : 0; // arriba : abajo
            }

            AnimarCaminata();
        }
        else
        {
            MostrarIdle();
        }

        // Restaurar el color normal después del "flash" de colisión
        if (temporizadorFlash > 0f)
        {
            temporizadorFlash -= Time.deltaTime;
            if (temporizadorFlash <= 0f)
            {
                sr.color = colorNormal;
            }
        }
    }

    private void AnimarCaminata()
    {
        Sprite[] framesActuales = ObtenerFramesDireccionActual();
        if (framesActuales == null || framesActuales.Length == 0) return;

        temporizadorFrame += Time.deltaTime;
        if (temporizadorFrame >= tiempoPorFrame)
        {
            temporizadorFrame = 0f;
            indiceFrame = (indiceFrame + 1) % framesActuales.Length;
            sr.sprite = framesActuales[indiceFrame];
        }
    }

    private void MostrarIdle()
    {
        indiceFrame = 0;
        temporizadorFrame = 0f;

        switch (direccionActual)
        {
            case 0: sr.sprite = idleAbajo; break;
            case 1: sr.sprite = idleArriba; break;
            case 2: sr.sprite = idleIzquierda; break;
            case 3: sr.sprite = idleDerecha; break;
        }
    }

    private Sprite[] ObtenerFramesDireccionActual()
    {
        switch (direccionActual)
        {
            case 0: return caminarAbajo;
            case 1: return caminarArriba;
            case 2: return caminarIzquierda;
            case 3: return caminarDerecha;
            default: return null;
        }
    }

    // Se llama automáticamente cuando este objeto choca con otro Collider2D
    // (no trigger) que tenga Rigidbody2D o sea estático.
    void OnCollisionEnter2D(Collision2D colision)
    {
        sr.color = colorAlChocar;
        temporizadorFlash = duracionFlash;

        Debug.Log("Colisión detectada con: " + colision.gameObject.name);
    }
}
