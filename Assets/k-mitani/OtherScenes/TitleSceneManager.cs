using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private SceneTransitionCurtain curtain = null;
    [SerializeField] private TextMeshProUGUI pressAnyKey;
    [SerializeField] private float textBlinkDurationMax = 1f;

    private List<IDisposable> disposables = new List<IDisposable>();

    // Start is called before the first frame update
    void Start()
    {
        var pressAnyKeyAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "*/<Button>",
            interactions: "Press");
        disposables.Add(pressAnyKeyAction);
        pressAnyKeyAction.Enable();
        pressAnyKeyAction.performed += _ =>
        {
            pressAnyKeyAction.Disable();
            StartCoroutine(LoadingSceneManager.LoadCoroutine("MKPrototypeScene-k-mitani", curtain));
        };

        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        var a = 0f;
        var originalColor = pressAnyKey.color;
        pressAnyKey.color = originalColor * new Color(1, 1, 1, a);
        while (true)
        {
            var gameOverBlinkDuration = textBlinkDurationMax;
            while (true)
            {
                yield return null;
                gameOverBlinkDuration -= Time.deltaTime;
                if (gameOverBlinkDuration <= 0) break;
                a = 1 - gameOverBlinkDuration / textBlinkDurationMax;
                pressAnyKey.color = originalColor * new Color(1, 1, 1, a);
            }
            yield return new WaitForSeconds(0.3f);
            gameOverBlinkDuration = textBlinkDurationMax;
            while (true)
            {
                yield return null;
                gameOverBlinkDuration -= Time.deltaTime;
                if (gameOverBlinkDuration <= 0) break;
                a = gameOverBlinkDuration / textBlinkDurationMax;
                pressAnyKey.color = originalColor * new Color(1, 1, 1, a);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
    }
}
