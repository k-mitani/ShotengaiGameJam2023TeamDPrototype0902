using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfo", menuName = "StageInfo", order = 0)]
public class StageInfo : ScriptableObject
{
    [SerializeField] public string stageName;
    [SerializeField] public Texture2D stageImage;
    [SerializeField] public float mokuhyoJosenritu = 0.8f;
    [SerializeField] public string sceneName;
    
    [System.NonSerialized] public float? bestScore;
}
