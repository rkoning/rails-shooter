using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : WeaponEffect
{
    public GameObject projectilePrefab;
    private readonly List<Projectile> projectiles = new();
    private int current;
    public int maxCacheSize = 10;

    public override void Activate() {
        if (projectiles.Count <= current) {
            if (projectiles.Count < maxCacheSize) {
                projectiles.Add(Instantiate(projectilePrefab).GetComponent<Projectile>());
            } else {
                current = 0;
            }
        }
        projectiles[current].gameObject.SetActive(true);
        projectiles[current].transform.position = transform.position;
        projectiles[current].transform.rotation = Quaternion.LookRotation(weapon.focusPoint - transform.position);
        projectiles[current].FireAt(weapon.focusPoint);
        current++;
    }
}
