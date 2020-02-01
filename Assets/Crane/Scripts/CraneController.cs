using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CraneController : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _verticalBar;

	[SerializeField]
	private SpriteRenderer _horizontalBar;

	[SerializeField]
	private SpriteRenderer _leftEnd;

	[SerializeField]
	private SpriteRenderer _rightEnd;

	[SerializeField]
	private Transform _barJoiner;

	public SpriteRenderer horizontalBar => _horizontalBar;
	public SpriteRenderer verticalBar => _verticalBar;
	public SpriteRenderer leftEnd => _leftEnd;
	public SpriteRenderer rightEnd => _rightEnd;
	public Transform barJoiner => _barJoiner;

	[SerializeField]
	private CranePulleyMover _pulley;

	public CranePulleyMover pulley => _pulley;

	public CraneClaw claw => _pulley.claw;

    public void Event_ToggleClaw(bool grab)
    {
        if (claw.isGrasping != grab)
        {
            claw.isGrasping = grab;
        }
    }

    public void Event_MoveHorizontal(float distance)
    {
        pulley.horizontalPosition += distance;
    }

    public void Event_MoveVertical(float distance)
    {
        pulley.ClawDistance -= distance;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(CraneController))]
public class CraneController_Inspector : Editor
{

    public CraneController crane => target as CraneController;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        showCraneControls();
        showPulleyControls();

    }

    private void showCraneControls()
    {

        GUILayout.Space(20);

        // if the horizontal bar field is not set, return
        if (crane.horizontalBar == null)
        {
            return;
        }

        // create an int field to change the crane's width
        int width = EditorGUILayout.IntField("Width", Mathf.RoundToInt(crane.horizontalBar.size.x));

        // if the width is changed
        if (width != Mathf.RoundToInt(crane.horizontalBar.size.x))
        {

            // apply the size change
            crane.horizontalBar.size = new Vector2(width, crane.horizontalBar.size.y);

            // move the crane ends to match the horizonal bar
            Vector3 lpos = crane.leftEnd.transform.localPosition;
            Vector3 rpos = crane.rightEnd.transform.localPosition;
            lpos.x = width / -2f - crane.leftEnd.sprite.pivot.x / crane.leftEnd.sprite.pixelsPerUnit;
            rpos.x = width / 2f + crane.rightEnd.sprite.pivot.x / crane.rightEnd.sprite.pixelsPerUnit;
            crane.leftEnd.transform.localPosition = lpos;
            crane.rightEnd.transform.localPosition = rpos;

            // notify a the crane's data has been modified
            EditorUtility.SetDirty(crane);
        }


        // if the vertical bar field is not set, return
        if (crane.verticalBar == null || crane.barJoiner == null)
        {
            return;
        }

        // create a float field to change the crane's height
        float height = EditorGUILayout.FloatField("Height", crane.verticalBar.size.y);

        // if the value is changed
        if (height != crane.verticalBar.size.y)
        {

            // apply the size change
            crane.verticalBar.size = new Vector2(crane.verticalBar.size.x, height);

            // get the top of the vertical bar sprite
            float maxY = crane.verticalBar.transform.position.y + crane.verticalBar.size.y;

            // change the position of the bar joiner and horizontal bar's y position to be at the top of the vertical bar
            Vector3 jpos = crane.barJoiner.transform.position;
            Vector3 hpos = crane.horizontalBar.transform.position;
            jpos.y = maxY;
            hpos.y = maxY;
            crane.barJoiner.transform.position = jpos;
            crane.horizontalBar.transform.position = hpos;

            // notify a the crane's data has been modified
            EditorUtility.SetDirty(crane);
        }
    }

    private void showPulleyControls()
    {

        // return if the pulley is not set
        if (crane.pulley == null)
        {
            return;
        }

        GUILayout.Space(20);

        // create a slider that represents the pulley's horizontal position
        GUILayout.Label("Pulley Position");
        var x = crane.pulley.transform.position.x;
        float pulleyposX = EditorGUILayout.Slider(x, crane.pulley.leftBound, crane.pulley.rightBound);

        // if the slider is moved
        if (pulleyposX != x)
        {
            // apply the position change
            crane.Event_MoveHorizontal(pulleyposX - x);

            var clawPos = crane.pulley.claw.transform.localPosition;
            clawPos.x = crane.pulley.transform.localPosition.x;
            crane.pulley.claw.transform.localPosition = clawPos;

            // notify a the crane's data has been modified
            EditorUtility.SetDirty(crane.pulley);
        }

        // create a slider for the claw's vertical position
        GUILayout.Label("Claw Position");
        var dist = crane.pulley.ClawDistance;
        float pulleyDist = EditorGUILayout.Slider(dist, crane.pulley.clawUpperBound, crane.pulley.clawLowerBound);

        // if the slider is moved
        if (pulleyDist != dist)
        {
            // apply the position change
            crane.Event_MoveVertical(dist - pulleyDist);

            var clawPos = crane.pulley.claw.transform.localPosition;
            clawPos.y = crane.pulley.transform.localPosition.y - dist;
            crane.pulley.claw.transform.localPosition = clawPos;

            crane.pulley.LateUpdate();

            // notify a the crane's data has been modified
            EditorUtility.SetDirty(crane.pulley);
        }


        // add a toggle control to the inspector that the user can click on
        bool grasping = EditorGUILayout.Toggle("Claw Grasp", crane.pulley.claw.isGrasping);

        // if the toggle is clicked
        if (grasping != crane.pulley.claw.isGrasping)
        {

            // apply the property to the CraneClaw target
            crane.Event_ToggleClaw(grasping);
            EditorUtility.SetDirty(crane.pulley.claw); // notify that a change to the claw's data has occured
        }
    }
}

#endif