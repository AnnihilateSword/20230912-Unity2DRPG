using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject _cam;

    [SerializeField] private float _parallaxEffect;

    private float _xPosition;
    private float _length;

    private void Start()
    {
        _cam = GameObject.Find("Main Camera");

        _xPosition = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float distanceToMove = _cam.transform.position.x * _parallaxEffect;
        float distanceMoved = _cam.transform.position.x * (1 - _parallaxEffect);

        transform.position = new Vector3(_xPosition + distanceToMove, transform.position.y);

        // ÎÞÏÞ±³¾°
        if (distanceMoved > _xPosition + _length)
            _xPosition = _xPosition + _length;
        else if (distanceMoved < _xPosition - _length)
            _xPosition = _xPosition - _length;
    }
}
