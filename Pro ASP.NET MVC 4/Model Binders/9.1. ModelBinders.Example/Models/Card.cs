namespace ModelBinders.Example.Models
{
    using System;

    public class Card
    {
        public Card()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}