using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MKDialog : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI message;
    [SerializeField] public Button yesButton;
    [SerializeField] public Button noButton;
    [SerializeField] public Button cancelButton;

    private Action<MKDialogResult> onClose;

    private void Start()
    {
        if (yesButton != null) yesButton.onClick.AddListener(OnYesClick);
        if (noButton != null) noButton.onClick.AddListener(OnNoClick);
        if (cancelButton != null) cancelButton.onClick.AddListener(OnCancelClick);
    }

    public event EventHandler<MKDialogResult> Result;

    public void OnYesClick()
    {
        Debug.Log("yes");
        Result?.Invoke(this, MKDialogResult.Yes);
        onClose?.Invoke(MKDialogResult.Yes);
        onClose = null;
        gameObject.SetActive(false);
    }

    public void OnNoClick()
    {
        Debug.Log("no");
        Result?.Invoke(this, MKDialogResult.No);
        onClose?.Invoke(MKDialogResult.No);
        onClose = null;
        gameObject.SetActive(false);
    }

    public void OnCancelClick()
    {
        Debug.Log("cancel");
        Result?.Invoke(this, MKDialogResult.Cancel);
        onClose?.Invoke(MKDialogResult.Cancel);
        onClose = null;
        gameObject.SetActive(false);
    }

    internal void Show(Action<MKDialogResult> onClose = null)
    {
        gameObject.SetActive(true);
        this.onClose = onClose;
    }
}

public enum MKDialogResult
{
    None,
    Yes,
    No,
    Cancel,
}
