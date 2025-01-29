using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int type;

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    public void SetType(int type)
    {
        this.type = type;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BaloonObject baloon))
        {
            baloon.SetAnimalType(type);
        }

        if (collision.gameObject.TryGetComponent(out HealthController health))
        {
            health.Damage(10);
        }

        Destroy(gameObject);
    }
}
