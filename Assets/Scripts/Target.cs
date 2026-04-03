using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// IPointerClickHandler interface is used to detect pointer click events.
/// This is part of Unity's Event System.
/// it requires UI/EventSystem on scene, and the main camera should attach PhysicsRaycaster.
public class Target : MonoBehaviour, IPointerClickHandler
{
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    private Rigidbody rb;

    GameManager gameManager;
    public int point;
    public ParticleSystem explosionParticle;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        transform.position = RandomSpawnPos();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    // NOTE: OnPointerClick is part of IPointerClickHandler interface
    public void OnPointerClick(PointerEventData eventData)
    {
        gameManager.UpdateScore(point);
        var explosionFX = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Debug.Log("clicked");
        Destroy(this.gameObject);
        Destroy(explosionFX.gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            if (this.gameObject.CompareTag("Bad"))
            {
                Destroy(gameObject);
                return;
            }

            gameManager.UpdateScore(-point);
            Destroy(gameObject);
        }
    }
}
