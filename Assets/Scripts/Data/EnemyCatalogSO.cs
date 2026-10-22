using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 직렬화 클래스.
/// </summary>
[Serializable]
public class EnemyInfo
{
    public string id;
    public GameObject enemyPrefab;
}

[CreateAssetMenu(fileName = "EnemyCatalog", menuName = "Game/Enemy Catalog")]
public class EnemyCatalogSO : ScriptableObject
{
    public List<EnemyInfo> enemies = new List<EnemyInfo>();

    public GameObject FindPrefab(string enemyId)
    {
        for(int i=0; i<enemies.Count; i++)
        {
            if (enemies[i].id == enemyId)
            {
                return enemies[i].enemyPrefab;
            }
        }

        return null;
    }
}
