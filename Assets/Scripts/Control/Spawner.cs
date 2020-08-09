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
    private AudioSource audioData;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        audioData = GetComponent<AudioSource>();
        StartCoroutine(ItchSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnItch()
    {
        GameObject newItch = Instantiate(itch) as GameObject;
        SpriteRenderer itchSpriteRenderer = newItch.GetComponent<SpriteRenderer>();
        float width = itchSpriteRenderer.bounds.size.x;
        float height = itchSpriteRenderer.bounds.size.y;
        newItch.transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x - width), Random.Range(-screenBounds.y, screenBounds.y - height));
        currentItches++;
    }

    IEnumerator ItchSpawner()
    {
        while (currentItches < maxItches)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnItch();

        }
    }

    public void DestroyItch(GameObject itch)
    {
        audioData.Play();
        Destroy(itch);
        currentItches--;
    }
}
