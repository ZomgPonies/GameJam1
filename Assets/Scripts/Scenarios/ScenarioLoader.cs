using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioLoader : JSONFileParser<ScenarioLoader, Scenario>
{
    private Action CallBackOnConvertionFinished;

    public Scenario[] scenarios { get; private set; }
    
    public const string SCENARIOS_FOLDER_PATH = "Scenarios";
    public const string SCENARIOS_LIST_FILE = "ScenarioList.text";

    public void LoadScenarios(Action callBackOnConvertionFinished)
    {
        scenarios = ConvertJSONtoClass(Application.streamingAssetsPath + "/" + SCENARIOS_FOLDER_PATH + "/");
        callBackOnConvertionFinished();
    }

    public void LoadScenariosForWebGLPlayer(Action callBackOnConvertionFinished)
    {
        CallBackOnConvertionFinished = callBackOnConvertionFinished;
        StartCoroutine(ConvertJSONtoClassForWebGL(Application.streamingAssetsPath + "/" + SCENARIOS_FOLDER_PATH, SCENARIOS_LIST_FILE, OnLoadJSONForWebGLSuccess));
    }
    
    private void OnLoadJSONForWebGLSuccess(Scenario[] scenarios)
    {
        this.scenarios = scenarios;
        CallBackOnConvertionFinished();
    }
    
    public Scenario[] GetByRoomId(int id)
    {
        List<Scenario> roomScenario = new List<Scenario>();
        
        foreach (Scenario scenario in scenarios)
        {
            if (scenario.associatedRoomId == id) roomScenario.Add(scenario);
        }

        return roomScenario.ToArray();
    }
}
