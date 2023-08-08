using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class MKUtil
{
    public static MKKobutaType NextType(MKKobutaType type) => type switch
    {
        MKKobutaType.Red => MKKobutaType.Green,
        MKKobutaType.Green => MKKobutaType.Blue,
        MKKobutaType.Blue => MKKobutaType.Red,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
    };
}


public enum MKKobutaType
{
    Red,
    Green,
    Blue,
}
