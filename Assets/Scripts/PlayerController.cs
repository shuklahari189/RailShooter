using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    #region Variables
    float Xoffset;
    float Yoffset;
    float Xthrow;
    float Ythrow;
    #endregion

    [Header("Required")]
    public GameObject[] Guns;

    bool IsDestroyed;

    [Header("Constrains & speed")]
    public float Xrange;
    public float Yrange;
    public float Speed;

    [Header("Rotation Factors")]
    public float MaxPitch;
    public float MaxYaw;
    public float YawCfactor;
    public float pitchCfactor;
    public float RollCfactor;

    void Update()
    {
        if (!IsDestroyed) 
        {
            HandleMovement();
            HandleRotation();
            HandleFiring();
        }

    }

    void Collided()
    {
        IsDestroyed = true;
    }

    void HandleFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    void SetGunsActive(bool GunsActive)
    {
        foreach(GameObject gun in Guns)
        {
            var guns_P = gun.GetComponent<ParticleSystem>().emission;
            guns_P.enabled = GunsActive;
        }
    }

    private void HandleRotation()
    {
        float pitch;
        float yaw;
        float roll;

        float PitchFactor = transform.localPosition.y * MaxPitch;
        pitch = PitchFactor + Ythrow  * pitchCfactor;

        float YawFactor = transform.localPosition.x * MaxYaw;
        yaw = YawFactor + Xthrow * YawCfactor;

        roll = Xthrow * RollCfactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void HandleMovement()
    {
        Xthrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Ythrow = CrossPlatformInputManager.GetAxis("Vertical");

        Xoffset = Time.deltaTime * Xthrow * Speed;
        float rawXoff = transform.localPosition.x + Xoffset;
        float clampedXoff = Mathf.Clamp(rawXoff, -Xrange, Xrange);

        Yoffset = Time.deltaTime * Ythrow * Speed;
        float rawYpos = transform.localPosition.y + Yoffset;
        float clampedYoff = Mathf.Clamp(rawYpos, -Yrange, Yrange);

        transform.localPosition = new Vector3(clampedXoff, transform.localPosition.y, transform.localPosition.z);

        transform.localPosition = new Vector3(transform.localPosition.x, clampedYoff, transform.localPosition.z);
    }
}
