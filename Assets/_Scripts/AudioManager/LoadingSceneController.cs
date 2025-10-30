using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    public Slider progressBar;
    public Slider ProgressBar;
    public float minLoadTime;
    public float fillSpeed ;

    private void Start()
    {
        StartCoroutine(LoadMainMenuAsync());
    }

    IEnumerator LoadMainMenuAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("MainMenu");
        op.allowSceneActivation = false;

        float timer = 0f;
        float displayProgress = 0f;

        while (!op.isDone)
        {
           
            float targetProgress = Mathf.Clamp01(op.progress / 0.9f);

           
            displayProgress = Mathf.MoveTowards(displayProgress, targetProgress, Time.deltaTime * fillSpeed);

            progressBar.value = displayProgress;
            ProgressBar.value = displayProgress;

            timer += Time.deltaTime;

        
            if (op.progress >= 0.9f && timer >= minLoadTime && displayProgress >= 0.99f)
            {
                yield return new WaitForSeconds(1f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
