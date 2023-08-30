using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class MKWaveRandomChoise : MKWaveBase
{
    private MKWaveBase[] childWaves;
    private MKWaveBase selectedWave;

    protected override void Start()
    {
        base.Start();

        childWaves = GetComponentsInChildren<MKWaveBase>(true)
            .Where(x => x != this)
            .ToArray();
        foreach (var wave in childWaves)
        {
            wave.gameObject.SetActive(false);
        }
        selectedWave = childWaves[Random.Range(0, childWaves.Length)];
        selectedWave.gameObject.SetActive(true);

    }

    public override bool IsWaveClear()
    {
        return selectedWave.IsWaveClear();
    }
}
