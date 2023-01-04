using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DeployingScript : MonoBehaviour
{
    public List<GameObject> pinoyDefendersPrefabs;
    public List<Image> pinoyDefendersImages;
    public List<Image> pinoyDefendersMouseImages;
    public List<Button> pinoyDefendersButtons;
    public int selectedID = -1;
    public Tilemap deployableTileMap;
    public DeployingScript deployingScript;
    public SpawnEnemyScipt spawnEnemyScipt;
    public GameObject[] pinoySoldiers;
    public static Vector3 sellPos;

    public Tile tile;

    void Start() { }

    void Update()
    {
        canDeploy();
        cursorImage();
        setTileNull();
    }

    void canDeploy()
    {
        if (selectedID != -1)
        {
            detectDeployPoint();
        }
    }

    void setTileNull()
    {
        var tilepos = deployableTileMap.WorldToCell(sellPos);
        deployableTileMap.SetTile(tilepos, null);
        deployableTileMap.SetTile(tilepos, tile);
    }

    //detects the pointed coordinates of mouse,converting mouse position to cell position, centering the object to tile, checks if tilemap has sprite or none
    void detectDeployPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = deployableTileMap.LocalToCell(mousePosition);
            Vector3 cellPositionCentered = deployableTileMap.GetCellCenterLocal(cellPosition);

            if (deployableTileMap.GetColliderType(cellPosition) == Tile.ColliderType.Sprite)
            {
                deployPinoyDefenders(cellPositionCentered);
                deployableTileMap.SetColliderType(cellPosition, Tile.ColliderType.None);
            }
        }
    }

    //spawning/instantiating pinoy defenders prefab through id, setting the position of pinoy defender prefab
    void deployPinoyDefenders(Vector3 position)
    {
        GameObject pinoyDefender = Instantiate(pinoyDefendersPrefabs[selectedID]);
        pinoyDefender.transform.position = position;

        //decrease money based on the price of defender
        MoneyScript.money -= MoneyScript.pinoyDefendersCost[selectedID];

        DeselectPinoyDefenders();
    }

    //logic for selecting the right defender, setting the black and white when clicked
    public void selectPinoyDefender(int id)
    {
        if (selectedID == -1)
        {
            selectedID = id;
            var i = 0;

            foreach (var img in pinoyDefendersImages)
            {
                if (i == selectedID && i != -1)
                {
                    img.color = new Color(255, 255, 255, 0.7f);
                }
                else
                {
                    img.color = Color.white;
                }
                i++;
            }
        }
        else
        {
            DeselectPinoyDefenders();
        }
    }

    //deselecting the defender
    void DeselectPinoyDefenders()
    {
        selectedID = -1;

        foreach (var img in pinoyDefendersImages)
        {
            img.color = Color.white;
        }
    }

    //cursor image when clicked
    void cursorImage()
    {
        if (selectedID != -1)
        {
            pinoyDefendersMouseImages[selectedID].color = Color.white;
            // range color
            pinoyDefendersMouseImages[selectedID].GetComponentInChildren<SpriteRenderer>().color =
                new Color(255, 0, 0, 0.2747f);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 2f;
            pinoyDefendersMouseImages[selectedID].transform.position =
                Camera.main.ScreenToWorldPoint(mousePosition);
        }
        else
        {
            foreach (var img in pinoyDefendersMouseImages)
            {
                img.color = new Color(0, 0, 0, 0);
                img.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
        }
    }
}
