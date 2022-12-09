using EasySaveApp.model;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml.Linq;

namespace EasySaveApp.Socket
{
    public class Connection : IDisposable
    {
        private bool disposedValue;

        public TcpClient connection { get; set; }
        public Thread thread = null;


        public Connection(TcpClient client) { 
            connection = client;
            thread = new(() => {
                this.HandleConnection();
            });
        }

        internal void HandleConnection()
        {
            Byte[] bytes = new byte[1024];
            String data = null;

            // Send list of
            var message = File.ReadAllText(System.Environment.CurrentDirectory + @"\Works\backupList.json");

            var msgToSend = JsonSerializer.SerializeToUtf8Bytes(new MessageContent { Type = MessageType.ConnectionInit, Body = message });

            connection.GetStream().Write(msgToSend);

            while (true)
            {
                int i;
                try
                {
                    while ((i = connection.GetStream().Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        var msg = JsonSerializer.Deserialize<MessageContent>(data);

                        switch (msg.Type)
                        {
                            case MessageType.ClientStartTask:
                                string BackupName = msg.Body;
                                
                                var v = EasySaveApp.viewmodel.ViewModel.getInstance();

                                Thread temp = new(() => {
                                    v.LoadBackup(new BackupWithProgress(BackupName, 0, new ManualResetEvent(true)), "en", (progress) =>
                                    {
                                        BackupWithProgress bk = v._backupsWithProgress.Single(x => x.SaveName == BackupName);
                                        bk.Progress = progress;

                                        var msgToSend = JsonSerializer.SerializeToUtf8Bytes(new MessageContent { Type = MessageType.BackupProgress, Body = progress.ToString().Replace(",", ".") });

                                        if (connection != null)
                                        {
                                            connection.GetStream().Write(msgToSend);
                                        }
                                    });
                                });
                                temp.Start();
                                break;
                                
                            case MessageType.ClientPauseTask:
                                BackupName = msg.Body;
                                var vm = EasySaveApp.viewmodel.ViewModel.getInstance();

                                BackupWithProgress backup = vm._backupsWithProgress.Single(x => x.SaveName == BackupName);
                                backup.ResetEvent.Reset();
                                backup.IsSuspended = true;
                                backup.IsRunning = false;
                                break;

                            case MessageType.ClientStopConnection:
                                ConnectionPool.GetInstance().RemoveConnection(this);
                                connection.GetStream().Close();
                                connection.Close();
                                connection.Dispose();
                                connection = null;
                                break;

                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"[Client Connection] Error : {e.Message}");
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~Connection()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
