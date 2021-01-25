using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;
    public ObjectFactory objectFactory;
    public MaterialFactory materialFactory;
    public FormationFactory formationFactory;

    private void Awake()
    {

        instance = this;
        if (!SceneManager.GetSceneByBuildIndex((int)Enums.SceneId.MatchUi).isLoaded)
            SceneManager.LoadSceneAsync((int)Enums.SceneId.MatchUi, LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByBuildIndex((int)Enums.SceneId.Map1).isLoaded)
            SceneManager.LoadSceneAsync((int)Enums.SceneId.Map1, LoadSceneMode.Additive);

    }

    private void Start()
    {
        
        //if (!SceneManager.GetSceneByBuildIndex(4).isLoaded)
        //    SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        //if (!SceneManager.GetSceneByBuildIndex(5).isLoaded)
        //    SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);
        Invoke(nameof(StartInit), 1);
    }

    void StartInit()
    {
        MatchHandler.instance.Init();
    }
}
