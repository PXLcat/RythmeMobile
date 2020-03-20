using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyWithPool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemies;
    public int m_maxEnemies = 10;
    public GameObject m_enemyPrefab;

    private void Awake()
    {
        for (int i = 0; i < m_maxEnemies; i++)
        {
            _enemies.Add(Instantiate(m_enemyPrefab, transform));
        }
    }

}
