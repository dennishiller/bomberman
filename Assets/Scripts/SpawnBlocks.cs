using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    private GridScript grid;
    public GameObject unbreakableWall;
    public GameObject breakableWall;
    private int maxBreakableWalls = 130;
    private int currentBreakableWalls = 0;
    private float posX = 1.5f;
    private float posZ = 1.5f;
    private readonly float posY = 0.5f;
    private bool[,] WallPosition = new bool[15, 15];
    

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridScript>();
        PlaceWalls();
    }

    // Update is called once per frame
    void Update()
    {
        PlaceBreakable();
    }

    private void PlaceBreakable()   //übergabeparameter, ein array
    {
        if (currentBreakableWalls < maxBreakableWalls)
        {
            Vector3 posBreakable = new Vector3(Random.Range(0, 14.5f), 0, Random.Range(0, 14.5f));
            Vector3 posBreakable2 = grid.GetNearestPointOnGrid(posBreakable);

            if (!FelderCheck(posBreakable2))
            {
                WallPosition[(int)posBreakable2.x, (int)posBreakable2.z] = true;
                Instantiate(breakableWall, posBreakable2, Quaternion.identity);
                currentBreakableWalls++;
            }
        }
    }

    private bool FelderCheck(Vector3 posBreakable2)
    {
        if ((int)posBreakable2.x == 0 && (int)posBreakable2.z == 0 || (int)posBreakable2.x == 1 && (int)posBreakable2.z == 0 || (int)posBreakable2.x == 0 && (int)posBreakable2.z == 1
             || (int)posBreakable2.x == 14 && (int)posBreakable2.z == 14 || (int)posBreakable2.x == 14 && (int)posBreakable2.z == 13 || (int)posBreakable2.x == 13 && (int)posBreakable2.z == 14)
        {
            return true;
        }
        return false;
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

    private void SetArray()
    {
        for(int i = 1; i<15; i=+2)
            for(int j = 1; j < 15; j=+2)
            {
                WallPosition[i, j] = true;
            }
    }

    
}
