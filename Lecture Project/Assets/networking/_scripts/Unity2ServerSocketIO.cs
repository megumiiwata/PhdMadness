/*
 * Unity2ServerSocketIO.cs
 *
 * UBISS 2019 - Workshop A - Project: "PhD Madness"
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Unity2ServerSocketIO : MonoBehaviour {

    #region WEBSOCKET_MESSAGE_API

    // outgoing (Unity3D -> WebSocket server -> WebClient)

    // incoming (WebClient -> WebSocket server -> Unity3D)
    private static readonly string WEBUserWindIntensityUpdate = "web_user_wind_intensity_update";

    #endregion



    #region PROPERTIES

    // flag to indicate whether a connection to the web socket server should be established at startup or not (set via Unity Inspector)
    public bool connectToWebSocketServer;

    // reference to SocketIO component, responsible for establishing the WSS connection to the websocket server
    private SocketIOComponent unity2serverSocket;

    // reference to class taking care of all sensor input
    private WebInput webInput;

    #endregion



    #region START_UPDATE_CYCLE

    public void Start() 
    {
        // setup web socket connection only if indicated
        if (connectToWebSocketServer)
        {
            // initialize connection to websocket server
            GameObject socketioGO = GameObject.Find("SocketIO");
            if (socketioGO == null) Debug.LogError("[Unity2ServerSocketIO] Could not find SocketIO GameObject.");
            unity2serverSocket = socketioGO.GetComponent<SocketIOComponent>();
            if (unity2serverSocket == null) Debug.LogError("[Unity2ServerSocketIO] Could not find SocketIOComponent component of SocketIO GameObject.");
            else
            {
                // register socket status events
                unity2serverSocket.On("open", SocketOpen);
                unity2serverSocket.On("error", SocketError);
                unity2serverSocket.On("close", SocketClose);

                // register socket input events
                unity2serverSocket.On(WEBUserWindIntensityUpdate, SocketInc_WEBUserWindIntensityUpdate);

                // debug
                unity2serverSocket.On("boop", TestBoop);
                unity2serverSocket.On("message", TestMessage);
            }

            // initialize connection to web input
            webInput = GameObject.Find("WebInput").GetComponent<WebInput>();


            // DEBUG
            //StartCoroutine("BeepBoop");
            //StartCoroutine("SendTestMessage");
        }
    }

    #endregion




    #region SOCKET_STATUS_EVENTS

    /// <summary>
    /// Connection to WebSocket server resulted in "open" event.
    /// </summary>
    /// <param name="e">SocketIOEvent open</param>
    private void SocketOpen(SocketIOEvent e)
    {
        Debug.Log("[Unity2ServerSocketIO] Open received: " + e.name + " " + e.data);

        // DEBUG 
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Unity3D_VR"] = "Unity3D UBISS2019 client calling in!";
        unity2serverSocket.Emit("message", new JSONObject(data));
    }

    /// <summary>
    /// Connection to WebSocket server resulted in "error" event.
    /// </summary>
    /// <param name="e">SocketIOEvent error</param>
    private void SocketError(SocketIOEvent e)
    {
        Debug.Log("[Unity2ServerSocketIO] Error received: " + e.name + " " + e.data);
    }

    /// <summary>
    /// Connection to WebSocket server resulted in "close" event.
    /// </summary>
    /// <param name="e">SocketIOEvent close</param>
    private void SocketClose(SocketIOEvent e)
    {   
        Debug.Log("[Unity2ServerSocketIO] Close received: " + e.name + " " + e.data);
    }

    #endregion



    #region SOCKET_INPUT_EVENTS

    private void SocketInc_WEBUserWindIntensityUpdate(SocketIOEvent e)
    {
        //Debug.Log("[Unity2ServerSocketIO] WEBUserNodeSelectionUpdate received: " + e.name + " " + e.data);

        //Debug.Log(e.name);      // socket event name
        //Debug.Log(e.data);      // JSON (message) data, of Unity's type JSONObject

        //Debug.Log(e.data.GetField("wind_intensity").ToString());    // value access for individual key in JSON data (note: string = "string" incl. ")

        // update web user highlighted node for VR user
        //string windIntensity = normalizeIncJsonString(e.data.GetField("wind_intensity").ToString());
        //string windIntensity = normalizeIncJsonString(e.data.GetField("wind_intensity").ToString());
        int windIntensity = int.Parse(normalizeIncJsonString(e.data.GetField("wind_intensity").ToString()));
        //Debug.Log("wind intensity update: " + windIntensity);
        webInput.windIntensity = windIntensity;
        //webInput.windIntensity = Mathf.Clamp(windIntensity, 0, 200);    // restrict input within range between 0 and 200
    }

    #endregion



    #region SOCKET_OUTPUT_MESSAGES

    #endregion



    #region HELPER_METHODS

    /// <summary>
    /// Helper function to cut of leading and tailing " from entered string value.
    /// </summary>
    /// <param name="val">String value with leading and tailing ".</param>
    /// <returns></returns>
    private string normalizeIncJsonString(string val)
    {
        return val.Substring(1, val.Length - 2); // cut of leading and tailing " from string value
    }

    #endregion



    #region DEBUG

    // input event received: TestBoop
    public void TestBoop(SocketIOEvent e)
    {
        Debug.Log("[Unity2ServerSocketIO] Boop received: " + e.name + " " + e.data);
        Debug.Log(e.name);
        Debug.Log(e.data);

        // check out JsonObject implementation (or rather check SimpleJson in our other projects)
        // in order to take e.data and convert it into dictionary etc for usage in C#

        if (e.data == null) { return; }

        Debug.Log(
            "#####################################################" +
            "THIS: " + e.data.GetField("this").str +
            "#####################################################"
        );
    }

    // input event received: TestMessage
    public void TestMessage(SocketIOEvent e)
    {
        Debug.Log("[Unity2ServerSocketIO] Message received: " + e.name + " " + e.data);
    }

    // output message: SendTestMessage
    private IEnumerator SendTestMessage()
    {
        yield return new WaitForSeconds(1);
        unity2serverSocket.Emit("message");

        yield return new WaitForSeconds(1);
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["hello"] = "world";
        data["VRxAR"] = "Labs";
        unity2serverSocket.Emit("message", new JSONObject(data));

        yield return new WaitForSeconds(1);
        unity2serverSocket.Emit("beep");
    }

    // output message: BeepBoop
    private IEnumerator BeepBoop()
    {
        // wait 1 seconds and continue
        yield return new WaitForSeconds(1);
        
        unity2serverSocket.Emit("beep");
        
        // wait 3 seconds and continue
        yield return new WaitForSeconds(3);
        
        unity2serverSocket.Emit("beep");
        
        // wait 2 seconds and continue
        yield return new WaitForSeconds(2);
        
        unity2serverSocket.Emit("beep");
        
        // wait ONE FRAME and continue
        yield return null;
        
        unity2serverSocket.Emit("beep");
        unity2serverSocket.Emit("beep");
    }

    #endregion
}
