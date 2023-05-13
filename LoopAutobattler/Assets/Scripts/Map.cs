using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private GameObject _mapSkeleton;
    [SerializeField]
    private int _activeEnemies = 0;
    private int _maximumEnemies = 10;
    private bool _spawnerIsActive;
    private bool[] _cellHaveContent;
    private GameObject[] _mapCells;
    public GameObject[] MapCells => _mapCells;

    private void Awake()
    {
        //getting all map cells as a game objects for future use
        _mapCells = new GameObject[transform.childCount];
        for (int i = 0; i < _mapCells.Length; i++)
        {
            _mapCells[i] = transform.GetChild(i).gameObject;
        }

        _cellHaveContent = new bool[transform.childCount];
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if(!_spawnerIsActive && _activeEnemies < _maximumEnemies)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        _spawnerIsActive = true;

        while (_activeEnemies != _maximumEnemies)
        {
            yield return new WaitForSeconds(1f);

            bool enemySpawned = false;
            int spawnCell = Random.Range(1, 63);
            while (enemySpawned == false)
            {
                if (!_cellHaveContent[spawnCell])
                {
                    GameObject spawnedEnemy = Instantiate(_mapSkeleton, _mapCells[spawnCell].transform);
                    spawnedEnemy.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    spawnedEnemy.SetActive(true);

                    enemySpawned = true;
                    _cellHaveContent[spawnCell] = true;
                    _activeEnemies++;
                }
                else
                {
                    spawnCell = Random.Range(1, 63);
                }
            }
        }

        //i need to record what and where was recorded to use that data in future
        _spawnerIsActive = false;
    }
}
