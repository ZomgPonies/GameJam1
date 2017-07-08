﻿using System.Collections.Generic;
using UnityEngine;

public class ScenarioLoader : JSONFileParser<ScenarioLoader>
{
    public Scenario[] scenarios { get; private set; }

    public const string SCENARIOS_FILES_PATH = "../Data/Scenarios";

    public void LoadScenarios()
    {
        scenarios = ConvertJSONtoClass<Scenario>(Application.dataPath + "/" + SCENARIOS_FILES_PATH + "/");
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