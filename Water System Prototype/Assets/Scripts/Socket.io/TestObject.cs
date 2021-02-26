using System.Collections;
using System.Collections.Generic;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class TestObject : MonoBehaviour {
    private QSocket socket_v1, socket_v2, socket_v3, socket_v4, socket_v5, socket_v6;
    private QSocket node_socket_1, node_socket_2;

    void Start()
    {
        /*
        Debug.Log("start");
        socket = IO.Socket("ws://localhost");

        socket.On(QSocket.EVENT_CONNECT, () => {
            Debug.Log("Connected");
            socket.Emit("chat", "test");
        });

        socket.On("chat", data => {
            Debug.Log("data : " + data);
        });
        */

        //Start_socket("http://localhost:80", "starting ver_1", "connected_ver1", socket_v1);
        //Start_socket("http://localhost:90", "starting ver_2", "connected_ver2", socket_v2);
        Start_socket("ws://localhost:80", "starting ver_3", "connected_ver3", socket_v3);
        Start_socket("ws://localhost:90", "starting ver_4", "connected_ver4", socket_v4);
        //Start_socket("http://localhost", "starting ver_5", "connected_ver5", socket_v5);
        Start_socket("ws://localhost", "starting ver_6", "connected_ver6", socket_v6);

        //Start_socket("http://localhost:3000", "starting node_socket_1", "connected node_socket_1", node_socket_1);
        Start_socket("ws://localhost:3000", "starting node_socket_2", "connected node_socket_2", node_socket_2);
    }

    private void Start_socket(string uri, string start_debug_msg, string connected_debug_msg, QSocket socket)
    {
        Debug.Log(start_debug_msg);
        socket = IO.Socket(uri);

        socket.On(QSocket.EVENT_CONNECT, () => {
            Debug.Log(connected_debug_msg);
            //socket.Emit("chat", "test");
        });

        socket.On("chat", data => {
            Debug.Log("data : " + data);
        });
    }

    private void OnDestroy()
    {
        socket_v1.Disconnect();
        socket_v2.Disconnect();
        socket_v3.Disconnect();
        socket_v4.Disconnect();
        socket_v5.Disconnect();
        socket_v6.Disconnect();

        node_socket_1.Disconnect();
        node_socket_2.Disconnect();
    }

    public void sendMsg()
    {
        socket_v1.Emit("chat", "test");
        socket_v2.Emit("chat", "test");
        socket_v3.Emit("chat", "test");
        socket_v4.Emit("chat", "test");
        socket_v5.Emit("chat", "test");
        socket_v6.Emit("chat", "test");

        node_socket_1.Emit("chat", "test");
        node_socket_2.Emit("chat", "test");
    }
}