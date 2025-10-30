using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "LoadingScene";
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
