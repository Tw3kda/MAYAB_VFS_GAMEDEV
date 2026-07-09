using UnityEngine;

public interface IBullet
{
    public void Initialize(float damage, float speed, float lifetime, float knockback, ObjectPool pool, GameObject impactPrefab = null);
}
