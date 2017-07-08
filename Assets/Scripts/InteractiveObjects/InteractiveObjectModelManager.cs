﻿using System.IO;
using UnityEngine;

public class InteractiveObjectModelManager : JSONFileParser<InteractiveObjectModelManager>
{
    public InteractiveObjectModel[] interactiveObjectModels { get; private set; }

    public const string INTERACTIF_OBJECT_MODELS_FILES_PATH = "../Data/InteractiveObjectModels";

    public void LoadInteractiveObjectModels()
    {
        interactiveObjectModels = ConvertJSONtoClass<InteractiveObjectModel>(Application.dataPath + "/" + INTERACTIF_OBJECT_MODELS_FILES_PATH + "/");
    }
}
