using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Member Variables

    [SerializeField] float pSpeed;
    [SerializeField] float rotationSpeed;
    Vector2 input;
    float rotationInput;
    //Animator animator;
    Rigidbody rb;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        StaticJeff.controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        input = StaticJeff.controls.Standard.Movement.ReadValue<Vector2>();
        rotationInput = StaticJeff.controls.Standard.Camera.ReadValue<float>() * rotationSpeed * Time.deltaTime;

        //animator.SetFloat("Speed", input.magnitude);

    }

    private void FixedUpdate()
    {
        var newInput = GetCameraBasedInput(input, Camera.main);
        


        var newVelocity = new Vector3(newInput.x * pSpeed, rb.velocity.y, newInput.z * pSpeed);

        rb.velocity = newVelocity;

        if (newVelocity != Vector3.zero)
        {
            RotatePlayerModel(newVelocity);
        }

        if (rotationInput != 0)
        {
            gameObject.transform.Rotate(Vector3.up, rotationInput, Space.World);
        }
    }

    Vector3 GetCameraBasedInput(Vector2 input, Camera cam)
    {
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return input.x * camRight + input.y * camForward;
    }

    private void RotatePlayerModel(Vector3 dir)
    {
        dir.y = 0;
        //animator.transform.forward = dir;
    }


    public void OnSneak()
    {
        rb.velocity *= 0.5f;
        //animator.SetBool("Crouching", true);
    }


}
