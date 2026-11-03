using UnityEngine;

public class EntradaBloque : MonoBehaviour
{
    [Header("Referencia a los murciélagos de este bloque")]
    public MovimientoMurcielagosUnaVez murcielagosDelBloque;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el niño entra a este bloque, ordenamos que solo estos murciélagos vuelen*
            if (murcielagosDelBloque != null)
            {
                murcielagosDelBloque.ActivarVuelo();
            }
        }
    }
}