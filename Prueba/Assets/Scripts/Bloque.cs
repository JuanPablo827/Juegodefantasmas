using UnityEngine;

public class Bloque : MonoBehaviour
{
   public Transform inicioBloque;
   public Transform finalBloque;

    
   public void destruirBloque()
    {
        //avisa al juego para que cree uno mas 
        GameManager.instance.BloqueMurio();
        //Destruir bloque
        Destroy(gameObject, 2.5f);
    }

}
