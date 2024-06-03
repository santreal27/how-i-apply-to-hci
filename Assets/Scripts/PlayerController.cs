using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    [SerializeField] private float moveSpeed = 1f;

    public delegate void InputEvents();
    public static event InputEvents OnInteract;
    public static event InputEvents OnEnterInteractable;
    public static event InputEvents OnExitInteractable;

    public GameEvent dialogueStartEvent;

    private Rigidbody2D rb;

    Vector2 movement;

    bool canMove = true;
    bool canInteract = false;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();


    }
 
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerInputActions.Player.Interact.performed += x =>
        {
            if (OnInteract != null)
                OnInteract();

        };
  
    }
    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            dialogueStartEvent.Raise(this, 1);
        }
        if (playerInputActions.Player.Interact.triggered)
        {
            Debug.Log("Pressed E");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove)
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canInteract = true;
            if(OnEnterInteractable != null) OnEnterInteractable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Interactable"))
        {
            canInteract = false;
            if(OnExitInteractable !=null) OnExitInteractable();  
        }
    }

    public void DisableMovement()
    {
        canMove = false;
      //  InputSystem.DisableDevice(Keyboard.current);
    }

    public void EnableMovement()
    {
        canMove = true;
        //InputSystem.EnableDevice(Keyboard.current);
    }
}
