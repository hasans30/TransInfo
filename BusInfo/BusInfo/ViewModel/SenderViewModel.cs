using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;

namespace BusInfo.ViewModel
{
    class SenderViewModel:ViewModelBase
    {
        private RelayCommand<string> _sendCommand;
        
        public RelayCommand<string> SendCommand
        {
            get
            {
                return _sendCommand ??
                    (_sendCommand = new RelayCommand<string>(
                        text =>
                        {
                            var message = "Message1 sent from Sender View Model";

                            Messenger.Default.Send(message);
                        }));
            }
        }   
    }
}
