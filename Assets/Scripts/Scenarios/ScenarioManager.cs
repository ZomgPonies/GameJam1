using System.IO;
using UnityEngine;

public class ScenarioManager : Singleton<ScenarioManager>
{
    public Scenario[] scenarios { get; private set; }

    public const string SCENARIOS_FILES_PATH = "../Data/Scenarios";

    public void LoadScenarios()
    {
        string[] files = Directory.GetFiles(Application.dataPath + "/" + SCENARIOS_FILES_PATH + "/", "*.json");

        scenarios = new Scenario[files.Length];

        for(int i = 0; i < files.Length; i++)
        {
            scenarios[i] = LoadScenarioFromFiles(files[i]);
        }
    }
    
    private Scenario LoadScenarioFromFiles(string path)
    {
        string scenarioFileContent = File.ReadAllText(path);

        return JsonUtility.FromJson<Scenario>(scenarioFileContent);
    }
}
