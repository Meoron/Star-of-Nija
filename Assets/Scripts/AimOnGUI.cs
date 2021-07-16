using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimOnGUI : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursor = null;
    [SerializeField]
    private float _offsetX = 0;
    [SerializeField]
    private float _offsetY = 0;

    public float OffsetX {set {_offsetX = value; }}
    public float OffsetY { set { _offsetY = value; } }

    static private AimOnGUI _aimOnGUI;

    public Vector3 _mouseWorldPosition;

    public Vector3 MouseWorldPosition { get { return _mouseWorldPosition; } }
    static public AimOnGUI AimObj { get { return _aimOnGUI; } }

    private void Awake()
    {
        _aimOnGUI = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnGUI()
    {
        //GUI.depth = Depth;
        GUI.DrawTexture(new Rect(Input.mousePosition.x - _offsetX, Screen.height - Input.mousePosition.y - _offsetY, 25.0f, 25.0f), _cursor);
    }
}
