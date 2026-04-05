using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;
using System.Collections;

public class RunTimeNavmeshGenerator : MonoBehaviour
{
    private NavMeshSurface navmeshSurface;

    // Start is called before the first frame update
    void Start()
    {
        navmeshSurface = GetComponent<NavMeshSurface>();

        if (MRUK.Instance != null)
        {
            MRUK.Instance.RegisterSceneLoadedCallback(BuildNavmesh);
        }
    }

    public void BuildNavmesh()
    {
        StartCoroutine(BuildNavmeshRoutine());
    }

    public IEnumerator BuildNavmeshRoutine()
    {
        // Wait one frame to ensure scene is fully loaded
        yield return new WaitForEndOfFrame();

        navmeshSurface.BuildNavMesh();
    }
}