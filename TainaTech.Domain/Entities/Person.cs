using System;

namespace TainaTech.Domain.Entities
{
    public class Person : AuditableEntity
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
