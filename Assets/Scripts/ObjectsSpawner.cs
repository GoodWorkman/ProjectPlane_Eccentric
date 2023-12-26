using UnityEngine;
using Random = UnityEngine.Random;


public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] private int _coinsToSpawn = 10;
    [SerializeField] private int _bombsToSpawn = 15;
    [SerializeField] private float checkRadius = 1.5f;
    
    [SerializeField] private Transform _cornerA;
    [SerializeField] private Transform _cornerB;
    
    [SerializeField] private Transform _coinContainer;
    [SerializeField] private Transform _bombContainer;
    
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Bomb _bombPrefab;

    private void OnDrawGizmos()
    {
        Vector3 center = (_cornerA.position + _cornerB.position) / 2;
        Vector3 size = _cornerB.position - _cornerA.position;
        
        Gizmos.DrawWireCube(center, size);
    }

    private void Awake()
    {
        SpawnObjects(_coinPrefab, _coinsToSpawn, _coinContainer);
        SpawnObjects(_bombPrefab, _bombsToSpawn, _bombContainer);
    }

    private void SpawnObjects(ISpawnable prefab, int count, Transform container)
    {
        for (int i = 0; i < count; i++)
        {
            TrySpawnObjects(prefab, container);
        }
    }

    private void TrySpawnObjects(ISpawnable prefab, Transform container)
    {
        int attempts = 10;

        for (int i = 0; i < attempts; i++)
        {
            Vector3 spawnPosition = GeneratePosition();

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, checkRadius);

            if (colliders.Length == 0)
            {
                prefab.CreateInstance(spawnPosition, container);
                break;
            }
        }
    }

    private Vector3 GeneratePosition()
    {
        float coordX = Random.Range(_cornerA.position.x, _cornerB.position.x);
        float coordY = Random.Range(_cornerA.position.y, _cornerB.position.y);

        return new Vector3(coordX, coordY, 0f);
    }
}
