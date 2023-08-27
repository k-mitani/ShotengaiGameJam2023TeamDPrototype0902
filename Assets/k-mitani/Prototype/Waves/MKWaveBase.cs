using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKWaveBase : MonoBehaviour
{
    public event EventHandler WaveClear;

    void Start()
    {
        StartCoroutine(WaveClearCheckLoop());
    }

    protected IEnumerator WaveClearCheckLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            var clear = IsWaveClear();
            if (clear)
            {
                Debug.Log($"{name} Clear!");
                WaveClear?.Invoke(this, EventArgs.Empty);
                yield break;
            }
        }
    }

    protected virtual bool IsWaveClear()
    {
        // デフォルトでは、子要素が全て無くなったらクリアとする。
        return transform.childCount == 0;
    }
}
