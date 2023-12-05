using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Formation> formationsToSpawn;

    private void OnEnable() {
        foreach (var formation in formationsToSpawn) {
            formation.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other) {
        foreach (var formation in formationsToSpawn) {
            formation.gameObject.SetActive(true);
        }
    }
}
