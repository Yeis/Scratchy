using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratcher : MonoBehaviour
{
    [SerializeField] public float scratchStrength = 5f;
    public bool isScratching = false;

    private GameObject target;



    void OnTriggerStay2D(Collider2D other)
    {
        if (isScratching)
        {
            GameObject itch = other.gameObject;
            itch.GetComponent<Itch>().TakeDamage(scratchStrength);
        }
        print(other.name);
    }

}
