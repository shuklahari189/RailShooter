using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    #region Variables

    float Xoffset;
    float Yoffset;
    float Xthrow;
    float Ythrow;
    

    [Header("Required")]
    public GameObject[] Guns;

    bool IsDestroyed;

    [Header("Constrains & speed")]
    public float Xrange;
    public float YdRange;
    public float YuRange;
    public float Speed;

    [Header("Rotation Factors")]
    public float MaxPitch;
    public float MaxYaw;
    public float YawCfactor;
    public float pitchCfactor;
    public float RollCfactor;

    #endregion

    BoxCollider bx;
    MeshRenderer mR;

    private void Start()
    {
        bx = GetComponent<BoxCollider>();
        mR = GetComponent<MeshRenderer>();
    }

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
        mR.enabled = false;
        bx.enabled = false;
        Fire(false);
        Invoke("LoadSameLevel", 1f);
    }

    void LoadSameLevel()
    {
        SceneManager.LoadScene(1);
    }

    void HandleFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            Fire(true);
        }
        else
        {
            Fire(false);
        }
    }

    void Fire(bool GunsActive)
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
        float clampedYoff = Mathf.Clamp(rawYpos, YdRange, YuRange);

        transform.localPosition = new Vector3(clampedXoff, transform.localPosition.y, transform.localPosition.z);

        transform.localPosition = new Vector3(transform.localPosition.x, clampedYoff, transform.localPosition.z);
    }
}
