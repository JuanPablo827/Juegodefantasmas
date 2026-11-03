using UnityEngine;
using UnityEngine.Rendering;

public class MiCamara : MonoBehaviour
{
    [Header("Referencias")]
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -8f);

    [Header("Configuarion de movimiento")]
    public float suavidaddesalto = 5f;

    [Header("Configuraciones de la vista previa")]
    [Tooltip("Cuántos grados girará la cámara hacia un lado. Valor recomendado: 3 a 6")]
    public float anguloMaximodeVisión = 4f;
    public float suavisadodeRotacion = 5f;

    // Guarda la inclinación X original (cuando mira hacia abajo) para no perderla al rotar.
    private float _inclinacionXBase;

    void Start()
    {
        //Memoriza la rotación que dejaste en el Inspector justo al comenzar el juego.
        _inclinacionXBase = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (target == null)
            return;
        // 1. Posicion (Movimiento)
        float objetoY = target.position.y + offset.y;
        float ySuviazado = Mathf.Lerp(transform.position.y, objetoY, suavidaddesalto * Time.deltaTime);

        transform.position = new Vector3(
            offset.x,
            ySuviazado,
            target.position.z + offset.z);

        // 2. Rotacion (El "vistazo")

        //Sabemos que sus pistas extremas son -4 y 4.
        float proporcionLateral = target.position.x / 4f;
        float objetivodeRotacionY = proporcionLateral * anguloMaximodeVisión;
        Quaternion rotacionObjeto = Quaternion.Euler(_inclinacionXBase,objetivodeRotacionY,0f);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotacionObjeto,suavisadodeRotacion*Time.deltaTime);
    }

}
