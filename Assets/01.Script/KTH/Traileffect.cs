using UnityEngine;

public class Traileffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _tip;
    [SerializeField]
    private GameObject _base;
    [SerializeField]
    private GameObject _trailMesh;
    [SerializeField]
    private int _trailFrameLength;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private int _frameCount;
    private Vector3 _previousTipPosistion;
    private Vector3 _previousBasePosistion;

    private const int NUM_VERTICES = 12;

    void Start()
    {
        _mesh = new Mesh();
        _trailMesh.GetComponent<MeshFilter>().mesh = _mesh;

        _vertices = new Vector3[_trailFrameLength * NUM_VERTICES];
        _triangles = new int[_vertices.Length];

        _previousTipPosistion = _tip.transform.position;
        _previousBasePosistion = _base.transform.position;
    }

    private void LateUpdate()
    {
        if (_frameCount == (_trailFrameLength * NUM_VERTICES))
        {
            _frameCount = 0;
        }


        _vertices[_frameCount] = _base.transform.position;
        _vertices[_frameCount + 1] = _tip.transform.position;
        _vertices[_frameCount + 2] = _previousTipPosistion;

        _vertices[_frameCount + 3] = _base.transform.position;
        _vertices[_frameCount + 4] = _previousTipPosistion;
        _vertices[_frameCount + 5] = _tip.transform.position;

        _vertices[_frameCount + 6] = _previousTipPosistion;
        _vertices[_frameCount + 7] = _base.transform.position; 
        _vertices[_frameCount + 8] = _previousBasePosistion;

        _vertices[_frameCount + 9] = _previousTipPosistion;
        _vertices[_frameCount + 10] = _previousBasePosistion;
        _vertices[_frameCount + 11] = _base.transform.position;

        _triangles[_frameCount] = _frameCount;
        _triangles[_frameCount + 1] = _frameCount + 1;
        _triangles[_frameCount + 2] = _frameCount + 2;
        _triangles[_frameCount + 3] = _frameCount + 3;
        _triangles[_frameCount + 4] = _frameCount + 4;
        _triangles[_frameCount + 5] = _frameCount + 5;
        _triangles[_frameCount + 6] = _frameCount + 6;
        _triangles[_frameCount + 7] = _frameCount + 7;
        _triangles[_frameCount + 8] = _frameCount + 8;
        _triangles[_frameCount + 9] = _frameCount + 9;
        _triangles[_frameCount + 10] = _frameCount + 10;
        _triangles[_frameCount + 11] = _frameCount + 11;

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;

        _previousTipPosistion = _tip.transform.position;
        _previousBasePosistion = _base.transform.position;

        _frameCount += NUM_VERTICES;        
    }

}
