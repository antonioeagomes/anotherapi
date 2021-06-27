using System.Text;

namespace Another.Business.Notifications
{
    public class Notification
    {
        public string Message { get; set; }
        public Notification(string message)
        {
            Message = message;
        }
    }
}
