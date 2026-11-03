using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Lista de Bloques")]
    public Bloque[] modelosBloques;
    public Bloque ultimoBloque;

    [Header("Referencias de UI")]
    public GameObject panelMenu;
    public TextMeshProUGUI textoContador;
    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoRecordMenu;

    [Header("Panel de Récord y Guardado")]
    public GameObject panelRecord;
    public TMP_InputField inputNombreJugador;
    public GameObject botonGuardar;
    public TextMeshProUGUI textoPuntuacionFinal;

    private int dulcesColectados = 0;
    private int recordPartidaActual = 0;

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
        if (textoPuntuacion != null) textoPuntuacion.gameObject.SetActive(false);
        if (panelRecord != null) panelRecord.SetActive(false);

        ActualizarTextoRecordMenu();
    }

    public void BotonJugarPresionado()
    {
        if (panelMenu != null) panelMenu.SetActive(false);
        StartCoroutine(RutinaCuentaRegresiva());
    }

    IEnumerator RutinaCuentaRegresiva()
    {
        if (textoContador != null) textoContador.gameObject.SetActive(true);
        if (textoContador != null) textoContador.text = "3"; yield return new WaitForSeconds(1f);
        if (textoContador != null) textoContador.text = "2"; yield return new WaitForSeconds(1f);
        if (textoContador != null) textoContador.text = "1"; yield return new WaitForSeconds(1f);
        if (textoContador != null) textoContador.text = "¡YA!"; yield return new WaitForSeconds(0.5f);
        if (textoContador != null) textoContador.gameObject.SetActive(false);
        if (textoPuntuacion != null) textoPuntuacion.gameObject.SetActive(true);

        juegoIniciado = true;
        CrearVariosBloques();
    }

    public void SumarDulce()
    {
        dulcesColectados++;
        ActualizarInterfazPuntos();
    }

    void ActualizarInterfazPuntos()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = dulcesColectados.ToString();
            textoPuntuacion.ForceMeshUpdate();
        }
    }

    // Al perder: muestra el panel con la barra para escribir el nombre
    public void PerderJuego()
    {
        juegoIniciado = false;
        recordPartidaActual = dulcesColectados;

        if (textoPuntuacionFinal != null)
        {
            textoPuntuacionFinal.text = "¡Perdiste!\nCalabazas conseguidas: " + recordPartidaActual;
        }

        if (inputNombreJugador != null) inputNombreJugador.gameObject.SetActive(true);
        if (botonGuardar != null) botonGuardar.SetActive(true);

        if (panelRecord != null)
        {
            panelRecord.SetActive(true);
        }
    }

    // Botón Guardar: Acumula y guarda la partida actual en el historial de PlayerPrefs
    public void BotonGuardarPuntuacion()
    {
        string nombre = "";
        if (inputNombreJugador != null)
        {
            nombre = inputNombreJugador.text.Trim();
        }

        if (string.IsNullOrEmpty(nombre))
        {
            Debug.Log("No se ingresó nombre. Progreso descartado.");
        }
        else
        {
            // Recuperamos el historial que ya estaba guardado (o una cadena vacía si es la primera vez)
            string historialActual = PlayerPrefs.GetString("HistorialPartidas", "");

            // Creamos la nueva línea con el formato: "Nombre - X calabazas"
            string nuevaEntrada = nombre + " - " + recordPartidaActual + " 🎃\n";

            // Unimos el historial viejo con la nueva entrada
            string historialNuevo = nuevaEntrada + historialActual;

            // Guardamos el historial acumulado
            PlayerPrefs.SetString("HistorialPartidas", historialNuevo);
            PlayerPrefs.Save();

            Debug.Log("Progreso guardado para: " + nombre);
        }

        if (panelRecord != null) panelRecord.SetActive(false);
        ActualizarTextoRecordMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método para el botón de la Copa (Abre/Cierra el panel y muestra el historial)
    public void BotonCopasPresionado()
    {
        if (panelRecord != null)
        {
            bool estadoActual = panelRecord.activeSelf;

            if (!estadoActual)
            {
                if (inputNombreJugador != null) inputNombreJugador.gameObject.SetActive(false);
                if (botonGuardar != null) botonGuardar.SetActive(false);

                // Cargamos todo el historial acumulado de partidas
                string historialGuardado = PlayerPrefs.GetString("HistorialPartidas", "Aún no hay registros guardados.");

                if (textoPuntuacionFinal != null)
                {
                    textoPuntuacionFinal.text = "Historial de Jugadores:\n\n" + historialGuardado;
                }

                panelRecord.SetActive(true);
            }
            else
            {
                panelRecord.SetActive(false);
            }
        }
    }

    // Método para el botón "X" o "Salir" del panel (Reinicia la escena)
    public void BotonSalirPresionado()
    {
        if (panelRecord != null)
        {
            panelRecord.SetActive(false);
        }

        // Recargamos la escena para volver al inicio limpiamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    void ActualizarTextoRecordMenu()
    {
        if (textoRecordMenu != null)
        {
            string historial = PlayerPrefs.GetString("HistorialPartidas", "Sin registros");
            // Tomamos solo la primera línea o un mensaje general para el menú principal
            textoRecordMenu.text = "Historial guardado disponible";
        }
    }

    void CrearVariosBloques()
    {
        for (int i = 0; i < 10; i++) { CrearNuevoBloque(); }
    }

    public void BloqueMurio()
    {
        if (juegoIniciado) { CrearNuevoBloque(); }
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