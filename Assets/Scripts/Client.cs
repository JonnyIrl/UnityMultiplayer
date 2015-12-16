using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class Client : MonoBehaviour {

    int ChannelID;
    int socketID;
    int socketPort = 9000;
    int connectionID;
    List<byte> _updateMessage = new List<byte>();
    int _updateMessageLength = 9;
    private static int player = 0;
    public static bool moving;
    int recConnectionId;
    // Use this for initialization
    void Start()
    {
        NetworkTransport.Init();
        ConnectionConfig config = new ConnectionConfig();
        ChannelID = config.AddChannel(QosType.Reliable);
        int maxConnections = 15;
        HostTopology topology = new HostTopology(config, maxConnections);
        socketID = NetworkTransport.AddHost(topology, socketPort);
        //Debug.Log("Socket Open. Socket ID = " + socketID);
        player = 0;
        moving = false;
        //Connect();
    }

    public void Connect()
    {
        byte error;                                 //mINE IS 149.153.102.52
        connectionID = NetworkTransport.Connect(socketID, "149.153.102.62", socketPort, 0, out error);
        //Debug.Log("Sending my connect message " );
        if(player == 0)
        {
            player = 2;
        }
        

    }

    public void SendSocketMessage()
    {
        byte error;
        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, "Hello");

        int bufferSize = 1024;
        NetworkTransport.Send(socketID, connectionID, ChannelID, buffer, bufferSize, out error);
        //SendMyUpdate(10000, 100000,new Vector2(1000, 1000), 1000);
    }

    // Update is called once per frame
    void Update()
    { 
 
            int recHostId;
            int recChannelId;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            byte error;
            NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostId, out recConnectionId, out recChannelId,
                recBuffer, bufferSize, out dataSize, out error);
            //connectionID = recConnectionId;
            //change this to send player data
            GameObject m_object;


            if (player == 1)
            {
                m_object = GameObject.Find("BlueBall");
                Vector3 pos = m_object.transform.position;
                SendMyUpdate(pos.x, pos.y);
            }
            else if (player == 2)
            {
                m_object = GameObject.Find("RedBall");
                Vector3 pos = m_object.transform.position;
                SendMyUpdate(pos.x, pos.y);
            }


        switch (recNetworkEvent)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                //Debug.Log("incoming connection event received");
                if (player == 0)
                {
                    player = 1;
                }
                break;
            case NetworkEventType.DataEvent:
                // Stream stream = new MemoryStream(recBuffer);
                //BinaryFormatter formatter = new BinaryFormatter();
                // string message = formatter.Deserialize(stream) as string;
                // Debug.Log("incoming message event received: " + message);
                recievedMessage(recBuffer);
                break;
            case NetworkEventType.DisconnectEvent:
                //Debug.Log("remote client event disconnected");
                break;
        }
        
    }
    public void recievedMessage(byte[] data)
    {
        //Let's figure out what type of message this is.
        //Debug.Log("Receiving Message ");
        char messageType = (char)data[0];
        if (messageType == 'U' )
        {
            float posX = System.BitConverter.ToSingle(data, 1);
            float posY = System.BitConverter.ToSingle(data, 5);
            //float velX = System.BitConverter.ToSingle(data, 9);
            //float velY = System.BitConverter.ToSingle(data, 13);
            //float rotZ = System.BitConverter.ToSingle(data, 17);
            //Debug.Log("Player 2" + " is at (" + posX + ", " + posY + ") traveling (" + velX + ", " + velY + ") rotation " + rotZ);

            GameObject m_object;
            if (player == 1)
            {
                m_object = GameObject.Find("RedBall");
                m_object.transform.position = new Vector3(posX,posY,0);

            }
            else if (player == 2)
            {
                m_object = GameObject.Find("BlueBall");
                m_object.transform.position = new Vector3(posX, posY, 0);
            }
        }
            
        

    }
    public void SendMyUpdate(float posX, float posY)
    {

            byte error;
            _updateMessage.Clear();
            _updateMessage.Add((byte)'U');
            _updateMessage.AddRange(System.BitConverter.GetBytes(posX));
            _updateMessage.AddRange(System.BitConverter.GetBytes(posY));
            //_updateMessage.AddRange(System.BitConverter.GetBytes(velocity.x));
            //_updateMessage.AddRange(System.BitConverter.GetBytes(velocity.y));
            //_updateMessage.AddRange(System.BitConverter.GetBytes(rotZ));
            byte[] messageToSend = _updateMessage.ToArray();
            //Debug.Log("Sending my update message  " + messageToSend + " to all players in the room , Length " +
            //    messageToSend.Length);
            //PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, messageToSend);
            NetworkTransport.Send(socketID, connectionID, ChannelID, messageToSend, messageToSend.Length, out error);
    }
    public static int getPlayer()
    {
        return player;
    }

    public static void Moving(bool state)
    {
        moving = state;
    }
}
