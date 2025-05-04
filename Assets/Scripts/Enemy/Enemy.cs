using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [FormerlySerializedAs("_enemySpeed")] 
        [SerializeField]
        private float enemySpeed = 4.0f;
        private Player.Player _player;
        private Animator _animator;
        private AudioSource _explSrc;
        private bool _isHit;

        private readonly string _onEnemyDeath = "OnEnemyDeath";
        private readonly string _laserTag = "Laser";
        private readonly string _playerTag = "Player";

        void Start()
        {
            _explSrc = GetComponent<AudioSource>();
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
            _player = GameObject.Find(_playerTag).GetComponent<Player.Player>();
            _animator = GetComponent<Animator>();
        }
        void Update()
        {
            Vector3 enemyMovement = Vector3.down * (enemySpeed * Time.deltaTime);
            transform.Translate(enemyMovement);
            Vector3 respawnTop = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
            if (transform.position.y <= -5.5f)
            {
                transform.position = respawnTop;
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isHit == false)
            {
                _isHit = true;

                Player.Player player = other.transform.GetComponent<Player.Player>();
                if (other.CompareTag(_playerTag))
                {
                    if (player != null)
                    {
                        player.Damage();
                    }

                    _animator.SetTrigger(_onEnemyDeath);
                    enemySpeed = 0;
                    _explSrc.Play();
                    Destroy(this.gameObject, 2.5f);
                }

                if (other.CompareTag(_laserTag))
                {
                    Destroy(other.gameObject);
                    if (_player != null)
                    {
                        _player.AddScore(Random.Range(5, 25));
                    }

                    _animator.SetTrigger(_onEnemyDeath);
                    enemySpeed = 0;
                    _explSrc.Play();
                    Destroy(gameObject, 2.5f);
                }
            }

        }
    }
}
