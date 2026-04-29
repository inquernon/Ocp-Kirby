using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KirbyMovimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;
    public Transform camara; // Arrastra la Main Camera aquí en el Inspector

    private Rigidbody rb;
    private bool enSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Si no asignaste la cámara, la buscamos automáticamente
        if (camara == null) camara = Camera.main.transform;
    }

    void Update()
    {
        MoverRelativoACamara();

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        }
    }

    void MoverRelativoACamara()
    {
        float x = Input.GetAxisRaw("Horizontal"); // Usa GetAxisRaw para una respuesta más limpia
        float z = Input.GetAxisRaw("Vertical");

        // 1. Obtener direcciones de la cámara sin la inclinación vertical (Y = 0)
        Vector3 forward = camara.forward;
        Vector3 right = camara.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // 2. Calcular la dirección final multiplicando los inputs por los vectores de la cámara
        Vector3 dirDeseada = (forward * z + right * x).normalized;

        if (dirDeseada != Vector3.zero)
        {
            // Mover el Rigidbody
            rb.linearVelocity = new Vector3(dirDeseada.x * velocidad, rb.linearVelocity.y, dirDeseada.z * velocidad);

            // Rotar suavemente hacia la dirección de movimiento
            Quaternion rotacionDeseada = Quaternion.LookRotation(dirDeseada);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 10f);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void OnCollisionStay(Collision col) // Cambiado a Stay para mayor estabilidad
    {
        if (col.gameObject.CompareTag("Suelo")) enSuelo = true;
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Suelo")) enSuelo = false;
    }
}