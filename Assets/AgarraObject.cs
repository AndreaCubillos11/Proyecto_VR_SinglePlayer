using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    [Header("Configuración")]
    public float radioDeteccion = 0.5f;
    public float fuerzaAgarre = 20f;
    public Transform puntoAgarre;
    public string tagObjeto = "ObjetoAgarrable";
    public KeyCode teclaAgarrar = KeyCode.E; // Tecla configurable desde el Inspector

    private GameObject objetoAgarrado;
    private bool agarrando = false;
    private Rigidbody rbObjeto;

    void Update()
    {
        // Input clásico de Unity (sin Input System)
        if (Input.GetKeyDown(teclaAgarrar))
        {
            if (!agarrando)
                TryAgarrar();
            else
                Soltar();
        }

        // Mover objeto agarrado
        if (agarrando && objetoAgarrado != null)
        {
            Vector3 direccion = (puntoAgarre.position - objetoAgarrado.transform.position);
            rbObjeto.linearVelocity = direccion * fuerzaAgarre * Time.deltaTime;
        }
    }

    void TryAgarrar()
    {
        Collider[] objetosCercanos = Physics.OverlapSphere(puntoAgarre.position, radioDeteccion);

        foreach (Collider col in objetosCercanos)
        {
            if (col.CompareTag(tagObjeto))
            {
                objetoAgarrado = col.gameObject;
                rbObjeto = objetoAgarrado.GetComponent<Rigidbody>();
                
                if (rbObjeto != null)
                {
                    rbObjeto.useGravity = false;
                    agarrando = true;
                    Debug.Log("Objeto agarrado: " + objetoAgarrado.name);
                    return;
                }
            }
        }
        Debug.Log("No se encontraron objetos agarrables cerca");
    }

    void Soltar()
    {
        if (objetoAgarrado != null && rbObjeto != null)
        {
            rbObjeto.useGravity = true;
            Debug.Log("Objeto soltado: " + objetoAgarrado.name);
        }
        objetoAgarrado = null;
        rbObjeto = null;
        agarrando = false;
    }

    // Visualización del radio en el Editor
    void OnDrawGizmosSelected()
    {
        if (puntoAgarre != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(puntoAgarre.position, radioDeteccion);
        }
    }
}