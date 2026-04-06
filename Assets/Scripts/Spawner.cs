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

    // Enforce exactly 3 spawned enemies
    private const int MaxEnemies = 3;
    private int currentSpawned = 0;

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
        // Do not spawn if we've already reached the maximum
        if (currentSpawned >= MaxEnemies)
            return;

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

                GameObject go = Instantiate(prefabToSpawn, randomPositionNormalOffset, Quaternion.identity);

                // Track the spawned instance so we can decrement count when it's destroyed
                var tracker = go.AddComponent<SpawnedTracker>();
                tracker.Initialize(this);

                currentSpawned++;
                return;
            }

            currentTry++;
        }
    }

    // Called by SpawnedTracker when a spawned instance is destroyed
    internal void OnSpawnedDestroyed()
    {
        currentSpawned = Mathf.Max(0, currentSpawned - 1);
    }

    // Helper MonoBehaviour attached to each spawned instance to notify the spawner when it is destroyed
    private class SpawnedTracker : MonoBehaviour
    {
        private Spawner owner;

        public void Initialize(Spawner owner)
        {
            this.owner = owner;
        }

        void OnDestroy()
        {
            if (owner != null)
                owner.OnSpawnedDestroyed();
        }
    }
}