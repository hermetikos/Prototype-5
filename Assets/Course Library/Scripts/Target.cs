using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue = 5;
    public float minSpeed = 12;
    public float maxSpeed = 16;
    public float maxTorque = 10;
    public float xRange = 4;
    public float ySpawnPos = -6;
    public ParticleSystem explosionParticle;
    private Rigidbody targetRigidbody;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        targetRigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        targetRigidbody.AddTorque(
            RandomTorque(),
            RandomTorque(),
            RandomTorque(),
            ForceMode.Impulse
        );
        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
    }

    Vector3 RandomForce() {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque() {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPosition() {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown() {
        if(gameManager.isGameActive)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad")) {
            gameManager.GameOver();
        }
    }

    
}
