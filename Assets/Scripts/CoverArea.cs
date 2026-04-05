using UnityEngine;
using System.Collections.Generic;

namespace Tribhuvan_AI
{
    public class CoverArea : MonoBehaviour
    {
        public Cover[] covers;

        private bool hasSearched = false;

        private void FindCovers()
        {
            if (hasSearched) return;

            GameObject[] coverObjects = GameObject.FindGameObjectsWithTag("Cover");

            if (coverObjects != null && coverObjects.Length > 0)
            {
                List<Cover> list = new List<Cover>();

                foreach (var obj in coverObjects)
                {
                    if (obj == null) continue;

                    Cover c = obj.GetComponent<Cover>();
                    if (c != null)
                        list.Add(c);
                }

                covers = list.ToArray();
            }

            hasSearched = true;
        }

        public Cover GetRandomCover(Vector3 agentLocation)
        {
            // Lazy initialization happens here
            if (covers == null || covers.Length == 0)
            {
                FindCovers();
            }

            if (covers == null || covers.Length == 0)
                return null;

            return covers[Random.Range(0, covers.Length)];
        }
    }
}