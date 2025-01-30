using UnityEngine;

public class AudioEnableScript : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void OnEnable()
    {
        Camera.main.GetComponent<AudioSource>().clip = audioClip;
        Camera.main.GetComponent<AudioSource>().Play();
    }
}
