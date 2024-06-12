using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPointList;


    void Start()
    {
        foreach (var spawnPoint in spawnPointList)
        {
            StartCoroutine(SpawnObstalceCoroutine(spawnPoint));
        }
    }

    private IEnumerator SpawnObstalceCoroutine(Transform spawnPoint)
    {
        while (true)
        {
            var obj = PoolManager.Instance.SpawnFromPool(spawnPoint.position, Quaternion.identity);

            var randomForce = Random.Range(5f, 15f);
            var randomForceDirection = new Vector3(Random.Range(-1f, 0f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            var randomScale = new Vector3(Random.Range(2f, 5f), Random.Range(2f, 5f), Random.Range(2f, 5f));

            if (obj != null)
            {
                obj.GetComponent<Rigidbody>().AddForce(randomForce * randomForceDirection, ForceMode.Impulse);
                obj.transform.localScale = randomScale;

                StartCoroutine(DisableCoroutine(obj, 20f));
            }

            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator DisableCoroutine(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        obj.SetActive(false);
    }
}
