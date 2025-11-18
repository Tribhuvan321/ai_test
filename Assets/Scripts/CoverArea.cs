using UnityEngine;

namespace Tribhuvan_AI
{
    public class CoverArea : MonoBehaviour
    {
        public Cover[] covers;

        private void Awake()
        {
            covers = GetComponentsInChildren<Cover>();
        }

        public Cover GetRandomCover(Vector3 agentLocation)
        {
            return covers[Random.Range(0, covers.Length - 1)];
        }
    }

}
