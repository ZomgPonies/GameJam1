using System.IO;
using UnityEngine;

public class InteractiveObjectModelLoader : JSONFileParser<InteractiveObjectModelLoader>
{
    public InteractiveObjectModel[] interactiveObjectModels { get; private set; }

    public const string INTERACTIF_OBJECT_MODELS_FILES_PATH = "../Data/InteractiveObjectModels";

    public void LoadInteractiveObjectModels()
    {
        interactiveObjectModels = ConvertJSONtoClass<InteractiveObjectModel>(Application.dataPath + "/" + INTERACTIF_OBJECT_MODELS_FILES_PATH + "/");
    }

    public InteractiveObjectModel GetById(int id)
    {
        foreach (InteractiveObjectModel interactiveObjectModel in interactiveObjectModels)
        {
            if (interactiveObjectModel.id == id) return interactiveObjectModel;
        }

        return null;
    }
}

