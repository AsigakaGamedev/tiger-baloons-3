using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private string screenName;
    [SerializeField] private CursorLockMode cursorMode;
    [SerializeField] private bool cursorIsVisible = true;

    public string ScreenName { get => screenName; }
    public CursorLockMode CursorMode { get => cursorMode; }
    public bool CursorIsVisible { get => cursorIsVisible; }

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public void HideScreen()
    {
        gameObject.SetActive(false);
    }
}
