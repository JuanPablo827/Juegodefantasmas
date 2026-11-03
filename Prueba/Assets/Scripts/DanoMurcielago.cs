using UnityEngine;

public class DanoMurcielago : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Chocaste con los murciélagos!");

            // Llamamos a la función del GameManager para mostrar el panel de récord
            if (GameManager.instance != null)
            {
                GameManager.instance.PerderJuego();
            }
        }
    }
}