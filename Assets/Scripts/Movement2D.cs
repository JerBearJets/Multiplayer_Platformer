using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movement2D : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector2 _input;
    private Vector2 _direction;
    private float _velocity;
    private float _gravity= -9.0f;
    [SerializeField] private float gravitymultiplier = 3.0f;
    
    [SerializeField] private float speed;
    [SerializeField] private Movement movement;
    [SerializeField] private float jumpPower;
    private int numberOfJumps;
    [SerializeField] private int maxJumps = 2;

    // Start is called before the first frame update
    void Awake()
    {
    _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Applygrav();  
        ApplyMove();
    }

 private void ApplyMove()
    {
        var TargetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed =Mathf.MoveTowards(movement.currentSpeed, TargetSpeed, movement.acceleration* Time.deltaTime);
        _characterController.Move(_direction *movement.currentSpeed* Time.deltaTime);
    
    }
     private void Applygrav()
    {
     if (IsGrounded() && _velocity < 0.0f) {

        _velocity = 0.0f;

     }
     else {
     _velocity += _gravity * gravitymultiplier * Time.deltaTime;

     }

     _direction.y = _velocity;

    }
        public void Move(InputAction.CallbackContext context) 
    {
 
        _input= context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);

    }
    public void Jump(InputAction.CallbackContext context)
    {   
  
        if (!context.started) return;
        if (!IsGrounded() && numberOfJumps>= maxJumps) return;
        if (numberOfJumps ==0) StartCoroutine(WaitForLanding());

        numberOfJumps++;
        _velocity=0;
        _velocity +=jumpPower;
        //_velocity = jumpPower / numberOfJumps;
        

    }
    private IEnumerator WaitForLanding()
    {
       yield return new WaitUntil(() => !IsGrounded());
       yield return new WaitUntil(IsGrounded);
       numberOfJumps=0;

    }
    private bool IsGrounded() =>_characterController.isGrounded;
        [System.Serializable]
    public struct Movement
    {
        public float speed;
        public float multiplier;
        public float acceleration;
        [HideInInspector] public bool isSprinting;
        [HideInInspector] public float currentSpeed;

    }

}
