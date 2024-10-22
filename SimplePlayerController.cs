using UnityEngine;

/// <summary>
/// A Script that handles input from the user to move/rotate the GameObject it's attached to
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    /// <summary>
    /// How much to move this GameObject forawrd/backwards along it's local Forward vector in a single frame
    /// </summary>
    float movement = 0;

    [SerializeField]
    float turnSpeed = 1f;

    /// <summary>
    /// How much to turn this GameObject left/right along it's local Y axis in a single frame
    /// </summary>
    float turnAmount = 0;

    /// <summary>
    /// Constaints to read the values from axis defined in the Input Manager
    /// </summary>
    const string k_HORIZONTAL = "Horizontal";
    const string k_VERTICAL = "Vertical";

    // Update is called once per frame
    void Update()
    {
        #region Movement
        //  Get the foat value from the Input Manager
        movement = Input.GetAxis(k_VERTICAL);

        // Move the object along its Forward vector based on input
        transform.Translate(0f, 0f, movement * moveSpeed * Time.deltaTime);
        #endregion

        #region Turning
        //  Get the float value from the Input Manager
        turnAmount = Input.GetAxis(k_HORIZONTAL);

        //  Rotate the object around its Y axis based on input
        transform.localRotation *= Quaternion.Euler(0f, turnAmount * turnSpeed * Time.deltaTime, 0f);
        #endregion
    }
}
