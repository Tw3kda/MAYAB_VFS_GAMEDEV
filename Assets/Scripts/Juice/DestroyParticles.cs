using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    [SerializeField] private float _particleDuration = 0.3f;


    private void Start()
    {
        Destroy(gameObject, _particleDuration);
    }
}
