using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AutoPuTTY.Desktop.Services
{
    internal sealed class NetworkService
    {
        public Task<bool> GetPortAccessibility(string host, string port)
        {
            return Task.Factory.StartNew(() =>
            {
                Socket socket = null;

                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    IAsyncResult result = socket.BeginConnect(host, Int32.Parse(port), null, null);

                    bool success = result.AsyncWaitHandle.WaitOne(2000, true);

                    if (socket.Connected)
                    {
                        socket.EndConnect(result);
                        return true;
                    }
                    else return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (socket?.Connected ?? false)
                    {
                        socket?.Disconnect(false);
                    }

                    socket?.Close();
                }
            });
        }

        public Task<bool> GetPingAccessibility(string host)
        {
            return Task.Factory
                .StartNew(() =>
                {
                    bool pingable = false;
                    Ping pinger = null;

                    try
                    {
                        pinger = new Ping();
                        PingReply reply = pinger.Send(host);
                        pingable = reply.Status == IPStatus.Success;
                    }
                    catch (PingException)
                    {
                        // Discard PingExceptions and return false;
                    }
                    finally
                    {
                        if (pinger != null)
                        {
                            pinger.Dispose();
                        }
                    }

                    return pingable;
                });
        }
    }
}
