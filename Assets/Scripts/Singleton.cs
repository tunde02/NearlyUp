using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; }


    private void Awake()
    {
        Inst = FindObjectOfType(typeof(T)) as T;
    }
}
