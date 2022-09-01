using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 7f;
    public float gravity = -9.81f;
    public float jumpForce = 2f;
    public float turnTime = 0.15f;
    float turnVelocity;
    
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform cam;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask, stoneMask, grassMask, dirtMask, metalMask;
    
    public GameObject namebox;
    bool nameActive;
    public GameObject objectBox;
    bool objectBoxActive;
    

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        namebox.SetActive(false);
        nameActive = false;
        objectBox.SetActive(false);
        objectBoxActive = false;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0f, z).normalized;//transform.right * x + transform.forward *z;

        if (move.magnitude >= 0.1f)
        {
            float tarAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;


            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tarAngle, ref turnVelocity, turnTime); // smooths the turn
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, tarAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        }

        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!nameActive)
            { 
                namebox.SetActive(true);
                nameActive = true;
            }
            else
            {
                namebox.SetActive(false);
                nameActive = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(!objectBoxActive)
            { 
                objectBox.SetActive(true);
                objectBoxActive = true;
            }
            else
            {
                objectBox.SetActive(false);
                objectBoxActive = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(1);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
