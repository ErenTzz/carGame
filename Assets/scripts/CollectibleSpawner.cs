using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public ObjectPooler pooler;
    public string poolTag; // tag of pool entry which corresponds to prefab type
    public int spawnCount = 50;
    public Vector3 areaCenter;
    public Vector3 areaSize; // extents for random placement on Plane Y should match plane's Y

    private void Start()
    {
        SpawnAll();
    }

    public void SpawnAll()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(areaCenter.x - areaSize.x / 2f, areaCenter.x + areaSize.x / 2f),
                areaCenter.y,
                Random.Range(areaCenter.z - areaSize.z / 2f, areaCenter.z + areaSize.z / 2f)
            );

            pooler.SpawnFromPool(poolTag, pos, Quaternion.identity);
        }
    }
}
