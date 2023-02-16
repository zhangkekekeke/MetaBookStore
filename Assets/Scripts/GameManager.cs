using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    UnityTransport mTransport;

    void Start()
    {
        Instance = this;
        mTransport = GetComponent<UnityTransport>();

    }

    public void StartHost(string ip)
    {
        mTransport.SetConnectionData(ip, 7777);
        NetworkManager.Singleton.StartHost();
    }

    public void CollectClient(string ip)
    {
        mTransport.SetConnectionData(ip, 7777);
        NetworkManager.Singleton.StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
