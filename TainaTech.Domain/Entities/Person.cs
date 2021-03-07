using System;
using TainaTech.Domain.Common;
using TainaTech.Domain.Enums;

namespace TainaTech.Domain.Entities
{
    public class Person : AuditableEntity
    {
        public long PersonId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
