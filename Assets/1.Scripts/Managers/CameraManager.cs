using UnityEngine;
using DefineEnums;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    const float _camDefultAngle = 65;
    const float _camSinAngle = 0.46631f;

    const float _maxRaycastDistance = 20;

    Camera _mainCam;
    float _camHeight;

    public CameraPositionType _type { get; private set; }
    public UnitBase _followUnit { get; private set; }
    float CameraZCorrectValue => (_camHeight - _followUnit.transform.position.y) * _camSinAngle;

    private void LateUpdate()
    {
        switch (_type)
        {
            case CameraPositionType.Scene:
                //ÄÆ¾À ±â´É Ãß°¡.
                break;
            case CameraPositionType.UnitFollow:
                FollowRegisteredUnit();
                break;

            default:
                break;
        }
    }

    public void Init(float camOffset)
    {
        //_mainCam = Camera.main;
        _mainCam = GetComponent<Camera>();
        _type = CameraPositionType.UnitFollow;
        _camHeight = camOffset;
        transform.rotation = Quaternion.Euler(_camDefultAngle, 0, 0);
    }


    public void EnrollUnit(UnitBase unit)
    {
        _followUnit = unit;
        _type = CameraPositionType.UnitFollow;
    }

    public void FollowRegisteredUnit()
    {
        transform.position = new Vector3(_followUnit.transform.position.x, _camHeight,
                                          _followUnit.transform.position.z - CameraZCorrectValue);
    }

    public bool GetClickPosition(Vector3 mousePosition, out RaycastHit floorHit)
    {
        return Physics.Raycast(_mainCam.ScreenPointToRay(mousePosition), out floorHit, _maxRaycastDistance);
    }


}
