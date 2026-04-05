using UnityEngine;
using Meta.XR.MRUtilityKit;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnTimer = 1f;
    public int spawnTry = 1000;
    public float minEdgeDistance = 0.3f;
    public float normalOffset = 1f;
    public MRUKAnchor.SceneLabels spawnLabels;

    private float timer = 0f;

    void Start()
    {

    }

    void Update()
    {
        // Check if MRUK instance is valid and initialized
        if (MRUK.Instance == null || !MRUK.Instance.IsInitialized)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnTimer)
        {
            SpawnGhost();
            timer = 0f;
        }
    }

    public void SpawnGhost()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        int currentTry = 0;

        while (currentTry < spawnTry)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(
                MRUK.SurfaceType.VERTICAL,
                minEdgeDistance,
                LabelFilter.Included(spawnLabels),
                out Vector3 pos,
                out Vector3 norm
            );

            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                randomPositionNormalOffset.y = 0f;

                Instantiate(prefabToSpawn, randomPositionNormalOffset, Quaternion.identity);
                return;
            }

            currentTry++;
        }
    }
}