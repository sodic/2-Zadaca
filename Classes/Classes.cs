using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{

    
    public enum Gender
    {
        Male, Female
    }

    public class StringMethods
    {
        public static String Reverse(String value)
        {
            char[] charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name.Trim();
            Jmbag = jmbag.Trim();
        }

        public static bool operator ==(Student a, Student b)
        {
            return a.Jmbag==b.Jmbag;
        }

        public static bool operator !=(Student a, Student b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return (obj is Student) ? (Student) obj==this : false;
        }

        public override int GetHashCode()
        {
            return int.Parse(StringMethods.Reverse(Jmbag)) ^ int.Parse(Jmbag);
        }
    }

    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }
    }
}
