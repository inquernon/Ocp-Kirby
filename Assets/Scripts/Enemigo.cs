// Responsabilidad: identificar qué poder otorga este enemigo
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public string poder; // tipo de modelo

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<KirbyPoderes>().AbsorberPoder(poder);
        }
    }
}