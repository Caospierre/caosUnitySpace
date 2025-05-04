using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [FormerlySerializedAs("_isGameOver")] [SerializeField]
        private bool isGameOver;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && isGameOver)
            {
                SceneManager.LoadScene(1); 
            }
        }

        public void GameOver()
        {
            isGameOver = true;
        }

    }
}
