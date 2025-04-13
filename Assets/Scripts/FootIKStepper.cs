using UnityEngine;

public class FootIKStepper : MonoBehaviour
{
    public Transform body; // Assign the hip or main body
    public float stepDistance = 1f;
    public float stepSpeed = 5f;

    private Vector3 defaultOffset;

    void Start()
    {
        defaultOffset = transform.position - body.position;
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        // Offset the foot forward/backward relative to body
        Vector3 targetPos = body.position + defaultOffset + new Vector3(input * stepDistance, 0, 0);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * stepSpeed);
    }
}
