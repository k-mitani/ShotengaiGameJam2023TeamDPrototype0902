using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
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

    public static Color GetColor(MKKobutaType type) => type switch
    {
        MKKobutaType.Red => Color.red,
        MKKobutaType.Green => Color.green,
        MKKobutaType.Blue => Color.blue,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
    };

    public static IEnumerator BlinkText(TextMeshProUGUI text, float blinkDurationMax, float blightDuration = 0.3f)
    {
        var a = 0f;
        var originalColor = text.color;
        text.color = originalColor * new Color(1, 1, 1, a);
        while (true)
        {
            var duration = blinkDurationMax;
            while (true)
            {
                yield return null;
                duration -= Time.deltaTime;
                if (duration <= 0) break;
                a = 1 - duration / blinkDurationMax;
                text.color = originalColor * new Color(1, 1, 1, a);
            }
            yield return new WaitForSeconds(blightDuration);
            duration = blinkDurationMax;
            while (true)
            {
                yield return null;
                duration -= Time.deltaTime;
                if (duration <= 0) break;
                a = duration / blinkDurationMax;
                text.color = originalColor * new Color(1, 1, 1, a);
            }
        }
    }
}


public enum MKKobutaType
{
    Red,
    Green,
    Blue,
}
