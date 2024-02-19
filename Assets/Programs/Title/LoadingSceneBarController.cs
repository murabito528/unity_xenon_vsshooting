using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneBarController : MonoBehaviour
{
    [SerializeField] private GameObject _loadingUI;
    [SerializeField] private Slider _slider;
    public void LoadNextScene(string scenename)
    {
        //_loadingUI.SetActive(true);
        StartCoroutine(LoadScene(scenename));
    }
    IEnumerator LoadScene(string scenename)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scenename);
        while (!async.isDone)
        {
            //_slider.value = async.progress;
            yield return null;
        }
    }
}