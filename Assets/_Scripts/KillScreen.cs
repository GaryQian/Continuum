using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScreen : MonoBehaviour {

    Health health;

    private void OnTriggerEnter(Collider other)
    {
        //kills object through health so anything that would happen on die would happen
        if ((health = other.GetComponent<Health>()) != null) {
            health.Damage(100000);
        } else {
            Destroy(other);
        }
    }
}
