using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    Vector3 movement = Vector3.zero;

    const string k_HORIZONTAL = "Horizontal";
    const string k_VERTICAL = "Vertical";

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float lookSpeed = 1f;

    [SerializeField]
    float MinimumX = -90F, MaximumX = 90F;

    [SerializeField]
    float lookSmoothTime = 5f;

    Vector2 looking = Vector2.zero;

    const string k_MOUSE_X = "Mouse X";
    const string k_MOUSE_Y = "Mouse Y";

    Quaternion newRootRotation, newCameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        movement.x = Input.GetAxis(k_HORIZONTAL);
        movement.z = Input.GetAxis(k_VERTICAL);

        if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            movement.y = 1f;
        }
        else if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            movement.y = -1f;
        }
        else
        {
            movement.y = 0f;
        }

        transform.Translate(movement * moveSpeed * Time.deltaTime);
        #endregion

        #region Looking
        looking.x = Input.GetAxis(k_MOUSE_Y);
        looking.y = Input.GetAxis(k_MOUSE_X);

        newRootRotation = transform.localRotation * Quaternion.Euler(0f, looking.y * lookSpeed, 0f);
        newCameraRotation = ClampRotationAroundXAxis(cameraTransform.localRotation * Quaternion.Euler(looking.x * lookSpeed, 0f, 0f));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, newRootRotation, lookSmoothTime * Time.deltaTime);
        cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, newCameraRotation, lookSmoothTime * Time.deltaTime);
        #endregion
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
