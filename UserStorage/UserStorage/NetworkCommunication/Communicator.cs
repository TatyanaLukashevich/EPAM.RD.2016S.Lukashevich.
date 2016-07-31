using Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserStorage.Replication;

namespace UserStorage.NetworkCommunication
{
    public class Communicator : MarshalByRefObject
    {
        public event EventHandler<ChangedUserEventArgs> UserAdded;

        public event EventHandler<ChangedUserEventArgs> UserDeleted;

        private Sender sender;

        private Receiver receiver;

        private Task recieverTask;

        private CancellationTokenSource token;

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

        public Service Service { get; set; }

        public void Connect(IEnumerable<IPEndPoint> endPoints)
        {
            if (sender == null)
            {
                return;
            }

            sender.Connect(endPoints);
        }

        public void RunReceiver()
        {
            if (receiver == null)
            {
                return;
            }

            token = new CancellationTokenSource();
            recieverTask = Task.Run((Action)Receive, token.Token);
        }

        public void StopReceiver()
        {
            if (token.Token.CanBeCanceled)
            {
                token.Cancel();
            }
        }

        public void SendAdd(ChangedUserEventArgs args)
        {
            if (sender == null) return;

            Send(new Message
            {
                User = args.ChangedUser,
                MethodType = MethodType.Add
            });
        }
        public void SendDelete(ChangedUserEventArgs args)
        {
            if (sender == null) return;

            Send(new Message
            {
                User = args.ChangedUser,
                MethodType = MethodType.Delete
            });
        }

        private void Receive()
        {
            while (true)
            {
                if (token.IsCancellationRequested) return;
                var message = receiver.Receive();
                var args = new ChangedUserEventArgs
                {
                    ChangedUser = message.User
                };

                if (message.MethodType == 0)
                {
                    OnUserAdded(this, args);
                }
                else
                {
                    OnUserDeleted(this, args);
                }
            }
        }

        private void Send(Message message)
        {
            sender.Send(message);
        }

        protected virtual void OnUserDeleted(object sender, ChangedUserEventArgs args)
        {
            Service.Delete(args.ChangedUser);
            UserDeleted?.Invoke(sender, args);
        }

        protected virtual void OnUserAdded(object sender, ChangedUserEventArgs args)
        {
            Service.Add(args.ChangedUser);
            UserAdded?.Invoke(sender, args);
        }

        public void Dispose()
        {
            receiver?.Dispose();
            sender?.Dispose();
        }
    }
}
