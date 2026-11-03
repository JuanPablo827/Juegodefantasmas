using UnityEngine;

public class SalidaGatillo : MonoBehaviour
{
    public Bloque bloquePadre;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Choco");

        bloquePadre.destruirBloque();
    }
}
