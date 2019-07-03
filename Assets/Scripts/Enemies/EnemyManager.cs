using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static List<IEnemy> LiveEnemies => liveEnemies;

    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private List<GameObject> enemyPrefabs;
    [SerializeField]
    private float spawnDistance = 30;
    [SerializeField]
    private float spawnHeightOffset = -1;


    private static List<IEnemy> liveEnemies = new List<IEnemy>();


    public void KillAll()
    {
        foreach (IEnemy e in liveEnemies)
        {
            e.Kill();
        }
        liveEnemies.Clear();
    }

    public void CreateWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(RandomPosition(), enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
        }
    }

    private Vector3 RandomPosition()
    {
        Vector2 pos = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 returnPos;
        returnPos.x = pos.x;
        returnPos.z = pos.y;
        returnPos.y = spawnHeightOffset;
        return returnPos;
    }

    private void CheckEndWaveCondition()
    {
        if(liveEnemies.Count == 0 && player.PlayerHP != 0)
        {
            GameManager.Instance.EndWave();
        }
    }

    private void SpawnEnemy(Vector3 position, GameObject prefab)
    {
        IEnemy enemy = Instantiate(prefab, position, Quaternion.identity).GetComponentInChildren<IEnemy>();
        enemy.SetMultiliers(
            1 + (float)GameManager.Instance.waveNumber * 0.02f,
            1 + (float)GameManager.Instance.waveNumber * 0.05f,
            1 + (float)GameManager.Instance.waveNumber * 0.02f + Random.Range(0,0.2f),
            1
        );
        enemy.OnKill = () => {liveEnemies.Remove(enemy); CheckEndWaveCondition();};
        liveEnemies.Add(enemy);
    }
}
