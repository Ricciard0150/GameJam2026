using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))] // ← FORÇA o Outline a existir
public class SwitchState : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isOn;
    [SerializeField] private UnityEvent OnTurnOn;
    [SerializeField] private UnityEvent OnTurnOff;

    private Outline outline;

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
        Debug.Log("🔄 Interagindo com: " + gameObject.name);

        if (isOn)
        {
            Debug.Log("🔴 Desligando");
            OnTurnOff.Invoke();
            isOn = false;
        }
        else
        {
            Debug.Log("🟢 Ligando");
            OnTurnOn.Invoke();
            isOn = true;
        }
    }

    private void Start()
    {
        outline = GetComponent<Outline>();

        if (outline == null)
        {
            Debug.LogError($"❌ Outline não encontrado em {gameObject.name}! Adicione o componente Outline.");
            return;
        }

        outline.enabled = false;
        Debug.Log($"✅ SwitchState inicializado em {gameObject.name}");
    }
}