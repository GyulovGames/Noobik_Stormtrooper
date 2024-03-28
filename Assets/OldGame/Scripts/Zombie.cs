using UnityEngine;
using UnityEngine.Events;
using YG;

public class Zombie : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] zombieHitSounds;

    public static UnityEvent ZombieHitEvent = new UnityEvent();

    private bool isHite = false;



    private void Start()
    {
        if (!YandexGame.savesData.sounds)
        {
            audioSource.volume = 0.0f;
        }
    }

    public void Hit()
    {
        if (!isHite)
        {
            isHite = true;
            ZombieHitEvent.Invoke();

            int clipIndex = Random.Range(0, zombieHitSounds.Length);
            audioSource.clip  = zombieHitSounds[clipIndex];
            audioSource.Play();
        }
    }
}