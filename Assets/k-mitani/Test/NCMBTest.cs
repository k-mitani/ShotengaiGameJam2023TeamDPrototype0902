using System.Collections;
using System.Collections.Generic;
using NCMB;
using UnityEngine;

public class NCMBTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
