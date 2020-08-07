using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MouseController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 100f;
    [SerializeField] public float interpolationPeriod = 1f;
    private float time = 0.0f;
    private Vector3 mousePosition;
    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private AudioSource audioData;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        //Lock Cursor to camera
        Cursor.lockState = CursorLockMode.Confined;
        rigidbody = GetComponent<Rigidbody2D>();
        audioData = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Scratching();
        PhysicalMovement();
    }

    private void Scratching()
    {
        time += Time.deltaTime;
        print(time);
        if (Input.mouseScrollDelta.y != 0)
        {
            if (time >= interpolationPeriod)
            {
                time = 0.0f;
                if (!audioData.isPlaying) audioData.Play();
                animator.SetBool("scratching", true);
            }
        }
        else
        {
            if (time >= interpolationPeriod)
            {
                // time = 0.0f;
                if (audioData.isPlaying) audioData.Stop();
                animator.SetBool("scratching", false);
            }
        }
    }
    
    private void PhysicalMovement()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rigidbody.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
}
