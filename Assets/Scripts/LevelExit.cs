using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    private float levelLoadDelay = 2f;
    private float slowMotionTimeScale = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = slowMotionTimeScale;
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();
        Destroy(scenePersist);

        SceneManager.LoadScene(currentSceneIndex + 1);

       
    }
}
