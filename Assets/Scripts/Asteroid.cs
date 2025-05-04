using Enemy;
using UnityEngine;
using UnityEngine.Serialization;

public class Asteroid : MonoBehaviour
{
    [FormerlySerializedAs("_rotateSpeed")]
    [SerializeField]
    private float rotateSpeed = 19.0f;
    [FormerlySerializedAs("_explosionPrefab")] 
    [SerializeField]
    private GameObject explosionPrefab;
    private SpawnManager _spawnManager;

    private readonly string _laserTag= "Laser";
    private readonly string _spawnManagerName= "Spawn_Manager";

    void Start()
    {
        _spawnManager = GameObject.Find(_spawnManagerName).GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_laserTag))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(gameObject, 0.25f);
        }


    }

}
