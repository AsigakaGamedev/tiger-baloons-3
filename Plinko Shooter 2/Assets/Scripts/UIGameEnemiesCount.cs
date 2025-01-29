using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UIGameEnemiesCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;

    [Inject] private GameManager gameManager;

    private void Start()
    {
        gameManager.onEnemiesChange += OnEnemiesChange;
        OnEnemiesChange(gameManager.AllEnemies.Count);
    }

    private void OnDestroy()
    {
        gameManager.onEnemiesChange -= OnEnemiesChange;
    }

    private void OnEnemiesChange(int count)
    {
        countText.text = count.ToString();
    }
}
