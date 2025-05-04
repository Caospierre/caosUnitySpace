using System.Collections;
using Game;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MainMenu
{
    public class UIManager : MonoBehaviour
    {
        [FormerlySerializedAs("_scoreText")] [SerializeField]
        private Text scoreText;
        [FormerlySerializedAs("_livesImg")] [SerializeField]
        private Image livesImg;
        [FormerlySerializedAs("_liveSprites")] [SerializeField]
        private Sprite[] liveSprites;
        [FormerlySerializedAs("_gameOverText")] [SerializeField]
        private Text gameOverText;
        [FormerlySerializedAs("_restartLevelText")] [SerializeField]
        private Text restartLevelText;
        private GameManager _gameManager;
        private readonly string _scoreText="Score: ";
        private readonly string _gameManagerText="Game_Manager";
        private readonly string _gameOverText= "GAME OVER";
        private readonly string _emptyText= "";
        private readonly bool _hasIterate= true;
        private readonly float _delay= 0.75f;


        void Start()
        {
            _gameManager = GameObject.Find(_gameManagerText).GetComponent<GameManager>();
            scoreText.text =_scoreText  + 0;
            gameOverText.gameObject.SetActive(false);
        }

        public void UpdateScore(int playerScore)
        {
            scoreText.text = _scoreText + playerScore;
        }

        public void UpdateLives(int currentLives)
        {
            livesImg.sprite = liveSprites[currentLives];
            if (currentLives == 0)
            {
                GameOverSequence();
            }
        }

        void GameOverSequence()
        {
            _gameManager.GameOver();
            gameOverText.gameObject.SetActive(true);
            restartLevelText.gameObject.SetActive(true);
            StartCoroutine(FlickerGameOver());
        }

        IEnumerator FlickerGameOver()
        {
            while (_hasIterate)
            {
                gameOverText.text =_gameOverText;
                yield return new WaitForSeconds(_delay);
                gameOverText.text = _emptyText;
                yield return new WaitForSeconds(_delay);
            }
        }

    }
}
