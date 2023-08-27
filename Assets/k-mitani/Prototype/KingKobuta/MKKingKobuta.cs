using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MKKingKobuta : MonoBehaviour
{
    [field: SerializeField] public bool ShouldPause { get; private set; } = false;
    [SerializeField] private MKKingKobutaFace[] m_faces;

    private void Start()
    {
        ApplyPauseState();
    }

    public void Pause()
    {
        ShouldPause = true;
        ApplyPauseState();
    }
    
    public void Resume()
    {
        ShouldPause = false;
        ApplyPauseState();
    }

    private void ApplyPauseState()
    {
        foreach (var face in m_faces)
        {
            face.m_collider.enabled = !ShouldPause && !face.IsDead;
        }
    }

    public bool IsAllDead => m_faces.All(f => f.hp <= 0);
}
