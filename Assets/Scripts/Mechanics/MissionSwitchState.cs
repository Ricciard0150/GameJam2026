using UnityEngine;

[RequireComponent(typeof(SwitchState))]
public class MissionSwitchState : MonoBehaviour
{
    [SerializeField] private string missionName; // Nome da missão
    [SerializeField] private bool completeOnTurnOn = true; // Liga ou desliga?
    [SerializeField] private GameObject targetObject; // Opcional

    private SwitchState switchState;

    void Start()
    {
        switchState = GetComponent<SwitchState>();

        // Inscreve no evento de ligar
        // Você precisa modificar o SwitchState para expor eventos
    }

    // Esse método será chamado pelo SwitchState quando ligar
    public void OnTurnOn()
    {
        Debug.Log($"💡 LightSwitch ligado! Completando missão: {missionName}");

        if (MissionSystem.Instance != null)
        {
            if (!string.IsNullOrEmpty(missionName))
                MissionSystem.Instance.CompleteMission(missionName);
            else if (targetObject != null)
                MissionSystem.Instance.CompleteMissionByObject(targetObject);
        }
    }

    public void OnTurnOff()
    {
        Debug.Log("💡 LightSwitch desligado");
        // Pode descompletar? Deixe como está
    }
}