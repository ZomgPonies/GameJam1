using System;
using UnityEngine;

public class InteractiveObjectModelLoader : JSONFileParser<InteractiveObjectModelLoader, InteractiveObjectModel>
{
    private Action CallBackOnConvertionFinished;

    public InteractiveObjectModel[] interactiveObjectModels { get; private set; }
    
    public const string INTERACTIF_OBJECT_MODELS_FOLDER_PATH = "InteractiveObjectModels";
    public const string INTERACTIF_OBJECT_MODELS_LIST_FILE = "InteractiveObjectModelList.text";

    public void LoadInteractiveObjectModels(Action callBackOnConvertionFinished)
    {
        interactiveObjectModels = ConvertJSONtoClass(Application.streamingAssetsPath + "/" + INTERACTIF_OBJECT_MODELS_FOLDER_PATH + "/");
        callBackOnConvertionFinished();
    }

    public void LoadInteractiveObjectModelsForWebGLPlayer(Action callBackOnConvertionFinished)
    {
        CallBackOnConvertionFinished = callBackOnConvertionFinished;
        StartCoroutine(ConvertJSONtoClassForWebGL(Application.streamingAssetsPath + "/" + INTERACTIF_OBJECT_MODELS_FOLDER_PATH, INTERACTIF_OBJECT_MODELS_LIST_FILE, OnLoadJSONForWebGLSuccess));
    }

    private void OnLoadJSONForWebGLSuccess(InteractiveObjectModel[] interactiveObjectModels)
    {
        this.interactiveObjectModels = interactiveObjectModels;
        CallBackOnConvertionFinished();
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

