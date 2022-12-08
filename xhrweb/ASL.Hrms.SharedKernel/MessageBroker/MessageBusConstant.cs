namespace ASL.Hrms.SharedKernel.MessageBroker
{
    public class MessageBusConstant
    {
        public const string NUMBER_OF_RETRY_ATTEMPTS_LEFT_HEADER_NAME = "x-number-of-retry-attempts-left";

        public const string DELAY_HEADER_NAME = "x-delay";

        public const string FAILED_TO_QUEUE_HEADER_NAME = "x-failed-to-queue";

        public const string DELAYED_RETRY_EXCHANGE_NAME_FORMAT = "{0}_Delaied_Retry";

        public const string FAILED_TO_QUEUE_NAME_FORMAT = "{0}_Failed";
    }
}
