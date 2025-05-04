using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class SpawnManager : MonoBehaviour
    {
        [FormerlySerializedAs("_enemyPrefab")]
        [SerializeField]
        private GameObject enemyPrefab;
        [FormerlySerializedAs("_enemyContainer")] 
        [SerializeField]
        private GameObject enemyContainer;
        [FormerlySerializedAs("_asteroid")] [SerializeField]
        private GameObject asteroid;
        [FormerlySerializedAs("_powerUps")] [SerializeField]
        private GameObject[] powerUps;
    

        private bool _stopSpawning;

        public void StartSpawning()
        {
            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnPowerUpRoutine());
        }


        IEnumerator SpawnEnemyRoutine()
        {
            yield return new WaitForSeconds(3.0f);
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
            while (_stopSpawning == false)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = enemyContainer.transform;
                yield return new WaitForSeconds(5.0f);
            }

        }

        IEnumerator SpawnPowerUpRoutine()
        {
            yield return new WaitForSeconds(3.0f);
            while (_stopSpawning == false)
            {
                Vector3 posToSpawnPowerUp = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
                int randomPowerUp = Random.Range(0, 3);
                GameObject powerUp = Instantiate(powerUps[randomPowerUp], posToSpawnPowerUp, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(7.0f, 10.0f));
            }

        }

        public void OnPlayerDeath()
        {
            _stopSpawning = true;
        }
    }
}
