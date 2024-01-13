using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseMap : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && SceneManager.loadedSceneCount == 2)
        {
            SceneManager.UnloadSceneAsync("WorldMap");
            Time.timeScale = 1f;
        }
    }
}
