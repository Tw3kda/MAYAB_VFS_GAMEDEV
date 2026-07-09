using UnityEngine;

public class ParticleCreator : MonoBehaviour
{
    [SerializeField] private GameObject _particle;

    public void CreateParticle()
    {
        Debug.Log("HERE");
        GameObject particle = Instantiate(_particle,gameObject.transform.position, Quaternion.identity);
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        ps.Play();
    }
}
