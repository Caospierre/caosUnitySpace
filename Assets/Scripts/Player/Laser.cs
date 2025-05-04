using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Laser : MonoBehaviour
    {
        [FormerlySerializedAs("_laserSpeed")]
        [SerializeField]
        private float laserSpeed = 10f;


        void Update()
        {
            Vector3 laserDir = Vector3.up * (laserSpeed * Time.deltaTime);
            transform.Translate(laserDir);

            if (transform.position.y > 7.5f)
            {
                Transform parent = transform.parent;

                if (!ReferenceEquals(parent, null))
                {
                    Destroy(transform.parent.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
