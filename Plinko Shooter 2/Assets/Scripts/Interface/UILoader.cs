using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoader : MonoBehaviour
{
    [SerializeField] private Button loadBtn;
    [SerializeField] private int sceneIndex;

    private void Start()
    {
        loadBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneIndex);
        });
    }
}
