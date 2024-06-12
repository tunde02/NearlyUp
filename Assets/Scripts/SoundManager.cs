using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClip bgmAudioClip;
    [SerializeField] private AudioClip collisionEffectAudioClip;


    private AudioSource bgm;
    private AudioSource collisionEffect;


    private void Start()
    {
        var root = new GameObject { name = "Sounds" };
        DontDestroyOnLoad(root);

        var bgmGameObject = new GameObject { name = "BGM" };
        var collisionEffectGameObject = new GameObject { name = "Collision Effect" };

        bgm = bgmGameObject.AddComponent<AudioSource>();
        collisionEffect = collisionEffectGameObject.AddComponent<AudioSource>();

        bgm.clip = bgmAudioClip;
        bgm.volume = 0.5f;
        collisionEffect.clip = collisionEffectAudioClip;

        bgmGameObject.transform.parent = root.transform;
        collisionEffectGameObject.transform.parent = root.transform;

        bgm.loop = true;

        PlayBGM();
    }

    public void PlayBGM()
    {
        bgm.Play();
    }

    public void PlayCollisionEffect()
    {
        collisionEffect.Play();
    }
}
