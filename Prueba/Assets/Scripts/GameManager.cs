using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Lista de Bloques")]
    public Bloque[] modelosBloques;
    public Bloque ultimoBloque;

    [Header("Referencias de UI")]
    public GameObject panelMenu;
    public TextMeshProUGUI textoContador;   // <--- Actualizado a TextMeshPro

    [HideInInspector]
    public bool juegoIniciado = false;

    public void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        juegoIniciado = false;
        if (panelMenu != null) panelMenu.SetActive(true);
        if (textoContador != null) textoContador.gameObject.SetActive(false);
    }

    public void BotonJugarPresionado()
    {
        if (panelMenu != null) panelMenu.SetActive(false);
        StartCoroutine(RutinaCuentaRegresiva());
    }

    IEnumerator RutinaCuentaRegresiva()
    {
        if (textoContador != null) textoContador.gameObject.SetActive(true);

        if (textoContador != null) textoContador.text = "3";
        yield return new WaitForSeconds(1f);

        if (textoContador != null) textoContador.text = "2";
        yield return new WaitForSeconds(1f);

        if (textoContador != null) textoContador.text = "1";
        yield return new WaitForSeconds(1f);

        if (textoContador != null) textoContador.text = "¡YA!";
        yield return new WaitForSeconds(0.5f);

        if (textoContador != null) textoContador.gameObject.SetActive(false);

        juegoIniciado = true;
        CrearVariosBloques();
    }

    void CrearVariosBloques()
    {
        for (int i = 0; i < 10; i++)
        {
            CrearNuevoBloque();
        }
    }

    public void BloqueMurio()
    {
        if (juegoIniciado)
        {
            CrearNuevoBloque();
        }
    }

    void CrearNuevoBloque()
    {
        int randomIndex = Random.Range(0, modelosBloques.Length);
        Bloque bloqueSeleccionado = modelosBloques[randomIndex];

        Bloque nuevoBloque = Instantiate(bloqueSeleccionado);

        Vector3 diferentePosicion = nuevoBloque.transform.position - nuevoBloque.inicioBloque.transform.position;
        nuevoBloque.transform.position = ultimoBloque.finalBloque.position + diferentePosicion;

        ultimoBloque = nuevoBloque;
    }
}