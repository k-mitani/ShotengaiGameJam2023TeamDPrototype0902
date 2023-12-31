using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingSceneManager : MonoBehaviour
{
    private static bool prevSceneCurtainClosed = false;
    private static string targetSceneName = null;

    public static IEnumerator LoadCoroutine(string sceneName, SceneTransitionCurtain curtain)
    {
        prevSceneCurtainClosed = false;
        targetSceneName = sceneName;
        curtain.Close(() =>
        {
            prevSceneCurtainClosed = true;
        });
        // まずロード画面を読み込む。
        return LoadSceneAsyncCoroutine("LoadingScene");
    }

    private static IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        var op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            if (op.progress >= 0.9f && prevSceneCurtainClosed)
            {
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }


    [SerializeField] private SceneTransitionCurtain curtain;
    [SerializeField] private string sceneName;

    private void Start()
    {
        sceneName = targetSceneName ?? sceneName;
        StartCoroutine(DoTransition());
    }

    private IEnumerator DoTransition()
    {
        prevSceneCurtainClosed = false;
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        yield return new WaitForSeconds(1f);
        curtain.Close(() =>
        {
            prevSceneCurtainClosed = true;
        });
    }
}
