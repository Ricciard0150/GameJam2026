
using  UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // Se usar TextMeshPro

public class MissionUI : MonoBehaviour
{
    [SerializeField] private GameObject missionPanel; // Painel pai
    [SerializeField] private GameObject missionPrefab; // Prefab do item de missão
    [SerializeField] private Transform missionContainer; // Onde os itens vão

    private List<GameObject> missionItems = new List<GameObject>();

    void Start()
    {
        // Esconde o painel no começo (opcional)
        if (missionPanel != null)
            missionPanel.SetActive(true);
    }

    void Update()
    {
        // Abrir/fechar com TAB (opcional)
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (missionPanel != null)
                missionPanel.SetActive(!missionPanel.activeSelf);
        }
    }

    public void UpdateUI(List<Mission> missions)
    {
        // Limpa itens antigos
        foreach (GameObject item in missionItems)
        {
            Destroy(item);
        }
        missionItems.Clear();

        // Cria novos itens
        foreach (Mission mission in missions)
        {
            GameObject newItem = Instantiate(missionPrefab, missionContainer);
            missionItems.Add(newItem);

            // Configura o texto
            Text missionText = newItem.GetComponentInChildren<Text>();
            if (missionText != null)
            {
                if (mission.isCompleted)
                {
                    missionText.text = $"✅ <s>{mission.missionName}</s>";
                    missionText.color = Color.gray;
                }
                else
                {
                    missionText.text = $"⬜ {mission.missionName}";
                    missionText.color = Color.white;
                }
            }

            // Se usar TMP:
            // TextMeshProUGUI tmp = newItem.GetComponentInChildren<TextMeshProUGUI>();
            // if (tmp != null) { ... }
        }
    }
}