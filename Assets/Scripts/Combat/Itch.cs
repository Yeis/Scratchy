using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itch : MonoBehaviour
{
    [SerializeField] public float healthPoints = 20f;

    public void TakeDamage(float damage)
    {
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        if (healthPoints == 0)
        {
            EaseItch();
        }
    }

    private void EaseItch() {
        FindObjectOfType<Spawner>().DestroyItch(gameObject);
    }

}
