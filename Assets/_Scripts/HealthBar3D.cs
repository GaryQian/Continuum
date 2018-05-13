using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar3D : MonoBehaviour {

    public GameObject HealthCube;
    public int healthPerCube = 10;
    public float cubeDistance = 0.5f;
    public float aboveHeadDistance = 1f;
    public float horizontalOffset = 0f;
    public Health HealthComponent;
    public Vector3 healthCubeScale = Vector3.one;
    public int currentHealthCubeNum = 0;
    private Stack<GameObject> HealthCubes;
    private GameObject HealthBarContainer;


	// Use this for initialization
	void Start () {        
        HealthCubes = new Stack<GameObject>();
        HealthComponent = this.GetComponent<Health>();
        HealthComponent.OnDie += OnDie;
        currentHealthCubeNum = (int)HealthComponent.health / healthPerCube;
        HealthBarContainer = new GameObject();
        HealthBarContainer.transform.parent = this.gameObject.transform;
        HealthBarContainer.transform.localPosition = new Vector3(horizontalOffset, aboveHeadDistance, 0f);

        Vector3 cubePosition = Vector3.zero;
        cubePosition.y += aboveHeadDistance;
        int midpoint = currentHealthCubeNum / 2;
        for (int i = midpoint; i > midpoint * (-1); i--){
            cubePosition.x = ((healthCubeScale.x) + cubeDistance) * i;
            GameObject cubeInstance = Instantiate(HealthCube, Vector3.zero, Quaternion.identity);
            cubeInstance.transform.parent = HealthBarContainer.transform;
            cubeInstance.transform.localPosition = cubePosition;
            cubeInstance.transform.localScale = healthCubeScale;
            HealthCubes.Push(cubeInstance);
        }

	}
	
	// Update is called once per frame
	void Update () {
        int updatedHealthCubeNum = (int)HealthComponent.health / healthPerCube;
        if(currentHealthCubeNum > updatedHealthCubeNum){
            for (int i = 0; i < currentHealthCubeNum - updatedHealthCubeNum; i++)
            {
                Destroy(HealthCubes.Pop());
            }
        }
        currentHealthCubeNum = updatedHealthCubeNum;


        HealthBarContainer.transform.LookAt(GameManager.Instance.player.transform);
	}

    void OnDie(){
        while (HealthCubes.Count != 0)
        {
            Destroy(HealthCubes.Pop());
        }
        Destroy(gameObject);
    }
}
