using System.IO;
using UnityEngine;

public class ScenarioManager : JSONFileParser<ScenarioManager>
{
    public Scenario[] scenarios { get; private set; }

    public const string SCENARIOS_FILES_PATH = "../Data/Scenarios";

    public void LoadScenarios()
    {
        scenarios = ConvertJSONtoClass<Scenario>(Application.dataPath + "/" + SCENARIOS_FILES_PATH + "/");
    }
}
