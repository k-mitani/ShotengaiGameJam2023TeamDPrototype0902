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
            StartCoroutine(LoadingSceneManager.LoadCoroutine("MKPrototypeScene", curtain));
        };

        StartCoroutine(MKUtil.BlinkText(pressAnyKey, textBlinkDurationMax));
    }

    private void OnDestroy()
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
    }
}
