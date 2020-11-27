using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        UpdateManager.Instance.ClearAll();
    }
    public void LoadNextLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UpdateManager.Instance.ClearAll();
    }

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
        UpdateManager.Instance.ClearAll();
    }
}
