using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MKPlayerKobuta : MonoBehaviour
{
    [field: SerializeField] public MKKobutaType Type { get; private set; }
    private SpriteRenderer m_renderer;

    private void Awake()
    {
        TryGetComponent(out m_renderer);
    }
}
