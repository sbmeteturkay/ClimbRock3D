using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{

    // Start is called before the first frame update
    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void NextScene()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings != UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
