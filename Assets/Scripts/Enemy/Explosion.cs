using UnityEngine;

namespace Enemy
{
    public class Explosion : MonoBehaviour
    {

        void Start()
        {
            Destroy(gameObject, 3.0f);
        }
    }
}
