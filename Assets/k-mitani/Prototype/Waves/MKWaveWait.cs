using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKWaveWait : MKWaveBase
{
    [SerializeField] private float waitDurationMax = 1f;

    private bool isClear = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitDurationMax);
        isClear = true;
    }

    public override bool IsWaveClear()
    {
        // 3つの頭全てがやられたらクリア。
        return isClear;
    }
}
