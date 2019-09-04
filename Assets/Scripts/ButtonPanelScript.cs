using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPanelScript : MonoBehaviour
{
    public void loadGame()
    {
        // Spielt nächste sequenz ab
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}


