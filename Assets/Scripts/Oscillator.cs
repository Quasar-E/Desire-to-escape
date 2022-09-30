using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon)
            return;

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1) / 2;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
