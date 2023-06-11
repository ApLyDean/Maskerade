using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class V2PlayerControls : MonoBehaviour
{
    private PlayerControls playerControls;
    private DialogueManager dialogueManager;
    public Animator anim;
    #region Movement Variables
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;
    [SerializeField] private float speed;
    public float walkSpeed;
    public float sprintSpeed;
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    [SerializeField] private float jumpPower;
    public bool isSprinting;
    #endregion


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        #region movement stuff
        walkSpeed = speed;
        sprintSpeed = speed * 1.75f;
        playerControls = new PlayerControls();
        OnEnable();
        playerControls.PlayerMovement.Sprint.performed += x => SprintPressed();
        playerControls.PlayerMovement.Sprint.canceled += x => SprintReleased();
        #endregion
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        #region movement stuff
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        if (!isSprinting)
        {
            anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        }
        else
        {
            anim.SetFloat("vertical", 2);
        }
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        #endregion
    }

    public void ActivateDialogue()
    {
        dialogueManager.StartDialogue();
    }

    #region Movement Functions
    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            anim.SetBool("isInteracting", false);
            _velocity = -1.0f;
        }
        else
        { 
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
        
        _direction.y = _velocity;
    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0)
        {
            return;
        }
        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        if (!IsGrounded())
        {
            return;
        }
        anim.SetBool("isInteracting", true);
        _velocity += jumpPower;
    }

    public void SprintPressed()
    {        
        if (IsGrounded())
        {
            Debug.Log("sprinting");
            speed = sprintSpeed;
            isSprinting = true;
            
        }
    }     
    
    public void SprintReleased()
    {        
        Debug.Log("sprint over");
        speed = walkSpeed;        
        isSprinting = false;
    }    

    private bool IsGrounded() => _characterController.isGrounded; 

    #endregion


}