using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionCurtain : MonoBehaviour
{
    [SerializeField] private bool openedAtStart = false;
    [SerializeField] private float durationMax = 0.5f;

    private void Awake()
    {
        if (openedAtStart)
        {
            transform.localScale = new Vector3(1, 0, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            Open();
        }
    }

    public void Open(Action onFinished = null)
    {
        StartCoroutine(OpenCoroutine(onFinished));
    }

    private IEnumerator OpenCoroutine(Action onFinished = null)
    {
        var duration = durationMax;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            transform.localScale = new Vector3(1, duration / durationMax, 1);
            yield return null;
        }
        transform.localScale = new Vector3(1, 0, 1);
        onFinished?.Invoke();
    }

    public void Close(Action onFinished = null)
    {
        StartCoroutine(CloseCoroutine(onFinished));
    }

    private IEnumerator CloseCoroutine(Action onFinished = null)
    {
        var duration = durationMax;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            transform.localScale = new Vector3(1, 1 - duration / durationMax, 1);
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
        onFinished?.Invoke();
    }
}
