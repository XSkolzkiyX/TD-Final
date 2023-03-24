using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class BuildingPlacer : MonoBehaviour
{
    public bool needToBuild;
    public GameObject buildingPrefab;
    public LevelScript level;
    public int price;
    [SerializeField] private int gridSize;
    [SerializeField] private GameObject placerPrefab;
    private GameObject placer;

    public List<Vector3Int> occupiedPositions = new List<Vector3Int>();

    private void Update()
    {
        if (needToBuild)
        {
            if (!placer) placer = Instantiate(placerPrefab);
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3Int position = Vector3Int.RoundToInt(hit.point);
                    position.x = Mathf.RoundToInt(position.x / gridSize) * gridSize;
                    position.y = 0;
                    position.z = Mathf.RoundToInt(position.z / gridSize) * gridSize;
                    placer.transform.position = position;
                    if (!occupiedPositions.Contains(position) && Input.GetMouseButtonDown(0)) PlaceBuiding(position);
                    else if (Input.GetMouseButtonDown(1)) needToBuild = false;
                }
            }
            //if (Input.GetMouseButtonDown(0)) TryPlaceBuiding();
            //else if (Input.GetMouseButtonDown(1)) needToBuild = false;
        }
    }

    private void PlaceBuiding(Vector3Int position)
    {
        Destroy(placer);
        Instantiate(buildingPrefab, position, Quaternion.identity);
        occupiedPositions.Add(position);
        level.FillScoreText(price);
        needToBuild = false;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Vector3Int position = Vector3Int.RoundToInt(hit.point);
        //    position.x = Mathf.RoundToInt(position.x / gridSize) * gridSize;
        //    position.y = 0;
        //    position.z = Mathf.RoundToInt(position.z / gridSize) * gridSize;
        //    if (!occupiedPositions.Contains(position))
        //    {
        //        Destroy(curPlacer);
        //        Instantiate(buildingPrefab, position, Quaternion.identity);
        //        occupiedPositions.Add(position);
        //        level.FillScoreText(price);
        //        needToBuild = false;
        //    }
        //}
    }
}
