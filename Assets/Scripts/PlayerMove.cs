using Unity.Netcode;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    NetworkVariable<Vector3> SyncPos = new NetworkVariable<Vector3>();
    NetworkVariable<Quaternion> SyncRot = new NetworkVariable<Quaternion>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            Move();
            UpdateTransform();
        }

    }

    private void FixedUpdate()
    {
        if (!IsLocalPlayer)
        {
            SyncTransform();
        }
    }

    private void SyncTransform()
    {
        transform.position = Vector3.Lerp(transform.position, SyncPos.Value, Time.deltaTime * 10);
        transform.rotation = Quaternion.Lerp(transform.rotation, SyncRot.Value, Time.deltaTime * 10);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 delta = transform.forward * vertical + transform.right * horizontal;

        transform.position += Time.deltaTime * delta * 10;
    }

    void UpdateTransform()
    {
        if (IsServer)
        {
            SyncPos.Value = transform.position;
            SyncRot.Value = transform.rotation;
        }
        else
        {
            UpdateTransformServerRpc(transform.position, transform.rotation);
        }
    }

    [ServerRpc]
    void UpdateTransformServerRpc(Vector3 pos, Quaternion rot)
    {
        SyncPos.Value = pos;
        SyncRot.Value = rot;
    }
}
