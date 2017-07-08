using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// PlayerMouvement script requires thoses components and will be added if they aren't already there
[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private Vector3 maxVelocity = new Vector3(0.96f, 0, 0.96f);

    [SerializeField]
    private float rotationSpeed = 120.0f;

    // Modifiers are a percentage of how much of the movement must be executed, NOT how much isn't executed
    [SerializeField]
    private float sideStepModifier = 1.08f;

    // Modifiers are a percentage of how much of the movement must be executed, NOT how much isn't executed
    [SerializeField]
    private float backwardModifier = 0.9f;

    [SerializeField]
    private float acceleration = 13.0f;

    [SerializeField]
    private float decceleration = 11.0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    
    public void AddLocalVelocity(Vector3 addedVelocity)
    {
        velocity = velocity + addedVelocity;
    }
    
    public void NullifyLocalVelocity()
    {
        velocity = Vector3.zero;
    }

    public void MoveCharacter(Hashtable inputs, bool debug = false)
    {
        rotateCharacterWithInput(inputs);

        // Calculate modifiers that will affect the movement
        Dictionary<string, float> modifiers = CalculateModifiers(inputs);

        CalculateLocalYVelocity();
        CalculateLocalZVelocity(inputs, modifiers);
        CalculateLocalXVelocity(inputs, modifiers);

        Vector3 convertedVelpocity = ConvertToGlobalVelocity(velocity)*Time.deltaTime;

        controller.Move(convertedVelpocity);
        
        // Draw debug lines
        if (debug) DrawVector3AtCharacterPosAndGroundLevel(convertedVelpocity / Time.deltaTime, Color.red, false, true);
    }

    private void rotateCharacterWithInput(Hashtable inputs)
    {
        float rotY = (float)inputs["xAxis"] * rotationSpeed * Time.deltaTime;
        
        // Rotate around the global Y axis
        transform.rotation *= Quaternion.AngleAxis(rotY, Vector3.up);
    }
    
    private Dictionary<string, float> CalculateModifiers(Hashtable inputs)
    {
        // Set the base modifiers values
        float currentBackwardModifier = 1;
        float currentSideStepModifier = 1;
        
        // Calculate the modifiers
        if (Mathf.Sign((float)inputs["verticalInput"]) == -1) currentBackwardModifier = backwardModifier;
        if ((float)inputs["horizontalInput"] != 0) currentSideStepModifier = sideStepModifier;
        
        // Create a dictionnary of the calculated modifiers and returns it
        Dictionary<string, float> modifiers = new Dictionary<string, float>();

        modifiers.Add("backwardModifier", currentBackwardModifier);
        modifiers.Add("sideStepModifier", currentSideStepModifier);
        
        return modifiers;
    }
    
    private void CalculateLocalYVelocity()
    {
        // Increments the effect of the gravity on the character fall
        velocity.y -= GameManager.GRAVITY * Time.deltaTime;
    }
    
    private void CalculateLocalZVelocity(Hashtable inputs, Dictionary<string, float> modifiers)
    {
        // Prepare the necessary variables
        float previousVelocityZ = velocity.z;
        float currentMaxVelocityZ = maxVelocity.z * (float)inputs["verticalInput"] * modifiers["backwardModifier"];

        // If the character moves without having achived is maximum vertical velocity
        if ((float)inputs["verticalInput"] != 0 && previousVelocityZ != currentMaxVelocityZ)
        {
            // Increment the vertical velocity 
            velocity.z += Mathf.Abs((float)inputs["verticalInput"]) * acceleration * Mathf.Sign(currentMaxVelocityZ - velocity.z) * modifiers["backwardModifier"] * Time.deltaTime;

            // Adjusts the vertical velocity if necessary
            if ((previousVelocityZ < currentMaxVelocityZ && velocity.z > currentMaxVelocityZ) ||
                (previousVelocityZ > currentMaxVelocityZ && velocity.z < currentMaxVelocityZ))
            {
                velocity.z = currentMaxVelocityZ;
            }
        }
        // If the character doesn't want to move, but didn't loose all is vertical velocity
        else if ((float)inputs["verticalInput"] == 0 && controller.isGrounded && previousVelocityZ != 0)
        {
            // Reduce the vertical velocity
            velocity.z -= decceleration * Mathf.Sign(previousVelocityZ) * Time.deltaTime;

            // Adjusts the vertical velocity if necessary
            if (-Mathf.Sign(previousVelocityZ) * velocity.z > 0) velocity.z = 0;
        }
    }

    private void CalculateLocalXVelocity(Hashtable inputs, Dictionary<string, float> modifiers)
    {
        // Prepare the necessary variables
        float previousVelocityX = velocity.x;
        float currentMaxVelocityX = maxVelocity.x * (float)inputs["horizontalInput"] * modifiers["sideStepModifier"];

        // If the character moves without having achived is maximum horizontal velocity
        if ((float)inputs["horizontalInput"] != 0 && previousVelocityX != currentMaxVelocityX)
        {
            // Increment the vertical velocity
            velocity.x += Mathf.Abs((float)inputs["horizontalInput"]) * acceleration * Mathf.Sign(currentMaxVelocityX - velocity.x) * modifiers["sideStepModifier"] * Time.deltaTime;

            // Adjusts the horizontal velocity if necessary
            if ((previousVelocityX < currentMaxVelocityX && velocity.x > currentMaxVelocityX) ||
                (previousVelocityX > currentMaxVelocityX && velocity.x < currentMaxVelocityX))
            {
                velocity.x = currentMaxVelocityX;
            }
        }
        // If the character doesn't want to move, but didn't loose all is horizontal velocity
        else if ((float)inputs["horizontalInput"] == 0 && controller.isGrounded && previousVelocityX != 0)
        {
            // Reduce the horizontal velocity
            velocity.x -= decceleration * Mathf.Sign(previousVelocityX) * Time.deltaTime;

            // Adjusts the horizontal velocity if necessary
            if (-Mathf.Sign(previousVelocityX) * velocity.x > 0) velocity.x = 0;
        }
    }

    private Vector3 ConvertToGlobalVelocity(Vector3 toConvert)
    {
        return transform.TransformDirection(toConvert);
    }

    // Draw the Vector3 as a line starting from the character transform at ground level
    private void DrawVector3AtCharacterPosAndGroundLevel(Vector3 vector3, Color color, bool ignoreXaxis = false, bool ignoreYaxis = false, bool ignoreZaxis = false)
    {
        Vector3 basePos = transform.position - new Vector3(0, transform.position.y, 0);

        float usedXcomponent = vector3.x + transform.position.x;
        float usedYcomponent = vector3.y;
        float usedZcomponent = vector3.z + transform.position.z;

        if (ignoreXaxis) usedXcomponent -= vector3.x;
        if (ignoreYaxis) usedYcomponent -= vector3.y;
        if (ignoreZaxis) usedZcomponent -= vector3.z;

        Debug.DrawLine(basePos, new Vector3(usedXcomponent, usedYcomponent, usedZcomponent), color);
    }
}
