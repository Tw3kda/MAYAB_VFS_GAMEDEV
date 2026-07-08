using UnityEngine;

public class HC_Bullet : MonoBehaviour
{
    [Header("Defaults (overridden by weapon)")]
    [SerializeField] float defaultSpeed = 40f;
    [SerializeField] float defaultLifetime = 2f;
    [SerializeField] float defaultDamage = 10f;

    [Header("Impact")]
    [SerializeField] GameObject impactEffectPrefab;
    [SerializeField] float impactEffectDuration = 1f;
    [SerializeField] LayerMask hitLayers = ~0;
    [SerializeField] int piercingTargets = 1;

    Rigidbody rb;
    ObjectPool ownerPool;
    float damage;
    float lifetime;
    float timer;
    float knockbackForce;
    int pierceCount = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnEnable()
    {
        timer = 0f;
    }

    public void Initialize(float damage, float speed, float lifetime, float knockback, ObjectPool pool, GameObject impactPrefab = null)
    {
        this.damage = damage;
        this.lifetime = lifetime;
        this.knockbackForce = knockback;
        this.ownerPool = pool;
        if (impactPrefab != null)
            this.impactEffectPrefab = impactPrefab;

        rb.linearVelocity = transform.forward * speed;
        timer = 0f;
    }

    public void InitializeSimple()
    {
        damage = defaultDamage;
        lifetime = defaultLifetime;
        knockbackForce = 0f;
        ownerPool = null;
        rb.linearVelocity = transform.forward * defaultSpeed;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
            ReturnOrDestroy();
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((hitLayers.value & (1 << collision.gameObject.layer)) == 0) return;

        IDamageable target = collision.gameObject.GetComponent<IDamageable>();
        if (target != null)
        {
            Vector3 hitPoint = collision.contacts.Length > 0 ? collision.contacts[0].point : transform.position;
            Vector3 hitDir = transform.forward;
            target.TakeDamage(damage, hitPoint, hitDir);
            pierceCount++;

            if (knockbackForce > 0f)
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                if (targetRb != null)
                    targetRb.AddForce(hitDir * knockbackForce, ForceMode.Impulse);
            }
        }

        SpawnImpactEffect(collision);
       if (pierceCount >= piercingTargets)
        {
            ReturnOrDestroy();
        }
        else
        {
            return;
        }
            
    }

    void SpawnImpactEffect(Collision collision)
    {
        if (impactEffectPrefab == null) return;
        Vector3 point = collision.contacts.Length > 0 ? collision.contacts[0].point : transform.position;
        Vector3 normal = collision.contacts.Length > 0 ? collision.contacts[0].normal : -transform.forward;
        GameObject fx = Instantiate(impactEffectPrefab, point, Quaternion.LookRotation(normal));
        Destroy(fx, impactEffectDuration);
    }

    void ReturnOrDestroy()
    {
        rb.linearVelocity = Vector3.zero;
        if (ownerPool != null)
            ownerPool.Return(gameObject);
        else
            Destroy(gameObject);
    }       
}
