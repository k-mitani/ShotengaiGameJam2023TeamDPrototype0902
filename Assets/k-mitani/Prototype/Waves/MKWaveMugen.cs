using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class MKWaveMugen : MKWaveBase
{
    [SerializeField] float endTime = -1;
    [SerializeField] int kobunCount = 10;
    [SerializeField] int kobunCountMax = 50;
    [SerializeField] int kobunCountStep = 1;
    [SerializeField] Vector3 generationLimtTopLeft = Vector3.zero;
    [SerializeField] Vector3 generationLimtBottomRight = Vector3.zero;
    [SerializeField] MKKobun[] kobunPrefabs;


    private List<MKKobun> kobunsCurrentWave = new List<MKKobun>();

    private float elappsedTime = 0f;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Waves());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(generationLimtTopLeft, 0.1f);
        Gizmos.DrawSphere(generationLimtBottomRight, 0.1f);
    }

    private IEnumerator Waves()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            var anyAlive = false;
            for (int i = 0; i < kobunsCurrentWave.Count; i++)
            {
                if (!kobunsCurrentWave[i].IsDestroyed())
                {
                    anyAlive = true;
                    break;
                }
            }

            if (!anyAlive)
            {
                kobunCount += kobunCountStep;
                yield return new WaitForSeconds(1);
                for (int i = 0; i < kobunCount; i++)
                {
                    var prefabIndex = Random.Range(0, kobunPrefabs.Length);
                    var prefab = kobunPrefabs[prefabIndex];
                    var kobun = Instantiate(prefab, transform);
                    kobun.transform.position = new Vector3(
                        Random.Range(generationLimtTopLeft.x, generationLimtBottomRight.x),
                        Random.Range(generationLimtBottomRight.y, generationLimtTopLeft.y),
                        prefabIndex * 0.1f);
                    kobunsCurrentWave.Add(kobun);
                }
            }
        }
    }

    private void Update()
    {
        elappsedTime += Time.deltaTime;
    }


    protected override bool IsWaveClear()
    {
        return (endTime != -1 && elappsedTime > endTime) || kobunCount > kobunCountMax;
    }
}
