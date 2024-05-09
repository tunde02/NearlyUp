using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    Debug.LogError($"There's no instance of {typeof(T)}");

                    return null;
                }
            }

            return _instance;
        }
    }


    private void Awake()
    {
        // Instance = FindObjectOfType(typeof(T)) as T;

        DontDestroyOnLoad(gameObject);
    }
}
