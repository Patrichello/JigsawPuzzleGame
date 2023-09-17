using UnityEngine;
using UnityEngine.Rendering;

public class PieceScript : MonoBehaviour
{
    private Vector3 RightPosition;
    private Transform _transform;
    private SortingGroup _sortingGroup;
    public bool InRightPosition;
    public bool Selected;

    private void Start()
    {
        RightPosition = transform.position;
        _transform = transform;
        _sortingGroup = GetComponent<SortingGroup>();
        _transform.position = new Vector3(Random.Range(5f, 11f), Random.Range(2.5f, -7)); //5f,11f,2.5f,-7f this is free area in my scene
    }

    private void Update()
    {
        if (!Selected && !InRightPosition && Vector3.Distance(_transform.position, RightPosition) < 0.5f)
        {
            _transform.position = RightPosition;
            InRightPosition = true;
            _sortingGroup.sortingOrder = 0;
            Camera.main.GetComponent<DragAndDrop_>().PlacedPieces++;
        }
    }
}
