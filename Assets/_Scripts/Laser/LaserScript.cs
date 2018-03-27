using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public LineRenderer laserLineRenderer;
    public float laserMaxLength = 10f;
    public float laserDPS = 100.0f;
    public GameObject laser;
    public Transform target;
    public Vector3 dist;

    public float moveDist = 1.5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    private Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
        laserLineRenderer = this.gameObject.GetComponent<LineRenderer>();
        moveDist = Random.Range(1, 3);
    }

    void Update()
    {
        castLaser();
        moveLR();
    }

    void castLaser() {
        dist = target.position - laser.transform.position;
		laserLineRenderer.SetPosition(0, laser.transform.position);
        laserLineRenderer.SetPosition(1, DetectHit(laser.transform.position, laserMaxLength, dist));
    }

    void moveLR() {
        Vector3 v = startPosition;
        v.x += moveDist * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }

    Vector3 DetectHit(Vector3 startPos, float distance, Vector3 direction)
    {
        Ray ray = new Ray(startPos, direction.normalized);
        RaycastHit hit;
        Vector3 endPos = startPos + (distance * direction);

        if (Physics.Raycast(ray, out hit, distance))
        {
            endPos = hit.point;
            if (hit.transform.gameObject.CompareTag("Player"))
            {
				Debug.Log ("Player");
                damagePlayer();
            }
            return endPos;
        }

        return endPos;
    }

    void damagePlayer() {
        GameManager.Instance.player.GetComponent<Health>().Damage(laserDPS * Time.deltaTime);
    }
}
