using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    private CharacterController myCharacterController;
    private float horizontalInput, verticalInput;
    private float moveSpeed = 7f;
    private Vector3 playerInput;
    private Vector3 playerMovement;

    private float jumpHeight = 0.7f;
    private float gravity =0.06f;
    private bool jump = false;

    private float fallSpeed = 1.6f;

    private float stayToGround = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate() //if we work with Physics
    {
        PlayerJump();

        PlayerMove();

        RotatePlayerToMouse();

    }

    private void PlayerInput()
    {
        
        horizontalInput = Input.GetAxis("Horizontal"); //Edit->project settings
        verticalInput = Input.GetAxis("Vertical"); //Edit->project settings
        playerInput = new Vector3(horizontalInput, 0, verticalInput).normalized;//when moving diagonally, speed was higher, so we need to normalize

        if(!jump && Input.GetButtonDown("Jump") && myCharacterController.isGrounded) //Space button in project setting-input manager-Axis/jump only when on ground
        {
            jump = true;
        }
    }

    private void PlayerJump()
    {
        if(!myCharacterController.isGrounded) // if character is above ground
        {
            if(playerMovement.y>0)//character going upwards
                playerMovement.y -=gravity;
            else
                playerMovement.y -=gravity * fallSpeed; //character going down faster/heavier 


        }
        else
        {
            playerMovement.y -= stayToGround; //stop when it touches the ground (there is a bug and the characher is not considered grounded)
        }

        if(jump == true)
        {
            playerMovement.y = jumpHeight;
            jump = false;
        }

    }

    private void PlayerMove()
    {
        playerMovement.z = playerInput.z * moveSpeed * Time.deltaTime; //taxitita statheri aneksartita apo to poso grigoro einai to pc sto opoio ekteleitai
        playerMovement.x = playerInput.x * moveSpeed * Time.deltaTime; //taxitita statheri aneksartita apo to poso grigoro einai to pc sto opoio ekteleitai

       // if(playerInput != Vector3.zero) //the eye always looks in front
       // {
       //     transform.forward = playerInput; //Vector3.Slerp gia na mi strivei apotoma, alla thelei ta rotations
       // }
        myCharacterController.Move(playerMovement);

    }

    private void RotatePlayerToMouse()
    {
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//position of mouse in game world, will not use this now

        Vector3 mousePosition = Input.mousePosition; //mouse position in pixels         //0,0 is in left lower corner in pixels of the window of the game
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);//get players position from game world and translate it to pixel
        Vector3 direction = mousePosition - playerPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //tokso efaptomenis se aktinia, to metatrepoume se moires
        transform.rotation = Quaternion.AngleAxis(-angle + 90f,Vector3.up); //x right y up z forward

    }
}
