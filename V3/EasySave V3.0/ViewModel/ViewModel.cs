using System;
using EasySaveApp.model;
using System.Collections.Generic;
using MessageBox = System.Windows.MessageBox;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.ComponentModel;
using System.Linq;
using EasySaveApp.view;

namespace EasySaveApp.viewmodel
{
    public class ViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private Model model;
        string[] jail_apps = Model.GetJailApps();
        public string[] BlackListApp { get => jail_apps; set => jail_apps = value; }
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        
        private List<string> nameslist;
        public List<Backup> backups;

        public ViewModel()
        {
            model = new Model();
            Thread ServerThread = new Thread(StartServer);
            ServerThread.Start();
        }

        private static ViewModel? _viewModel;
        public static ViewModel getInstance()
        {
            if (_viewModel == null)
            {
                _viewModel = new ViewModel();
            }
            return _viewModel;
        }


        public void AddSaveModel(string type, string saveName, string sourceDir, string targetDir, string mirrorDir)//Function that allows you to add a backup
        {
            model.SaveName = saveName;
            Backup backup = new Backup(model.SaveName, sourceDir, targetDir, type, mirrorDir);
            model.AddSave(backup); // Calling the function to add a backup job
        }


        public List<string> ListBackup()//Function that lets you know the lists of the names of the backups.
        {

            List<string> nameslist = new List<string>();
            foreach (var obj in model.NameList())
            {
                nameslist.Add(obj.SaveName);
            }
            return nameslist;
        }

        public void LoadBackup(BackupWithProgress backup, string language, Action<float> progressChangeFunction)//Function that allows you to load the backups that were selected by the user.
        {
            if (Model.CheckSoftware(BlackListApp))//If a program is in the blacklist we do not start the backup.
            {
                if (language == "fr")
                {
                    MessageBox.Show("ECHEC DE SAUVEGARDE \n" +
                        "ERREUR N°1 : LOGICIEL BLACKLIST \n" +
                        "EN COURS D'EXECUTION");

                }
                else
                {
                    MessageBox.Show("BACKUP FAILURE \n" +
                        "ERROR N°1 : BLACKLIST SOFTWARE\n" +
                        "IN PROGRESS");
                }
            }
            else
            {
                model.LoadSave(backup, progressChangeFunction);//Function that launches backups

                backup.IsRunning = false;
                if (!backup.IsAborted)
                {
                    if (language == "fr")
                    {
                        MessageBox.Show("SAUVEGARDE REUSSIE ");
                    }
                    else
                    {
                        MessageBox.Show("SUCCESSFUL BACKUP ");
                    }
                } else
                {
                    if (language == "fr")
                    {
                        MessageBox.Show("SAUVEGARDE ABORTEE ");
                    }
                    else
                    {
                        MessageBox.Show("BACKUP ABORTED ");
                    }
                }
            }
        }

        public void IsCryptChecked(bool state)
        {           
            model.isCheck = state;
        }


        public void StartServer()//Function to start the server
        {
            // Establish the local endpoint for the socket.    
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).First();
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 66);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    allDone.Reset();// Set the event to nonsignaled state.  

                    // Start an asynchronous socket to listen for connections. 
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    allDone.WaitOne();// Wait until a connection is made before continuing.  
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();// Signal the main thread to continue.  

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            try
            {
                // Retrieve the state object and the handler socket  
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                // Read data from the client socket.
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead >= 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read
                    // more data.  
                    content = state.sb.ToString();

                    List<string> names = ListBackup();
                    foreach (var name in names)//Loop that allows you to manage the names in the list.
                    {
                        if (content.IndexOf("getdata") > -1)
                        {
                            Send(handler, name + Environment.NewLine); //Function that allows you to insert the names of the backups in the list.
                        }
                        else if (content.IndexOf("PLAY" + name) > -1)
                        {
                            // MessageBox.Show("PLAY" + name);
                            LoadBackup(new BackupWithProgress(name, 0, new ManualResetEvent(true)), "en", (float progress) => { }); //Function that allows you to launch the backups.
                        }
                        else if (content.IndexOf("PAUSE" + name) > -1)
                        {
                            MessageBox.Show("PAUSE" + "" + name);

                        }
                        else if (content.IndexOf("STOP" + name) > -1)
                        {
                            MessageBox.Show("STOP" + " " + name);
                        }
                        else if (content.IndexOf("getprogressing" + name) > -1)
                        {
                            string prog = "Progressions de la Save";
                            Send(handler, prog);
                        }
                        else
                        {
                            // Not all data received. Get more.  
                            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReadCallback), state);
                        }

                    }
                }
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch
            {

            }
        }

        private void Send(Socket handler, String data)//Function to send a message
        {
            try
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);// Convert the string data to byte data using ASCII encoding.  

                handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler); // Begin sending the data to the remote device. 

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        private void SendCallback(IAsyncResult ar)//Function to send a message a asynchronous
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;// Retrieve the socket from the state object.  

                int bytesSent = handler.EndSend(ar); // Complete sending the data to the remote device.  
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void DeleteBackup(string backupname)//Function that allows you to delete the backups that were selected by the user.
        {
            model.DeleteSave(backupname);
        }

        public void ModelFormat(bool format)
        {
            model.ModelFormat(format);
        }

        public string Check_buttonStatus()
        {

            return model.StatusButton;
        }

        public void PlayButton_click()
        {
            model.Play_click();
        }
        public void PauseButton_click()
        {
            model.Pause_click();
        }
        public void StopButton_click()
        {
            model.Stop_click();
        }
    }
}
