using UnityEngine;
using UnityEngine.UI;

public class AudioClickScript : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<AudioSource>().clip = audioClip;
            Camera.main.GetComponent<AudioSource>().Play();
        });
    }
}
