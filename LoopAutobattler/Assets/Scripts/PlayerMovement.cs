using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Map _map;
    [SerializeField]
    private GameObject _playerVisual;

    [SerializeField]
    private Transform childTransform;

    private int _cellIndex = 1;
    private Transform _target;
    private float _movementSpeed = 0.8f;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        _target = _map.MapCells[_cellIndex].transform;
        transform.position = _map.MapCells[_cellIndex].transform.position;
    }

    private void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _movementSpeed * Time.deltaTime, Space.World);

        ActiveCellCheck();

        if (Vector3.Distance(transform.position, _target.position) <= 0.01f)
        {
            if (_map.MapCells[_cellIndex].name == "Ground (33)" || _map.MapCells[_cellIndex].name == "Ground (61)")
                _spriteRenderer.flipX = !_spriteRenderer.flipX;

            GetNextMapCell();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            if (_movementSpeed == 0.8f)
            {
                _movementSpeed = 0;
                _playerVisual.GetComponent<Animator>().speed = 0;
            }
            else
            {
                _movementSpeed = 0.8f;
                _playerVisual.GetComponent<Animator>().speed = 1;
            }  
    }

    private void GetNextMapCell()
    {

        if (_cellIndex >= _map.MapCells.Length - 1)
        {
            _cellIndex = 0;
            return;
        }
        _cellIndex++;
        _target = _map.MapCells[_cellIndex].transform;
    }

    private void ActiveCellCheck()
    {
        if (_map.MapCells[_cellIndex].transform.childCount > 0)
        {
            if (_cellIndex == 0)
            {
                Debug.Log("City");
                return;
            }

            /* Script that remove enemy from the cell
            foreach (Transform child in _map.MapCells[_cellIndex].transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            */
        }
    }
}
