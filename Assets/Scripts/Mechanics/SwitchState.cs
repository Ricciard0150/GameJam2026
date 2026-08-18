using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class SwitchState : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isOn = true; // Começa LIGADO
    [SerializeField] private UnityEvent OnTurnOff; // Só evento de desligar
    [SerializeField] private UnityEvent OnCompleteMission; // Evento separado pra missão

    private Outline outline;
    private bool isCompleted = false;

    public void HideOutline()
    {
        if (outline != null)
            outline.enabled = false;
    }

    public void ShowOutline()
    {
        if (outline != null)
            outline.enabled = true;
    }

    public void Interact()
    {
        if (isCompleted)
        {
            Debug.Log("⚠️ Já foi desligado!");
            return;
        }

        if (isOn)
        {
            Debug.Log("🔴 Desligando: " + gameObject.name);
            OnTurnOff.Invoke();          // Desliga (ex: apaga a luz)
            OnCompleteMission.Invoke();  // Completa a missão
            isOn = false;
            isCompleted = true;
        }
        else
        {
            Debug.Log("⚠️ Já está desligado!");
        }
    }

    private void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;

        isOn = true; // Começa ligado
        Debug.Log($"✅ SwitchState inicializado em: {gameObject.name} (começa ligado)");
    }
}