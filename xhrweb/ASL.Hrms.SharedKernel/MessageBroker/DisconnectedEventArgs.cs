namespace ASL.Hrms.SharedKernel.MessageBroker
{
    public class DisconnectedEventArgs
    {
        public object Cause { get; set; }
        public ushort ClassId { get; set; }
        public string Initiator { get; set; }
        public ushort MethodId { get; set; }
        public ushort ReplyCode { get; set; }
        public string ReplyText { get; set; }
    }
}
