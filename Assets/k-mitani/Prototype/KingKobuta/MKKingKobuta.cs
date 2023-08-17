using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MKKingKobuta : MonoBehaviour
{
    [field: SerializeField] public bool ShouldPause { get; set; } = false;
    [SerializeField] private MKKingKobutaFace[] m_faces;

    public bool IsAllDead => m_faces.All(f => f.hp <= 0);
}
