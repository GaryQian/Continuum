using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalBossBehavior : MonoBehaviour {

    public GameObject LaserSphereWrapper;
    public float laserSphereRotateSpeed = 50f;
    public GameObject laserSphere1;


    public GameObject BossBullet;
    public float shotInterval = 5f;
    public int shots = 30;

    // Use this for initialization
    void Start () {
        GetComponent<Health>().OnDie += OnDie;

        InvokeRepeating("FireCircle", 10f, shotInterval);
	}

    void FireCircle() {
        var angleStep = 360.0f / shots;

        foreach (var direction in GetSphereDirections(shots)) {
            Shoot(direction);
        }
    }

    private Vector3[] GetSphereDirections(int numDirections) {
        var pts = new Vector3[numDirections];
        var inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        var off = 2f / numDirections;

        foreach (var k in Enumerable.Range(0, numDirections)) {
            var y = k * off - 1 + (off / 2);
            var r = Mathf.Sqrt(1 - y * y);
            var phi = k * inc;
            var x = (float)(Mathf.Cos(phi) * r);
            var z = (float)(Mathf.Sin(phi) * r);
            pts[k] = new Vector3(x, y, z);
        }

        return pts;
    }

    void Shoot(Vector3 shotDirection) {
        //Vector3 shotDirection = this.transform.position - this.transform.parent.position;
        GameObject instanceBullet = Instantiate(BossBullet, this.transform.position, Quaternion.identity);
        instanceBullet.transform.LookAt(instanceBullet.transform.position + shotDirection);
        BulletMovement bulletScript = instanceBullet.GetComponent<BulletMovement>();
        bulletScript.ShotSource = this.gameObject;
    }

	// Update is called once per frame
	void Update () {
        LaserSphereWrapper.transform.Rotate(Vector3.up * Time.deltaTime * laserSphereRotateSpeed);
	}

    void OnDie() {
        Ending.Instance.Begin();
        Destroy(gameObject);
    }
}
