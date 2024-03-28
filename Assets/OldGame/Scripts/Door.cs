using UnityEngine;
using YG;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private AudioSource audioSource;
    [Space(15)]
    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] private Sprite closeDoorSprite;
    [Space(15)]
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;

    private void Start()
    {
        if (!YandexGame.savesData.sounds)
        {
            audioSource.volume = 0.0f;
        }
    }

    public void OpenAndClose()
    {
        if(spriteRenderer.sprite == closeDoorSprite)
        {
            audioSource.clip = doorOpenSound;
            audioSource.Play();
            spriteRenderer.sprite = openDoorSprite;
            boxCollider2D.enabled = false;
        }
        else if(spriteRenderer.sprite == openDoorSprite)
        {
            audioSource.clip = doorCloseSound;
            audioSource.Play();
            spriteRenderer.sprite = closeDoorSprite;
            boxCollider2D.enabled = true;
        }
    }
}