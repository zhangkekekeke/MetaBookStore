using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text IpText;
    public Button CollectBtn;
    public Button StartHostBtn;
    public InputField Input;

    // Start is called before the first frame update
    void Start()
    {
        string ip = IP();

        IpText.text = ip;

        CollectBtn.onClick.AddListener(delegate ()
        {
            onCollectClick(ip);
        });
        StartHostBtn.onClick.AddListener(delegate ()
        {
            onStartHostClick(ip);
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    void onCollectClick(string ip)
    {
        GameManager.Instance.CollectClient(ip);
        gameObject.SetActive(false);
    }

    void onStartHostClick(string ip)
    {
        GameManager.Instance.StartHost(ip);
        gameObject.SetActive(false);
    }

    public static string IP()
    {
        string output = "";
        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                  /*  else if (fam == address.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }*/
                }
            }
        }
        return output;
    }
}
