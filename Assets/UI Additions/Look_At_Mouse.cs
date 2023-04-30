using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_At_Mouse : MonoBehaviour
{
    [SerializeField]
    private float _Magnetute = 0.00075f;
    [SerializeField]
    private float _Limits = 0.05f;
    private void Start()
    {
        if(_Magnetute == 0)
            _Magnetute = 0.00075f;
        if (_Limits == 0)
            _Limits = 0.05f;
    }
    void Update()
    {

        if(Input.GetAxis("Mouse X") < 0)
            {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y + _Magnetute, this.transform.rotation.z, this.transform.rotation.w);
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y - _Magnetute, this.transform.rotation.z, this.transform.rotation.w);
        }

        if (Input.GetAxis("Mouse Y") < 0)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x - _Magnetute, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x + _Magnetute, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        }

        //FIX X AXIS
        if (this.transform.rotation.x < -_Limits) {
            this.transform.rotation = new Quaternion(-_Limits, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w); 
        }
        if (this.transform.rotation.x > _Limits)
        {
            this.transform.rotation = new Quaternion(_Limits, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        }
        //FIX Y AXIS
        if (this.transform.rotation.y > _Limits)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, _Limits, this.transform.rotation.z, this.transform.rotation.w);
        }
        if (this.transform.rotation.y < -_Limits)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, -_Limits, this.transform.rotation.z, this.transform.rotation.w);
        }
    }
}
