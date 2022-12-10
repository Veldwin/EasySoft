using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using EasySaveApp.model;
using System.Text.Json;
using EasySaveApp.Socket;
using System.Net.Http;
using System.Threading;
using System.IO;
using System.Windows.Documents;
using System.Threading.Tasks;

namespace EasySaveConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpClient _connection = null;
        private NetworkStream _serverStream = null;
        private BackgroundWorker thread;
        private bool IsConnected;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartConnexionToServer(object sender, RoutedEventArgs e)
        {
            _connection = new();

            string address = AdresseTextBox.Text;

            _connection.Connect(IPAddress.Parse(address), 66);

            _serverStream = _connection.GetStream();
            IsConnected = true;

            thread = new BackgroundWorker();
            thread.WorkerReportsProgress = true;
            thread.WorkerSupportsCancellation = true;

            thread.DoWork += (sender, eventArgs) =>
            {
                int i;
                byte[] bytes = new byte[1024];
                String data = null;


                while (!(sender as BackgroundWorker).CancellationPending && (i = _serverStream.Read(bytes, 0, bytes.Length)) != 0)
                {                    
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    var msg = JsonSerializer.Deserialize<MessageContent>(data);

                    thread.ReportProgress(1, msg);                    
                }

            };

            thread.ProgressChanged += (sender, eventArgs) =>
            {
                MessageContent m = eventArgs.UserState as MessageContent;

                switch (m.Type)
                {
                    case MessageType.ConnectionInit:
                        List<Backup> backups = JsonSerializer.Deserialize<List<Backup>>(m.Body);
                        ListAllBackups(backups);
                        break;
                    case MessageType.BackupProgress:
                        int backupWithProgress = (int)JsonSerializer.Deserialize<float>(m.Body);
                        ProgressionBar.Value = backupWithProgress;
                        break;
                }
            };

            thread.RunWorkerCompleted += (s, e) =>
            {
                var message = JsonSerializer.SerializeToUtf8Bytes(new MessageContent { Type = MessageType.ClientStopConnection, Body = null });
                _serverStream.Write(message);
                Task.Run(() =>
                {
                    Thread.Sleep(10000);
                    _serverStream.Close();
                    _connection.Close();
                });
                MessageBox.Show("Connexion interrompu");
            };

            thread.RunWorkerAsync();
        }

        //stop the connection
        private void StopConnexionToServer(object sender, RoutedEventArgs e)
        {
            thread.CancelAsync();
        }


        private void ListAllBackups(List<Backup> l)
        {
            foreach (Backup b in l) ListSaveWork.Items.Add(b.SaveName);
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void StartBackup_Click(object sender, RoutedEventArgs e)
        {
            string selected = ListSaveWork.SelectedItem.ToString();

            var message = JsonSerializer.SerializeToUtf8Bytes(new MessageContent { Type = MessageType.ClientStartTask, Body = selected });

            _serverStream.Write(message);
        }

        private void PauseBackup_Click(object sender, RoutedEventArgs e)
        {
            string selected = ListSaveWork.SelectedItem.ToString();

            var message = JsonSerializer.SerializeToUtf8Bytes(new MessageContent { Type = MessageType.ClientPauseTask, Body = selected });

            _serverStream.Write(message);
        }

        private void StopBackup_Click(object sender, RoutedEventArgs e)
        {
            string selected = ListSaveWork.SelectedItem.ToString();

            var message = JsonSerializer.SerializeToUtf8Bytes(new MessageContent { Type = MessageType.ClientStopTask, Body = selected });

            _serverStream.Write(message);
        }
        

        private void GridMenuMouseDown(object sender, RoutedEventArgs e)//Function that allows you to move the software window.
        {
            DragMove();//Function that allows movement
        }
    }
}
