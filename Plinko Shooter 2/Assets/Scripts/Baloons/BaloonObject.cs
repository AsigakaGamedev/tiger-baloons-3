using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaloonObject : MonoBehaviour
{
    [SerializeField] private HealthController health;
    [SerializeField] private GameObject afterDestroyObj;
    [SerializeField] private ParticleSystem destroyEffect;
    [SerializeField] private AudioClip audioClip;

    private int animalType;

    private void Start()
    {
        GameManager.Instance.AddBaloon();
        health.onDamage += OnDamage;
        health.onDie += OnDie;
    }

    private void OnDestroy()
    {
        health.onDamage -= OnDamage;
        health.onDie -= OnDie;
    }

    private void OnDamage(float damage)
    {

    }

    public void SetAnimalType(int type)
    {
        animalType = type;
    }

    private void OnDie()
    {
        Camera.main.GetComponent<AudioSource>().clip = audioClip;
        Camera.main.GetComponent<AudioSource>().Play();
        GameManager.Instance.RemoveBaloons();
        GameObject newObj = Instantiate(afterDestroyObj, transform.position, Quaternion.identity);

        if (newObj.TryGetComponent(out Animal animal))
        {
            animal.SetType(animalType);
        }

        ParticleSystem desEffect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(desEffect.gameObject, 2);
        Destroy(gameObject);
    }
}
