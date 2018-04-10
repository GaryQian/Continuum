using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillScreen : MonoBehaviour {

    public string levelName;
    Health health;

    private void OnTriggerEnter(Collider other)
    {
        //kills object through health so anything that would happen on die would happen
        if ((health = other.GetComponent<Health>()) != null && other.gameObject.tag == "Player") {
            SceneManager.LoadScene(levelName);
            //health.Damage(100000);
        } else {
            Destroy(other);
        }
    }
}
