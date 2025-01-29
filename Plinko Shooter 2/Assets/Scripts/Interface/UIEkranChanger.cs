using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIEkranChanger : MonoBehaviour
{
    [SerializeField] private string screenName;

    private UIEkran uiManager;
    private Button button;

    [Inject]
    private void Construct(UIEkran uiManager)
    {
        this.uiManager = uiManager;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            uiManager.ChangeScreen(screenName);
        });
    }
}
