using System;
using System.Linq;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace BasicApp.Connectivity
{
    public class ConnectivityService : IConnectivityService
    {
        private ConnectivityTypeChangedEventHandler _onChangeHandler;

        public bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public bool IsConnectedToMobileNetwork()
        {
            return CrossConnectivity.Current.ConnectionTypes.Contains(ConnectionType.Cellular);
        }

        public bool IsConnectedToWifi()
        {
            return CrossConnectivity.Current.ConnectionTypes.Contains(ConnectionType.WiFi);
        }

        public void ListenToConnectivityChanges(Action<object, ConnectivityTypeChangedEventArgs> onChangeHandler)
        {
            _onChangeHandler = onChangeHandler.Invoke;

            CrossConnectivity.Current.ConnectivityTypeChanged += _onChangeHandler;
        }

        public void RemoveListenerFromConnectivityChanges()
        {
            CrossConnectivity.Current.ConnectivityTypeChanged -= _onChangeHandler;
        }
    }
}
