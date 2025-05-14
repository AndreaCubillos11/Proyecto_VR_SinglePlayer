using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using System.Collections.Generic;

// Script mejorado para interacciones de botones en VR
public class VRButtonInteraction : MonoBehaviour
{
    private Button button;
    public Distance distanceScript; // Asigna este componente en el inspector
    
    // Referencias para eventos de puntero
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    
    // Referencias a los transforms de los controladores
    public Transform transformControladorIzquierdo;
    public Transform transformControladorDerecho;
    
    // Parámetros de interacción
    public float distanciaInteraccion = 0.1f; // Distancia para interacción en metros
    private bool botonAggarrePresionado = false;
    
    void Start()
    {
        // Obtener componente de botón
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("No se encontró el componente Button en " + gameObject.name);
            return;
        }
        
        // Encontrar el sistema de eventos
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.LogError("No se encontró un EventSystem en la escena. Por favor, añade uno.");
            return;
        }
        
        // Inicializar datos de evento de puntero
        pointerEventData = new PointerEventData(eventSystem);
        
        // Encontrar referencias de controladores si no están asignadas
        if (transformControladorIzquierdo == null)
        {
            var manoIzquierda = GameObject.Find("LeftHand Controller");
            if (manoIzquierda != null) transformControladorIzquierdo = manoIzquierda.transform;
        }
        
        if (transformControladorDerecho == null)
        {
            var manoDerecha = GameObject.Find("RightHand Controller");
            if (manoDerecha != null) transformControladorDerecho = manoDerecha.transform;
        }
    }
    
    void Update()
    {
        ComprobarInteraccionControlador(XRNode.LeftHand, transformControladorIzquierdo);
        ComprobarInteraccionControlador(XRNode.RightHand, transformControladorDerecho);
    }
    
    void ComprobarInteraccionControlador(XRNode nodoControlador, Transform transformControlador)
    {
        // Saltar si no hay transform asignado
        if (transformControlador == null) return;
        
        // Obtener dispositivo del controlador
        InputDevice dispositivo = InputDevices.GetDeviceAtXRNode(nodoControlador);
        if (!dispositivo.isValid) return;
        
        // Comprobar estado del botón de agarre
        bool agarrePresionado = false;
        if (dispositivo.TryGetFeatureValue(CommonUsages.gripButton, out agarrePresionado))
        {
            // Calcular distancia entre el controlador y el botón
            float distancia = Vector3.Distance(transformControlador.position, transform.position);
            
            // Si el controlador está lo suficientemente cerca y el botón de agarre está presionado
            if (distancia <= distanciaInteraccion && agarrePresionado && !botonAggarrePresionado)
            {
                // Activar clic del botón
                EjecutarClicBoton();
            }
            
            botonAggarrePresionado = agarrePresionado;
        }
    }
    
    void EjecutarClicBoton()
    {
        // Registrar la interacción
        Debug.Log("Controlador VR interactuó con el botón: " + gameObject.name);
        
        // Activar el clic del botón UI
        if (button != null && button.interactable)
        {
            button.onClick.Invoke();
            
            // Reproducir respuesta háptica si está disponible
            ReproducirRespuestaHaptica();
        }
        
        // Llamar al script de distancia si está asignado
        if (distanceScript != null)
        {
            distanceScript.PlayAudio();
        }
        else
        {
            Debug.LogWarning("Script Distance no asignado a " + gameObject.name);
        }
    }
    
    void ReproducirRespuestaHaptica()
    {
        // Enviar pulso háptico a ambos controladores
        InputDevice dispositivoIzquierdo = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice dispositivoDerecho = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        
        if (dispositivoIzquierdo.isValid)
        {
            HapticCapabilities capacidades;
            if (dispositivoIzquierdo.TryGetHapticCapabilities(out capacidades) && capacidades.supportsImpulse)
            {
                dispositivoIzquierdo.SendHapticImpulse(0, 0.3f, 0.1f);
            }
        }
        
        if (dispositivoDerecho.isValid)
        {
            HapticCapabilities capacidades;
            if (dispositivoDerecho.TryGetHapticCapabilities(out capacidades) && capacidades.supportsImpulse)
            {
                dispositivoDerecho.SendHapticImpulse(0, 0.3f, 0.1f);
            }
        }
    }
}