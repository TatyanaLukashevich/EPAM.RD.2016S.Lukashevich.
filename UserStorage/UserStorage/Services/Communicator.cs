using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserStorage.Replication;

namespace UserStorage.NetworkCommunication
{
    /// <summary>
    /// Communicator bentween sender and receivers
    /// </summary>
    [Serializable]
    public class Communicator : MarshalByRefObject
    {
        private Sender sender;

        private Receiver receiver;

        private Task recieverTask;

        private CancellationTokenSource tokenSource;

        public Communicator(Sender sender, Receiver receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }

        public Communicator(Sender sender) : this(sender, null)
        {
        }

        public Communicator(Receiver receiver) : this(null, receiver)
        {
        }

        public event EventHandler<ChangedUserEventArgs> UserAdded;

        public event EventHandler<ChangedUserEventArgs> UserDeleted;

        /// <summary>
        /// Connect to listeners
        /// </summary>
        /// <param name="endPoints"></param>
        public void Connect(IEnumerable<IPEndPoint> endPoints)
        {
            if (sender == null)
            {
                return;
            }

            sender.Connect(endPoints);
        }


        /// <summary>
        /// Start message receiving
        /// </summary>
        public void RunReceiver()
        {
            if (receiver == null)
            {
                return;
            }
                
            tokenSource = new CancellationTokenSource();
            recieverTask = Task.Run((Action)Receive, tokenSource.Token);
        }

        /// <summary>
        /// Stop message receiving
        /// </summary>
        public void StopReceiver()
        {
            if (tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
        }

        /// <summary>
        /// Send message that user was added
        /// </summary>
        /// <param name="args"></param>
        public void SendAdd(ChangedUserEventArgs args)
        {
            if (sender == null)
            {
                return;
            }

            Send(new Message
            {
                User = args.ChangedUser,
                MethodType = MethodType.Add
            });
        }

        /// <summary>
        /// Send message that user was deleted
        /// </summary>
        /// <param name="args"></param>
        public void SendDelete(ChangedUserEventArgs args)
        {
            if (sender == null)
            {
                return;
            }

            Send(new Message
            {
                User = args.ChangedUser,
                MethodType = MethodType.Delete
            });
        }

        public void Dispose()
        {
            receiver?.Dispose();
            sender?.Dispose();
        }

        /// <summary>
        /// Receive message
        /// </summary>
        private void Receive()
        {
            while (true)
            {
                if (tokenSource.IsCancellationRequested)
                {
                    return;
                }

                var message = receiver.Receive();
                if (message == null)
                {
                    return;
                }

                var args = new ChangedUserEventArgs
                {
                    ChangedUser = message.User
                };

                switch (message.MethodType)
                {
                    case MethodType.Add: OnUserAdded(this, args);
                        break;
                    case MethodType.Delete: OnUserDeleted(this, args);
                        break;
                }
            }
        }


        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message"></param>
        private void Send(Message message)
        {
            sender.Send(message);
        }

        private void OnUserDeleted(object sender, ChangedUserEventArgs args)
        {
            UserDeleted?.Invoke(sender, args);
        }

        private void OnUserAdded(object sender, ChangedUserEventArgs args)
        {
            UserAdded?.Invoke(sender, args);
        }
    }
}
