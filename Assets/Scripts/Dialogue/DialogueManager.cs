using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text authors, title;
    public Animator animator;
    public float timeBetweenLetters = 0.1f;
    public AudioClip femaleBlip;
    public AudioClip maleBlip;
    public AudioClip busDoor;
    private Queue<string> sentences;
    private AudioSource audioData;
    private FadeController fadeController;
    private string currentDialogueName;
    private DialogueTrigger narrator;
    private DialogueTrigger player;
    private bool isSoundPlaying;

    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        audioData = GetComponent<AudioSource>();
        fadeController = GetComponent<FadeController>();

        //We start the game with everything fade In;
        fadeController.FadeIn(0);
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Beginning_Scene")
        {
            narrator = GameObject.Find("NarratorDialogue").GetComponent<DialogueTrigger>();
            player = GameObject.Find("PlayerDialogue").GetComponent<DialogueTrigger>();
            narrator.TriggerDialogue();
        }
        else if (scene.name == "Bus_Scene")
        {
            fadeController.FadeOut(2);
            player = GameObject.Find("PlayerDialogue").GetComponent<DialogueTrigger>();
            player.TriggerDialogue();
        }
        else if (scene.name == "House_Overview")
        {
            fadeController.FadeOut(2);
            player = GameObject.Find("PlayerDialogue").GetComponent<DialogueTrigger>();
            player.TriggerDialogue();
        }
        else if (scene.name == "End_Scene")
        {
            title.enabled = false;
            authors.enabled = false;
            fadeController.FadeOut(2);
            player = GameObject.Find("PlayerDialogue").GetComponent<DialogueTrigger>();
            player.TriggerDialogue();
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
        else if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (scene.name == "Beginning_Scene" && narrator.hasFinished && !audioData.isPlaying)
        {
            StartCoroutine(ChangeSceneAfterSound("Bus_Scene"));
        }
        else if (scene.name == "Bus_Scene" && player.hasFinished && !audioData.isPlaying)
        {
            fadeController.FadeIn(1);
            StartCoroutine(ChangeSceneAfterSound("House_Overview"));
        }
        else if (scene.name == "House_Overview" && player.hasFinished && !audioData.isPlaying)
        {
            fadeController.FadeIn(1);
            StartCoroutine(ChangeSceneAfterSound("Scratch_Game"));
        }
        else if (scene.name == "End_Scene" && player.hasFinished && !audioData.isPlaying)
        {
            fadeController.FadeIn(1);
            title.enabled = true;
            authors.enabled = true;
            StartCoroutine(FinishGameAfterDuration(5));
        }


    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        //we don't have any names so far
        // nameText.text = dialogue.name;
        print("dialogue of: " + dialogue.name);
        print("number of sentences: " + dialogue.sentences.Length);

        nameText.text = "";
        currentDialogueName = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, currentDialogueName));
    }

    IEnumerator FinishGameAfterDuration(int seconds) {
        yield return new WaitForSeconds(seconds);
        print("We are Done");
        Application.Quit();
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

    IEnumerator TypeSentence(string sentence, string name)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            if (name == "narrator") audioData.PlayOneShot(femaleBlip, 1f);
            if (name == "player") audioData.PlayOneShot(maleBlip, 1f);
            // yield return null;
            if (narrator != null && narrator.hasFinished) break;
            if (player != null && player.hasFinished) break;

            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        audioData.Stop();
        if (currentDialogueName == "narrator") narrator.hasFinished = true;
        if (currentDialogueName == "player") player.hasFinished = true;

    }
}
