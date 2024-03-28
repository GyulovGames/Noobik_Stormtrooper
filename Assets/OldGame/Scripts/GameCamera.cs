using System.Collections;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float shootsShakeDuration = 0.1f;
    [SerializeField] private float shootShakeIntensity = 0.05f;
    [Space(10)]
    [SerializeField] private float tntShakeDureation = 0.1f;
    [SerializeField] private float tntShakeIntensity = 0.05f;

    private Transform cameraTransform;
    private Vector3 originalPosition;

    private void Start()
    {
        Noobik.ShootEvent.AddListener(ShootingShake);
        TNT.TNTExplosion.AddListener(ExplosionShake);

        cameraTransform = GetComponent<Transform>();
        originalPosition = cameraTransform.position;
    }

    private void ShootingShake()
    {
        StartCoroutine(ShakeCoroutine(shootsShakeDuration, shootShakeIntensity));
    }

    private void ExplosionShake()
    {
        StartCoroutine(ShakeCoroutine(tntShakeDureation, tntShakeIntensity));
    }


    private IEnumerator ShakeCoroutine(float duration, float intensity)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * intensity;
            cameraTransform.position = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = originalPosition;
    }
}