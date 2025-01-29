using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIEkransInstal : MonoInstaller
{
    [SerializeField] private UIEkran uiManager;

    public override void InstallBindings()
    {
        Container.Bind<UIEkran>().FromInstance(uiManager);
        uiManager.Init();
    }
}
