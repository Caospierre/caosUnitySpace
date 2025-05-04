using System.Collections;
using Enemy;
using MainMenu;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private float _speed;
        [FormerlySerializedAs("_laserPrefab")]
        [SerializeField]
        private GameObject laserPrefab;
        [FormerlySerializedAs("_shieldsVisuals")] 
        [SerializeField]
        private GameObject shieldsVisuals;
        [FormerlySerializedAs("_tripleShotPrefab")] 
        [SerializeField]
        private GameObject tripleShotPrefab;
        private readonly float _fireRate = 0.2f;
        private float _canFire;
        private int _lives = 3;
        private SpawnManager _spawnManager;
        private bool _isTripleShotActive;
        private bool _isSpeedBoostActive;
        private bool _isShieldActive;
        private int _score;
        [FormerlySerializedAs("_rEngine")]
        [SerializeField]
        private GameObject rEngine;
        [FormerlySerializedAs("_lEngine")] 
        [SerializeField]
        private GameObject lEngine;
        [FormerlySerializedAs("_laserAudio")]
        [SerializeField]
        private AudioClip laserAudio;
        private AudioSource _laserSrc;
        private AudioSource _explSrc;
        private AudioSource _powerUpSrc;
        private UIManager _uiManager;
        private readonly string _canvasName="Canvas";
        private readonly string _spawnManagerName="Spawn_Manager";
        private readonly string _horizontalName="Horizontal";
        private readonly string _verticalName="Vertical";
    

        void Start()
        {
            transform.position = new Vector3(0, 0, 0);
            _uiManager = GameObject.Find(_canvasName).GetComponent<UIManager>();
            _spawnManager = GameObject.Find(_spawnManagerName).GetComponent<SpawnManager>();
            _laserSrc = GetComponent<AudioSource>();
            _explSrc = GetComponent<AudioSource>();
            if (_uiManager != null && _spawnManager != null && _laserSrc != null)
            {
                _laserSrc.clip = laserAudio;
            }

        
        }

        void Update()
        {
            ShipMovement();
            Boundaries();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                FireLaser();
            }

        }

        void Boundaries()
        {
            if (transform.position.y >= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }
            else if (transform.position.y <= -3.4f)
            {
                transform.position = new Vector3(transform.position.x, -3.4f, 0);
            }
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.4f, 0), 0);
            if (transform.position.x >= 11.25)
            {
                transform.position = new Vector3(-11.25f, transform.position.y, 0);
            }
            else if (transform.position.x <= -11.25)
            {
                transform.position = new Vector3(11.25f, transform.position.y, 0);
            }
        }


        void ShipMovement()
        {
            float horizontalInput = Input.GetAxis(_horizontalName);
            float verticalInput = Input.GetAxis(_verticalName);
            Vector3 dirMovement = new Vector3(horizontalInput, verticalInput, 0);
            _speed = 5.0f;
            if (_isSpeedBoostActive)
            {
                _speed = 9.0f;
            }
            transform.Translate(dirMovement * (_speed * Time.deltaTime));


        }


        void FireLaser()
        {
            Vector3 offsetY = new Vector3(0, 1.05f, 0);
        
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + offsetY, Quaternion.identity);
            }

            _laserSrc.Play();
        }

        public void Damage()
        {

            if (_isShieldActive)
            {
                _isShieldActive = false;
                shieldsVisuals.SetActive(false); 
                return;
            }

            _lives --;

            _uiManager.UpdateLives(_lives);

            if (_lives == 2)
            {
                rEngine.SetActive(true);
            }
            else if (_lives == 1)
            {
                lEngine.SetActive(true);
            } 
            else if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                _explSrc.Play();
                Destroy(this.gameObject);
            }
        }

        public void TripleShotActive()
        {
            _isTripleShotActive = true;
            StartCoroutine(TripleShotPowerDownRoutine());

        }

        IEnumerator TripleShotPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }

        public void SpeedPowerUpActive()
        {
            _isSpeedBoostActive = true;
            StartCoroutine(SpeedBoostDownRoutine());

        }

        IEnumerator SpeedBoostDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedBoostActive = false;
        }

        public void ShieldsActive()
        {
            _isShieldActive = true;
            shieldsVisuals.SetActive(true);  
        }

        public void AddScore(int points)
        {
            _score += points;
            _uiManager.UpdateScore(_score);
        }

    }
}
