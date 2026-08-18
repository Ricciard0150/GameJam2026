using UnityEngine;

[System.Serializable]
public class Mission
{
    public string missionName;
    [TextArea] public string description;
    public bool isCompleted = false;
    public GameObject targetObject; // Objeto alvo (opcional)

    public void Complete()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            Debug.Log($"✅ Missão completada: {missionName}");
        }
    }
}