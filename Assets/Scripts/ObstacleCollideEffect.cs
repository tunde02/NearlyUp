using UnityEngine;


public class ObstacleCollideEffect : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-50f, 5f, 0f), ForceMode.Impulse);
        }
    }
}
