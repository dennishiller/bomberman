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
    private int maxPowerUps = 50;       //reingehauen
    private int currentPowerUps = 0;       //same
    public GameObject MaxBombsPowerUp;
    public GameObject StrongerbombsPowerUp;
    public GameObject SpeedPowerUp;
    private GameObject[] PowerUpArray = new GameObject[3];
    private bool[,] PowerUpPosition = new bool[15, 15];




    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridScript>();
        PowerUpArray[0] = MaxBombsPowerUp;
        PowerUpArray[1] = StrongerbombsPowerUp;
        PowerUpArray[2] = SpeedPowerUp;
        PlaceWalls();
    }

    // Update is called once per frame
    void Update()

    {
        PlaceBreakable();
        PlacePowerUps();
    }

    private void PlaceBreakable()   //übergabeparameter, ein array
    {
        if (currentBreakableWalls < maxBreakableWalls)
        {
            Vector3 posBreakable = new Vector3(Random.Range(0, 14.5f), 0, Random.Range(0, 14.5f));
            Vector3 posBreakable2 = grid.GetNearestPointOnGrid(posBreakable);

            if (!FelderCheck(posBreakable2) && WallPosition[(int)posBreakable2.x, (int)posBreakable2.z]==false)
            {
                WallPosition[(int)posBreakable2.x, (int)posBreakable2.z] = true;
                Instantiate(breakableWall, posBreakable2, Quaternion.identity);
                currentBreakableWalls++;
            }
        }
    }

    private void PlacePowerUps()
    {
        if (currentPowerUps < maxPowerUps)
        {
            Vector3 posPowerUp = new Vector3(Random.Range(0, 14.5f), 0, Random.Range(0, 14.5f));
            Vector3 posPowerUp2 = grid.GetNearestPointOnGrid(posPowerUp);

            if (!FelderCheck(posPowerUp2) && PowerUpPosition[(int)posPowerUp2.x, (int)posPowerUp2.z] == false)
            {
                PowerUpPosition[(int)posPowerUp2.x, (int)posPowerUp2.z] = true;
                Instantiate(PowerUpArray[Random.Range(0,3)], posPowerUp2, Quaternion.identity);
                currentPowerUps++;
            }
        }
    }

    private bool FelderCheck(Vector3 pos)
    {
        if ((int)pos.x == 0 && (int)pos.z == 0 || (int)pos.x == 1 && (int)pos.z == 0 || (int)pos.x == 0 && (int)pos.z == 1
             || (int)pos.x == 14 && (int)pos.z == 14 || (int)pos.x == 14 && (int)pos.z == 13 || (int)pos.x == 13 && (int)pos.z == 14)
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
