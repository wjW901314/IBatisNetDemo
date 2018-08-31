using System;

namespace Service.Model
{
    public class Person : Entity
    {
        private int id;
        private string firstName;
        private string lastName;
        private DateTime? birthDate;
        private double? weightInKilograms;
        private double? heightInMeters;

        public Person() { }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public DateTime? BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public double? WeightInKilograms
        {
            get { return weightInKilograms; }
            set { weightInKilograms = value; }
        }

        public double? HeightInMeters
        {
            get { return heightInMeters; }
            set { heightInMeters = value; }
        }
    }

}
