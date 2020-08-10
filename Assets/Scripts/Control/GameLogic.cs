using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public int pointsToWin = 10;
    public int currentPoints = 0;
    private AudioSource audioData;

    private void Start() {
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(currentPoints >= pointsToWin) {
            //You Win the game
            StartCoroutine(ChangeSceneAfterSound("End_Scene"));
        }
    }

    IEnumerator ChangeSceneAfterSound(string sceneName)
    {
        audioData.pitch = 1.0f;
        audioData.volume = 1.0f;
        audioData.loop = false;
        audioData.Play();
        yield return new WaitForSeconds(audioData.clip.length + 1);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

}
