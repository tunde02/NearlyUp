using UnityEngine;


public class Trampoline : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Vector3 randomDirection = new(Random.Range(0f, 1f), 0.55f, Random.Range(0f, 1f));

        other.rigidbody.AddForce(randomDirection, ForceMode.Impulse);

        GetComponent<Animation>().Play("Trampoline");
    }
}
