using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneLoader : MonoBehaviour
{
    public void ChangeScean(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();

        #else
        Application.Quit();
        #endif
    }
}
