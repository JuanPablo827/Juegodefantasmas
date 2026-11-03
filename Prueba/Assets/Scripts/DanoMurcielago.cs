using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class DanoMurcielago : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Chocaste con los murciélagos!");

            // Opción 1: Reiniciar el nivel actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Opción 2: Si tienes un GameManager, puedes llamar a una función de muerte:
            // GameManager.instance.JugadorMurio();
        }
    }
}