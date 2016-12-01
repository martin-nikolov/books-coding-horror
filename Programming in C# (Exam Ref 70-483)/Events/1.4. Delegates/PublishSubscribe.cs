namespace ProgrammingInCSharp.Delegates
{
    using System;

    public class PublishSubscribe
    {
        private static void Main()
        {
            var subscriberOne = new SubscriberOne();
            var subscriberTwo = new SubscriberTwo();

            var publisher = new Publisher();
            publisher.Add(subscriberOne);
            publisher.Add(subscriberTwo);

            publisher.Invite();
        }
    }

    public class Publisher
    {
        private Publish publisher;

        public void Add(ISubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException($"{nameof(subscriber)} cannot be null.");
            }

            if (this.publisher == null)
            {
                this.publisher = subscriber.Action;
            }
            else
            {
                this.publisher += subscriber.Action;
            }
        }

        public void Invite()
        {
            this.publisher?.Invoke();
        }

        private delegate void Publish();
    }

    public class SubscriberOne : ISubscriber
    {
        public void Action()
        {
            Console.WriteLine("SubscriberOne");
        }
    }

    public class SubscriberTwo : ISubscriber
    {
        public void Action()
        {
            Console.WriteLine("SubscriberTwo");
        }
    }

    public interface ISubscriber
    {
        void Action();
    }
}