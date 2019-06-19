using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnbreakable : MonoBehaviour
{
    private GridScript grid;
    public GameObject unbreakableWall;
    private float posX = 1.5f;
    private float posZ = 1.5f;
    private readonly float posY = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridScript>();
        PlaceWalls();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlaceWalls()
    {

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Instantiate(unbreakableWall, new Vector3(posX, posY, posZ), Quaternion.identity);
                    posZ += 2;
                }
                posZ = 1.5f;
                posX += 2;
            }
        
    }
}
