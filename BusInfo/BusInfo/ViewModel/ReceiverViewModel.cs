using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusInfo.ViewModel
{
    class ReceiverViewModel:ViewModelBase
    {
        private RelayCommand<string> _sendFeedbackCommand;
        private Action<bool> _feedback;
        private string _display;
        public string Display
        {
            get
            {
                return _display;
            }
            set
            {
                Set("Display", ref _display, value);

            }
        }

        public RelayCommand<string> SendFeedbackCommand
        {
            get
            {
                return _sendFeedbackCommand
                    ?? (_sendFeedbackCommand = new RelayCommand<string>(
                                          result =>
                                          {
                                              if (_feedback != null)
                                              {
                                                  _feedback(result == "1");
                                              }
                                          }));
            }
        }

        public ReceiverViewModel()
        {
            Messenger.Default.Register<string>(
                    this,
                    message =>
                    {
                        var display = string.Format(
                            "Message: {0} sent ",
                            message);
                        Display = display;
                    });
        }
        public void Unload()
        {
            Messenger.Default.Unregister<string>(this);
            Messenger.Default.Unregister<string>(this);
        }
    }
}
