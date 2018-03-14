using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public GameManager gm;
    public LineRenderer laserLineRenderer;
    public float laserMaxLength = 10f;
    public float laserDPS = 100.0f;
    public GameObject laser;
    public Transform target;
    public Vector3 dist;

    void Start() {
        laserLineRenderer = this.gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        castLaser();
    }

    void castLaser() {
        dist = target.position - laser.transform.position;
        laserLineRenderer.SetPosition(0, laser.transform.position);
        laserLineRenderer.SetPosition(1, DetectHit(laser.transform.position, laserMaxLength, dist));
    }

    Vector3 DetectHit(Vector3 startPos, float distance, Vector3 direction)
    {
        Ray ray = new Ray(startPos, direction);
        RaycastHit hit;
        Vector3 endPos = startPos + (distance * direction);

        if (Physics.Raycast(ray, out hit, distance))
        {
            endPos = hit.point;
            if (hit.transform.gameObject.CompareTag("Player"))
            {
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
