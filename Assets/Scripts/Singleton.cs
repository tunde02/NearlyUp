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

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    Debug.LogWarning($"There're more than 1 instances of {typeof(T)} in Scene");
                }

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton) " + typeof(T).ToString();

                    Debug.LogWarning("[Singleton] An instance of " + typeof(T) +
                        " is needed in the scene, so '" + singleton +
                        "' was created.");
                }
            }

            return _instance;
        }
    }


    protected virtual void Awake()
    {
        if (FindObjectsOfType(typeof(T)).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
