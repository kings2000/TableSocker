using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
