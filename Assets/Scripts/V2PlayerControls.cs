using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class V2PlayerControls : MonoBehaviour
{
    private PlayerControls playerControls;
    public GameObject playerActions;
    SplashDialogueManager dialogueManager;
    public HealthManager htm;
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
    public bool actionsEnabled;
    #region Mask Variables
    
    public GameObject venetianMaskUI;
    public GameObject ballroomMaskUI;
    public GameObject carnavalMaskUI;
    
    public GameObject mask4UI;
    public GameObject mask5UI;
    public GameObject mask6UI;
    public GameObject mask7UI;
    public GameObject mask8UI;
    public GameObject mask9UI;
    public GameObject mask10UI;
    #endregion
    #region Mask Functionality Variables
    public GameObject[] hiddenObjects; //ballroom
    public float boostedATK; //venetian
    public float standardATK; //venetian
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
        playerControls.PlayerActions.Sprint.performed += x => SprintPressed();
        playerControls.PlayerActions.Sprint.canceled += x => SprintReleased();
        #endregion
        
        dialogueManager = GameObject.Find("Canvas").GetComponent<SplashDialogueManager>();

        hiddenObjects = GameObject.FindGameObjectsWithTag("HiddenObject");
        //standardATK = 8;
        //boostedATK = 16;        
        ResetMaskUI();
    }

    private void OnEnable()
    {
        //Debug.Log("Controls Enabled");
        actionsEnabled = true;
        playerControls.Enable();
        playerActions.SetActive(true);
    }
    private void OnDisable()
    {
        actionsEnabled = false;
        playerControls.Disable();
        playerActions.SetActive(false);
    }

    private void Update()
    {
        #region movement stuff
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        if (!isSprinting)
        {    
            anim.SetFloat("vertical", Mathf.Abs(Input.GetAxis("Vertical")));
        }
        else
        {
            anim.SetFloat("vertical", 2);
        }
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        #endregion    

        if(Input.GetKeyDown(KeyCode.L))
        {
            SwitchMasktoVenetian();
        }   
        if(Input.GetKeyDown(KeyCode.K))
        {
            SwitchMasktoBallroom();
        }   
        if(Input.GetKeyDown(KeyCode.J))
        {
            ResetMaskUI();
        }
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

//MASKS
    #region Mask Functionality
    public void SwitchMasktoVenetian()
    {
        Debug.Log("Venetian Mask Equipped");
        ResetMaskUI();
        venetianMaskUI.SetActive(true);
        
        //functionality
        htm.attack = boostedATK;  

    }
    public void SwitchMasktoBallroom()
    {
        Debug.Log("Ballroom Mask Equipped");
        ResetMaskUI();
        ballroomMaskUI.SetActive(true);

        //functionality
        foreach (GameObject ho in hiddenObjects)
        {
            ho.SetActive(true);
        }
    }
    public void ResetMaskUI()
    {
        Debug.Log("Removing Mask"); 
        
        venetianMaskUI.SetActive(false);
        htm.attack = standardATK;
        ballroomMaskUI.SetActive(false);
        foreach (GameObject ho in hiddenObjects)
        {
            ho.SetActive(false);
        }
        carnavalMaskUI.SetActive(false);        
        mask4UI.SetActive(false);
        mask5UI.SetActive(false);
        mask6UI.SetActive(false);
        mask7UI.SetActive(false);
        mask8UI.SetActive(false);
        mask9UI.SetActive(false);
        mask10UI.SetActive(false);
    }
    
    #endregion

}
