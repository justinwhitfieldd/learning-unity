using UnityEngine;

public class ArmSetup : MonoBehaviour
{
    public bool isLeftArm = false; // Flag to determine if it's the left arm
    public float flapSpeed = 0.2f; // Speed of the flapping motion
    public float flapHeight = 10.0f; // The maximum angle for flapping

    void Update()
    {
        // Simple flapping motion
        float angle = Mathf.Sin(Time.time * flapSpeed) * flapHeight;

        // If it's the left arm, we need to invert the angle for mirroring effect
        if (isLeftArm)
        {
            angle *= -1;
        }

        // Apply the rotation to the upper arm for flapping
        transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
