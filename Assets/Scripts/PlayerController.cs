using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Member Variables

    [SerializeField] float pSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject FollowTarget;
    Vector2 input;
    float rotationInput;
    public bool Sneaking = false;
    Animator animator;
    Rigidbody rb;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        StaticJeff.controls.Enable();
        StaticJeff.controls.Standard.Sneak.performed += Sneak_performed;
        StaticJeff.controls.Standard.Sneak.canceled += Sneak_canceled;
    }

    private void OnDisable()
    {
        StaticJeff.controls.Disable();
        StaticJeff.controls.Standard.Sneak.performed -= Sneak_performed;
        StaticJeff.controls.Standard.Sneak.canceled -= Sneak_canceled;
    }

    // Update is called once per frame
    void Update()
    {
        input = StaticJeff.controls.Standard.Movement.ReadValue<Vector2>();
        rotationInput = StaticJeff.controls.Standard.Camera.ReadValue<float>() * rotationSpeed * Time.deltaTime;

        animator.SetFloat("Speed", input.magnitude);

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
        if (dir != Vector3.zero)
        {
            animator.transform.forward = dir;
        }
    }

    private void Sneak_performed(InputAction.CallbackContext obj)
    {
        pSpeed *= 0.75f;
        FollowTarget.transform.position = new Vector3(FollowTarget.transform.position.x, 1.4f, FollowTarget.transform.position.z);
        Sneaking = true;
        animator.SetBool("Crouching", true);
    }
    private void Sneak_canceled(InputAction.CallbackContext obj)
    {
        pSpeed *= (1f/.75f);
        FollowTarget.transform.position = new Vector3(FollowTarget.transform.position.x, 2f, FollowTarget.transform.position.z);
        Sneaking = false;
        animator.SetBool("Crouching", false);
    }

    public void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == 6)
        {
            text.text = "You've been caught!";
            pSpeed = 0;
            //play snake sound clip here
            StartCoroutine(RestartLevel());
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StealthScene01");
    }
}
