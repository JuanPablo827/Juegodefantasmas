using UnityEngine;

public class Dulces : MonoBehaviour
{
    public float velocidaddeRotacion = 100.0f;
    public float velocidaddePingPong = 1.0f;
    public float distanciadePingPong = 0.5f;

    private Vector3 posInicial;
    private float offset;

    private void Start()
    {
        posInicial = transform.position;
        offset = Random.Range(0.0f, 100.0f);
        transform.Rotate(0, Random.Range(0.0f, 360.0f),0);
    }
    void Update()
    {
        transform.Rotate(0, velocidaddeRotacion * Time.deltaTime, 0);
        float bounce = Mathf.PingPong((Time.time + 10) * velocidaddePingPong, distanciadePingPong);
        transform.position = posInicial + new Vector3(0, bounce, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
