using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadius : MonoBehaviour
{
    [SerializeField, Header("ê¸ÇÃî≠ê∂à íu")]
    private Transform _pencil;

    [SerializeField, Header("îºåaÇÃç≈ëÂ")]
    private float _maxRadius;

    [SerializeField, Header("îºåaÇÃç≈è¨")]
    private float _minRadius;

    [SerializeField, Header("îºåaÇÃí≤êﬂë¨ìx")]
    private float _speed;

    [SerializeField]
    private float _radius = 0;

    private float _prevScroll = 0;
    // Start is called before the first frame update
    void Start()
    {
        _radius = _pencil.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRadius();
    }

    private void UpdateRadius()
    {
        float scroll = Input.mouseScrollDelta.y*Time.deltaTime*_speed;
        //float scrollDelta = scroll - _prevScroll;
        //_prevScroll = scroll;

        //_radius -= scrollDelta;
        //_radius = Mathf.Clamp(_radius, _minRadius, _maxRadius);
        //Vector3 update = new Vector3(0, _radius, 0);
        //return update;
        _radius += scroll;

        _pencil.localPosition = new Vector3(0, _radius, 0);
    }
}
