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
    public class Connection
    {
        public TcpClient connection { get; set; }
        

        public Connection(TcpClient client) { 
            connection = client;
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

                                v.LoadBackup(new BackupWithProgress(BackupName, 0, new ManualResetEvent(true)), "en", (progress) =>
                                {
                                    BackupWithProgress bk = v._backupsWithProgress.Single(x => x.SaveName == BackupName);
                                    bk.Progress = progress;

                                    var msgToSend = JsonSerializer.SerializeToUtf8Bytes(new MessageContent { Type = MessageType.BackupProgress, Body = progress.ToString().Replace(",", ".") });

                                    connection.GetStream().Write(msgToSend);
                                });
                                break;
                            case MessageType.ClientPauseTask:
                                BackupName = msg.Body;
                                var vm = EasySaveApp.viewmodel.ViewModel.getInstance();

                                BackupWithProgress backup = vm._backupsWithProgress.Single(x => x.SaveName == BackupName);
                                backup.ResetEvent.Reset();
                                backup.IsSuspended = true;
                                backup.IsRunning = false;
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
    }
}
