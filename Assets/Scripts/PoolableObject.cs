using UnityEngine;


public class PoolableObject : MonoBehaviour
{
    private Rigidbody rb;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void OnDisable()
    {
        CancelInvoke();

        PoolManager.Instance.ReturnToPool(gameObject);

        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }
}
