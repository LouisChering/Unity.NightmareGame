using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public variables
        public float speed = 6.0f;
        public LayerMask floorMask;

    #endregion

    #region Private variables
        Vector3 movement;
        Animator animator;
        float cameraRaycastLength = 100.0f;
        Rigidbody playerRigidBody;
        float horizontalInput,verticalInput;
    #endregion

    void Awake(){
        if(animator == null) animator = GetComponent<Animator>();
        if(playerRigidBody == null) playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        GetControllerInput();
        Move(verticalInput,horizontalInput);
        Turn();
        Animating(horizontalInput, verticalInput);
    }
    
    void GetControllerInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void Move(float vertical,float horizontal){
        movement.Set(horizontal,0.0f,vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
      
    }

    void Turn(){
        Ray cameraRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(cameraRaycast, out floorHit, cameraRaycastLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float horizontal,float vertical)
    {
        var moving = horizontal != 0 || vertical != 0;
        animator.SetBool(PlayerAnimationVariables.IsWalking.ToString(), moving);
    }
}

public enum PlayerAnimationVariables
{
    IsWalking
}