using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpenMap : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && SceneManager.loadedSceneCount == 1)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("WorldMap", LoadSceneMode.Additive);
        }
    }
}
