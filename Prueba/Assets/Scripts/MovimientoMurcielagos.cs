using UnityEngine;

public class MovimientoMurcielagosUnaVez : MonoBehaviour
{
    [Header("Configuración de Cruce Único")]
    public float velocidadBase = 8f;
    public float xInicio = 4f;
    public float xFinal = -4f;

    private bool enMovimiento = false;
    private bool yaCruzo = false;
    private CharacterController playerController;

    void Start()
    {
        transform.localPosition = new Vector3(xInicio, transform.localPosition.y, transform.localPosition.z);

        // Encontramos el componente de movimiento del jugador en la escena sin tocar su código
        playerController = FindObjectOfType<CharacterController>();
    }

    void Update()
    {
        if (!enMovimiento || yaCruzo) return;

        // Calculamos la velocidad actual basándonos en qué tan rápido se mueve el personaje
        float velocidadActual = velocidadBase;
        if (playerController != null)
        {
            // Leemos la velocidad frontal real del CharacterController del jugador
            float velocidadJugador = Mathf.Abs(playerController.velocity.z);

            // Hacemos que los murciélagos escalen su velocidad de forma proporcional
            velocidadActual = velocidadBase + (velocidadJugador * 0.2f);
        }

        // Movemos de derecha a izquierda con la velocidad adaptada
        float nuevoX = Mathf.MoveTowards(transform.localPosition.x, xFinal, velocidadActual * Time.deltaTime);
        transform.localPosition = new Vector3(nuevoX, transform.localPosition.y, transform.localPosition.z);

        if (Mathf.Approximately(transform.localPosition.x, xFinal))
        {
            yaCruzo = true;
        }
    }

    public void ActivarVuelo()
    {
        if (!yaCruzo)
        {
            enMovimiento = true;
        }
    }
}