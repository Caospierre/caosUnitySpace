using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PowerUp : MonoBehaviour
    {

        [FormerlySerializedAs("_powerUpSpeed")]
        [SerializeField]
        private float powerUpSpeed = 3.0f;
        [FormerlySerializedAs("_powerUpID")] 
        [SerializeField] 
        private int powerUpID;
        [FormerlySerializedAs("_pwrUpClip")] 
        [SerializeField]
        private AudioClip pwrUpClip;

        private readonly string _playerTag = "Player";

        void Update()
        {
            Vector3 powUpMovement = Vector3.down * (powerUpSpeed * Time.deltaTime);
            transform.Translate(powUpMovement);
            if (transform.position.y <= -7.5f)
            {
                Destroy(gameObject);
            }
        
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(_playerTag))
            {
                global::Player.Player player = other.transform.GetComponent<global::Player.Player>();
                AudioSource.PlayClipAtPoint(pwrUpClip, transform.position);
                if (player != null)
                {
                    switch (powerUpID)
                    {
                        case 0:
                            player.TripleShotActive();
                            break;
                        case 1:
                            player.SpeedPowerUpActive();
                            break;
                        case 2:
                            player.ShieldsActive();
                            break;
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
