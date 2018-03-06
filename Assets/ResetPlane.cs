using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlane : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //kills object through health so anything that would happen on die would happen
        if (other.tag == "Player")
        {
            Transform transform = other.GetComponent<Transform>();
            Vector3 vec = new Vector3(0, 0, 0);
            transform.SetPositionAndRotation(vec, Quaternion.identity);
        }
    }
}
