using System;
using Plugin.Connectivity.Abstractions;

namespace BasicApp.Connectivity
{
    public interface IConnectivityService
    {
        bool IsConnected();

        bool IsConnectedToWifi();

        bool IsConnectedToMobileNetwork();

        void ListenToConnectivityChanges(Action<object, ConnectivityTypeChangedEventArgs> onChangeHandler);

        void RemoveListenerFromConnectivityChanges();
    }
}
