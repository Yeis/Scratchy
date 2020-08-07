using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float respawnTime = 1.0f;
    public int maxItches = 5;
    private Vector2 screenBounds;
    private int currentItches = 0;
    public GameObject itch;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(itchSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnItch()
    {
        GameObject newItch = Instantiate(itch) as GameObject;
        SpriteRenderer itchSpriteRenderer = newItch.GetComponent<SpriteRenderer>();
        float width = itchSpriteRenderer.bounds.size.x;
        float height = itchSpriteRenderer.bounds.size.y;
        newItch.transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x - width), Random.Range(-screenBounds.y, screenBounds.y - height));
        currentItches++;
    }

    IEnumerator itchSpawner()
    {
        while (currentItches < maxItches)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnItch();

        }
    }
}
