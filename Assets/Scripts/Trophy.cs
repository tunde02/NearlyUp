using System;
using UnityEngine;


public class Trophy : MonoBehaviour
{
    public static event EventHandler<OnLevelClearArgs> OnLevelClear;
    public class OnLevelClearArgs : EventArgs
    {
        public int level;
        public int tier;
    }


    [SerializeField] private Mesh[] trophyMesh;
    public int tier;


    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = trophyMesh[tier - 1];
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        GameManager.Instance.ShowCursor();
        GameManager.Instance.PauseGame();

        OnLevelClear?.Invoke(this, new OnLevelClearArgs { level = LevelManager.Instance.GetCurrentLevel(), tier = tier });
    }
}
