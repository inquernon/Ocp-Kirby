// Responsabilidad: manejar poderes de Kirby (SRP respetado)
// Viola OCP: agregar un poder nuevo = modificar este archivo
using UnityEngine;



public class KirbyPoderes : MonoBehaviour
{
    public GameObject modeloNormal;
    public GameObject modeloCaja;
    public GameObject modeloTiburon;
    public GameObject modeloRana;

    private string poderActual = "ninguno";
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        modeloNormal.SetActive(true);
        modeloCaja.SetActive(false);
        modeloTiburon.SetActive(false);
        modeloRana.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            UsarPoder();
    }

    // Cada poder nuevo agrega un else if aquí
    void UsarPoder()
    {
        if (poderActual == "Tiburon")
        {
            // lógica de tiburon
        }
        else if (poderActual == "Rana")
        {
            // lógica de rana
        }
        else if (poderActual == "Caja")
        {
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Sin poder");
        }
    }

    // Cada poder nuevo agrega un else if aquí también
    public void AbsorberPoder(string poder)
    {
        poderActual = poder;
        Debug.Log("Absorbiste: " + poder);

        // Apaga todos los modelos
        modeloNormal.SetActive(false);
        modeloCaja.SetActive(false);
        modeloTiburon.SetActive(false);
        modeloRana.SetActive(false);

        // Enciende el que corresponde
        if (poder == "Tiburon") modeloTiburon.SetActive(true);
        else if (poder == "Rana") modeloRana.SetActive(true);
        else if (poder == "Caja") modeloCaja.SetActive(true);
    }
}
