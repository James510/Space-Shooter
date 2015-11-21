using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject asteroidMed;
    public GameObject asteroidSmall;
    public GameObject enemyShip;
    public int enemyCount;
    public int level;
    public float spawnTimer;
    public float spawnWait;
    public Vector3 spawnValues;
    public Text score;
    private int points;

	void Start ()
    {
        points = 0;
        StartCoroutine (SpawnWaves());
	}
	
    void Score(int x)
    {
        points += x;
        score.text = "Score : " + points;
    }
	IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(spawnWait);
        while(true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                if(Random.Range(0,2)==0)
                    Instantiate(asteroidMed, spawnPosition, spawnRotation);
                if (Random.Range(0, 2) == 1)
                    Instantiate(asteroidSmall, spawnPosition, spawnRotation);
                if (Random.Range(0, 5) == 1)
                {
                    GameObject clone = (GameObject)Instantiate(enemyShip, spawnPosition, spawnRotation);
                    clone.name = "BasicEnemyShip1";
                }
                yield return new WaitForSeconds(spawnTimer);
            }
        }     
    }
}
