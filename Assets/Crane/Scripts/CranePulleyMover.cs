using UnityEngine;

public class CranePulleyMover : MonoBehaviour
{
    [SerializeField]
    private CraneClaw _claw = null;
    public CraneClaw claw => _claw;

    private DistanceJoint2D _clawJoint;
    private DistanceJoint2D clawJoint
    {
        get
        {
            if (_clawJoint == null)
            {
                _clawJoint = _claw.GetComponent<DistanceJoint2D>();
            }
            return _clawJoint;
        }
    }

    [SerializeField]
    private Transform _pulleyBase = null;

    [SerializeField]
    private Transform _midweight = null;

    [SerializeField]
    private SpriteRenderer[] _baseLines = null;

    [SerializeField]
    private SpriteRenderer _clawLine = null;

    [SerializeField]
    private Transform _leftBounds = null;

    [SerializeField]
    private Transform _rightBounds = null;

    [SerializeField]
    private Transform _clawUpperBounds = null;

    [SerializeField]
    private Transform _clawLowerBounds = null;


    public float leftBound => _leftBounds.position.x;
    public float rightBound => _rightBounds.position.x;
    public float clawUpperBound => _clawUpperBounds.position.y;
    public float clawLowerBound => _clawLowerBounds.position.y;

    public float horizontalPosition
    {
        get => transform.position.x;
        set
        {
            Vector3 tpos = transform.position;

            // clamp the x position between the left and right bounds
            tpos.x = Mathf.Clamp(value, leftBound, rightBound);

            // apply the new position
            transform.position = tpos;
        }
    }

    public float ClawDistance
    {
        get => clawJoint.distance;
        set
        {
            // clamp the y value between the upper and lower bounds
            var distance = Mathf.Clamp(value, clawLowerBound, clawUpperBound);

            // apply the position change
            clawJoint.distance = distance;
        }
    }

    private void lerpMidweight()
    {
        _midweight.transform.position = Vector3.Lerp(_pulleyBase.transform.position, claw.transform.position, 0.5f);
    }

    private void stretchLines()
    {
        Vector3 dif; // difference between two positions
        float direction; // direction from one position to another
        Vector2 size; // the size of a sprite as defined by it's sprite renderer

        // stretch out each base line to the midweight
        foreach (SpriteRenderer line in _baseLines)
        {

            // store the y offset of the line's position from the pulley base
            float offY = line.transform.localPosition.y;

            // calculate the difference in position between the base and the midweight
            dif = _midweight.position - _pulleyBase.position;

            // calculate the angle direction from the pulley base pointing to the midweight
            direction = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

            // rotate the lines according to the positional difference between the base and the midweight
            line.transform.rotation = Quaternion.Euler(0, 0, direction + 90);

            // get the tiled size of the sprite
            size = line.size;

            // set the y size to the distance between the midweight and pulley base
            size.y = dif.magnitude;

            // apply the y offset
            size.y += (offY);

            // apply the tiled size
            line.size = size;
        }

        // store the y offset of the claw line from the midweight
        float mwOffY = _clawLine.transform.localPosition.y;

        // calculate the positional difference between the midweight and the claw
        dif = _claw.transform.position - _midweight.position;

        // rotate the line from the midweight to the claw
        direction = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        _clawLine.transform.rotation = Quaternion.Euler(0, 0, direction + 90);

        // apply the size changes to the sprite
        size = _clawLine.size;
        size.y = dif.magnitude;
        size.y += mwOffY;
        _clawLine.size = size;
    }

    [ExecuteInEditMode]
    public void LateUpdate()
    {
        lerpMidweight();
        stretchLines();
    }
}

