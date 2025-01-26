using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0f;  // Duration of the shake
    public float shakeMagnitude = 0.1f;  // Magnitude of the shake (how much it moves)
    public float shakeFrequency = 2f;  // Frequency of the shake (speed of the shake)

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            // Apply the shake by changing the camera position
            float shakeAmountX = Random.Range(-1f, 1f) * shakeMagnitude;
            float shakeAmountY = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + shakeAmountX, originalPosition.y + shakeAmountY, originalPosition.z);

            // Decrease the duration of the shake
            shakeDuration -= Time.deltaTime * shakeFrequency;
        }
        else
        {
            // Reset the camera position when shake is over
            transform.localPosition = originalPosition;
        }
    }

    // Call this method to start the shake effect
    public void ShakeCamera(float duration, float magnitude, float frequency)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeFrequency = frequency;
    }
}
