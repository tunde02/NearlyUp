using UnityEngine;
using UnityEngine.UI;


public class SlowMotionGauge : MonoBehaviour
{
    [SerializeField] private RectTransform gauge;


    private readonly int initialWidth = 300;
    private readonly int initialHeight = 65;
    private PlayerController player;


    private void Start()
    {
        player = GameManager.Instance.playerController;
    }

    private void Update()
    {
        var width = initialWidth * (player.slowMotionGauge / player.maxSlowMotionGauge);
        gauge.sizeDelta = new Vector2(width, initialHeight);
    }
}
