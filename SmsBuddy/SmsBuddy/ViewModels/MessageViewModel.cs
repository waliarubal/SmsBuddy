using SmsBuddy.Models;

namespace SmsBuddy.ViewModels
{
    class MessageViewModel: DataViewModelBase
    {
        public MessageViewModel(): base("Message")
        {
            Icon = "/Assets/Images/sms-32.png";
        }

        protected override DataModelBase New(object argument)
        {
            return new MessageModel();
        }
    }
}
