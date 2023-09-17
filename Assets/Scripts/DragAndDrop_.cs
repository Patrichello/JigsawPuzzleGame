using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DragAndDrop_ : MonoBehaviour
{
    public Sprite[] Levels;

    public GameObject EndMenu;
    public GameObject SelectedPiece;
    private int sortingOrderIncrement = 1;
    public int PlacedPieces = 0;

    private const string PuzzleTag = "Puzzle";
    private const string LevelKey = "Level";
    private const string GameScene = "Game";
    private const string MenuScene = "Menu";

    private void Start()
    {
        GameObject[] puzzlePieces = GameObject.FindGameObjectsWithTag(PuzzleTag);
        foreach (GameObject piece in puzzlePieces)
        {
            piece.transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Levels[PlayerPrefs.GetInt(LevelKey)];
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.CompareTag(PuzzleTag) && !hit.transform.GetComponent<PieceScript>().InRightPosition)
            {
                SelectedPiece = hit.transform.gameObject;
                SelectedPiece.GetComponent<PieceScript>().Selected = true;
                SelectedPiece.GetComponent<SortingGroup>().sortingOrder = sortingOrderIncrement;
                sortingOrderIncrement++;
            }
        }

        if (Input.GetMouseButtonUp(0) && SelectedPiece != null)
        {
            SelectedPiece.GetComponent<PieceScript>().Selected = false;
            SelectedPiece = null;
        }

        if (SelectedPiece != null)
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);
        }

        if (PlacedPieces == 36 && EndMenu != null)
        {
            EndMenu.SetActive(true);
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt(LevelKey, PlayerPrefs.GetInt(LevelKey) + 1);
        SceneManager.LoadScene(GameScene);
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(MenuScene);
    }
}
