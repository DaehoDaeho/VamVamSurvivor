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

    private Dictionary<string, GameObject> prefabLookup;

    void BuildPrefabLookup()
    {
        prefabLookup = new Dictionary<string, GameObject>();

        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyInfo enemy = enemies[i];

            if(enemy == null || string.IsNullOrEmpty(enemy.id) == true || enemy.enemyPrefab == null)
            {
                continue;
            }

            if(prefabLookup.ContainsKey(enemy.id) == true)
            {
                continue;
            }

            prefabLookup.Add(enemy.id, enemy.enemyPrefab);
        }
    }

    public GameObject FindPrefab(string enemyId)
    {
        if(prefabLookup == null)
        {
            BuildPrefabLookup();
        }

        GameObject foundPrefab;
        if (prefabLookup.TryGetValue(enemyId, out foundPrefab) == true)
        {
            return foundPrefab;
        }

        //if (prefabLookup.ContainsKey(enemyId) == true)
        //{
        //    return prefabLookup[enemyId];
        //}

        return null;
    }
}
