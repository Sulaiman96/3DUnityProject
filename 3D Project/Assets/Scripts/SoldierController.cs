using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float transitionTime = 1;
    public bool isSprinting;
    public float jumpHeight;
    public bool isFalling = false;

    private float speed;
    private float dropRate = 3f;
    private float velocitySpeed = 120; //if the player is sprinting and jumps, he can jump further.

    Rigidbody rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        var x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(x, 0, z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            speed = runSpeed;
        }
        else if (speed != walkSpeed)
        {
            isSprinting = false;
            speed = walkSpeed;
        }
        #endregion

        #region Jumping
        if (Input.GetButtonDown("Jump") && !isFalling)
        {
            isFalling = true;
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            anim.SetTrigger("Jump");

            if (isSprinting)
            {
                //Horizontal Velocity
                if (Input.GetAxis("Horizontal") > 0)
                    VelocityUpdate("Horizontal", velocitySpeed);
                else if (Input.GetAxis("Horizontal") < 0)
                    VelocityUpdate("Horizontal", -velocitySpeed);

                //Vertical Velocity
                if (Input.GetAxis("Vertical") > 0)
                    VelocityUpdate("Vertical", velocitySpeed);
                else if (Input.GetAxis("Vertical") < 0)
                    VelocityUpdate("Vertical", -velocitySpeed);
            }
        }

        if (isFalling)
            rb.AddForce(transform.up * -dropRate, ForceMode.Acceleration);
        #endregion

        #region Animation
        if (isSprinting)
        {
            AnimationUpdate("Horizontal", "Direction", 1);
            AnimationUpdate("Vertical", "Speed", 1);
        }
        else
        {
            AnimationUpdate("Horizontal", "Direction", 0.5f);
            AnimationUpdate("Vertical", "Speed", 0.5f);
        }

        anim.SetBool("isFalling", isFalling);
        #endregion
    }



    #region Functions
    private void AnimationUpdate(string axisName, string motionName, float motionSpeed)
    {
        if (Input.GetAxis(axisName) > 0)
        {
            anim.SetFloat(motionName, Mathf.Lerp(anim.GetFloat(motionName), motionSpeed, transitionTime * Time.deltaTime)); //Set the animation for positive values
        }
        else if (Input.GetAxis(axisName) < 0)
        {
            anim.SetFloat(motionName, Mathf.Lerp(anim.GetFloat(motionName), -motionSpeed, transitionTime * Time.deltaTime)); //Set the animation for negative values
        }
        else
        {
            anim.SetFloat(motionName, Mathf.Lerp(anim.GetFloat(motionName), 0, transitionTime * Time.deltaTime)); //Set the animation for idle
        }
    }

    private void VelocityUpdate(string axisName, float velocity)
    {
        if (axisName == "Vertical")
            rb.AddForce(transform.forward * velocity, ForceMode.Force);

        if (axisName == "Horizontal")
            rb.AddForce(transform.right * velocity, ForceMode.Force);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFalling)
            StartCoroutine(FallCheck());
    }

    private IEnumerator FallCheck()
    {
        yield return new WaitForSeconds(0.001f);

        isFalling = false;
        StopCoroutine(FallCheck());
    }

    #endregion
}
