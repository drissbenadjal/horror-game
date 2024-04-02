using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ChangeSensibility : MonoBehaviour
{
    [SerializeField] private Slider sliderSensibility;
    private float sensibilityValue;

    private string jsonFilePath = "sensibility.json";

    private void Start()
    {
        LoadSensibility();
        sliderSensibility.value = sensibilityValue;
        sliderSensibility.onValueChanged.AddListener(delegate { OnSensibilityChanged(); });
    }

    private void OnSensibilityChanged()
    {
        sensibilityValue = sliderSensibility.value;
        SaveSensibility();
    }

    private void LoadSensibility()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            SensibilityData data = JsonUtility.FromJson<SensibilityData>(json);
            sensibilityValue = data.sensibility;
        }
        else
        {
            CreateDefaultSensibilityFile(); // Crée le fichier JSON avec la valeur par défaut
        }
    }

    private void CreateDefaultSensibilityFile()
    {
        SensibilityData defaultData = new SensibilityData();
        defaultData.sensibility = 0.5f; // Valeur de sensibilité par défaut
        string json = JsonUtility.ToJson(defaultData);
        File.WriteAllText(jsonFilePath, json);
        sensibilityValue = defaultData.sensibility;
    }

    private void SaveSensibility()
    {
        SensibilityData data = new SensibilityData();
        data.sensibility = sensibilityValue;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(jsonFilePath, json);
    }

    private class SensibilityData
    {
        public float sensibility;
    }
}
