using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private float[] calles = { -4, 0, 4 };//0 < 1 > 2
    private int miCalle = 1;//Empieza por la mitad
    
    private float velocidadY = 0;
    private float gravedad = -25.0f;
    private float velocidadFrontal = 1.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Si el juego no ha iniciado mediante el menú y la cuenta regresiva, no se mueve
        if (GameManager.instance != null && !GameManager.instance.juegoIniciado)
            return;

        verificarCalles();
        moverPersonaje();
        Saltar();

        if (velocidadFrontal < 30.0f)
        {
            velocidadFrontal += (0.5f * Time.deltaTime);
        }

    }


    void Saltar() 
    {
        //envia la orden de saltar al presionar el boton 
        if (Input.GetKeyDown(KeyCode.UpArrow) && characterController.isGrounded) 
        {
            velocidadY = 15.0f;
        }

        //Verifica si esta en el suelo y cayendo y mantine una fuerza ligera para que no de saltitos 
        if(characterController.isGrounded && velocidadY < 0)
        {
            velocidadY = -2.0f;
        }
        //aplicarle siempre una gravedad 
        else
        {
            velocidadY = velocidadY + (gravedad * Time.deltaTime);
        }
    }



    void verificarCalles()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))//<-
        {
            if (miCalle > 0)//(1)(2)
            {
                miCalle -= 1;
                //transform.position = new Vector3(calles[miCalle], 1.2f, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))//->
        {
            if (miCalle < 2)//(0)(1)
            {
                miCalle += 1;
                //transform.position = new Vector3(calles[miCalle], 1.2f, 0);
            }
        }

    }



    void moverPersonaje()
    {
        //Movimiento Horizontal - Izquierda y Derecha 
        float paraDondeIr = calles[miCalle];
        float distancia = paraDondeIr - transform.position.x;

        float velocidadX = 5.0f;
        Vector3 movimientoLateral = transform.right * (distancia * velocidadX);

        //Movimiento Vertical - Salto y Gravedad
        Vector3 movimientoVertical = transform.up * velocidadY;

        //Movimeinto Frontal
        Vector3 movimientoFrontal = transform.forward * velocidadFrontal;

        //Mover sumando todos los vectores
        characterController.Move((movimientoLateral + movimientoVertical + movimientoFrontal) * Time.deltaTime);
    }
}
