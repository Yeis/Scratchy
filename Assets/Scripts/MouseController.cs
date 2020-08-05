using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 100f;
    private Vector3 mousePosition;
    private Rigidbody2D rigidbody;
    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        //Lock Cursor to camera
        Cursor.lockState = CursorLockMode.Confined;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(Input.mouseScrollDelta.y);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rigidbody.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

    }
}
