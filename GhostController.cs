using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;
    public float randomMovementIntensity = 0.5f;
    public float randomChangeInterval = 1.5f;

    private Vector3 randomOffset;
    private float randomTimer = 0f;

    void Update()
    {
        randomTimer += Time.deltaTime;
        if (randomTimer >= randomChangeInterval)
        {
            randomOffset = new Vector3(
                Random.Range(-randomMovementIntensity, randomMovementIntensity),
                0,
                Random.Range(-randomMovementIntensity, randomMovementIntensity)
            );
            randomTimer = 0f;
        }

        Vector3 direction = (player.position - transform.position).normalized + randomOffset;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
