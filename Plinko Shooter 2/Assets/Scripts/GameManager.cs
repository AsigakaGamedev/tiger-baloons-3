using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> allEnemies;

    public Action<int> onEnemiesChange;

    public List<Enemy> AllEnemies { get => allEnemies; }

    [Inject] private UIEkran uiEkrans;

    public bool IsWin;
    public bool IsLose;

    public static GameManager Instance { get; private set; }

    private void OnEnable()
    {
        Time.timeScale = 1;
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    public void Init()
    {
        allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
    }

    private void Update()
    {
        if (IsWin || IsLose) return;

        for (int i = 0; i < allEnemies.Count; i++)
        {
            if (allEnemies[i] == null)
            {
                allEnemies.RemoveAt(i);
                onEnemiesChange?.Invoke(allEnemies.Count);
                if (allEnemies.Count <= 0)
                {
                    Win();
                }
            }
        }

        if (baloons == 0 && animals == 0)
        {
            curloseTimer -= Time.deltaTime;

            if (curloseTimer <= 0)
            {
                Time.timeScale = 0;
                IsLose = true;
                uiEkrans.ChangeScreen("lose");
            }
        }
        else if (IsEmpty && animals == 0)
        {
            curloseTimer -= Time.deltaTime;

            if (curloseTimer <= 0)
            {
                Time.timeScale = 0;
                IsLose = true;
                uiEkrans.ChangeScreen("lose");
            }
        }
    }

    private float loseTimer = 3;
    private float curloseTimer = 3;

    public void Win()
    {
        if (IsWin) return;

        IsWin = true;
        uiEkrans.ChangeScreen("win");
    }

    private int baloons = 0;
    private int animals = 0;

    public void AddBaloon()
    {
        baloons++;
    }

    private bool IsEmpty;

    public void SetEmpty()
    {
        IsEmpty = true;
    }

    public void RemoveBaloons()
    {
        baloons--;
        curloseTimer = loseTimer;
    }

    public void AddAnimals()
    {
        animals++;
    }

    public void RemoveAnimals()
    {
        animals--;
    }
}
