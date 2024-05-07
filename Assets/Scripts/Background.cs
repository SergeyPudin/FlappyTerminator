using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MeshRenderer _renderer;

    private Vector2 _meshOffset;

    private void Start()
    {
        _meshOffset = _renderer.sharedMaterial.mainTextureOffset;
    }

    private void Update()
    {
        var offsetX =Mathf.Repeat(Time.time *_speed, 1);
        var offset = new Vector2(offsetX, _meshOffset.y);

        _renderer.sharedMaterial.mainTextureOffset = offset;
    }

    private void OnDisable()
    {
        _renderer.sharedMaterial.mainTextureOffset = _meshOffset;
    }
}