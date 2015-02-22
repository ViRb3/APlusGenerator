using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using QRCoder;

namespace APlusGenerator
{
    class Student
    {
        public string FirstName, LastName, Class;

        public Student(string firstName, string lastName, string @class)
        {
            FirstName = firstName;
            LastName = lastName;
            Class = @class;
        }

        public Student(string data)
        {
            string[] dataArray = Regex.Split(data, ":");

            if (dataArray.Length != 3)
                throw new Exception("Invalid FirstName:LastName:Class line!");

            FirstName = dataArray[0];
            LastName = dataArray[1];
            Class = dataArray[2];
        }

        public string GetData()
        {
            return string.Format("{0}:{1}:{2}", FirstName, LastName, Class);
        }
    }

    class Generator
    {
        private readonly List<Student> _students = new List<Student>(); 

        public Generator(Student student)
        {
            _students.Add(student);
        }

        public Generator(List<Student> students)
        {
            _students.AddRange(students);
        }

        public Tuple<Student, Bitmap[]>[] Generate(int count)
        {
            //TODO: Add encryption
            
            QRCodeGenerator generator = new QRCodeGenerator();
            List<Tuple<Student, Bitmap[]>> results = new List<Tuple<Student, Bitmap[]>>();

            foreach (var student in _students)
            {
                List<Bitmap> codes = new List<Bitmap>();

                for (int i = 0; i < count; i++)
                    codes.Add(generator.CreateQrCode(student.GetData(), QRCodeGenerator.ECCLevel.H).GetGraphic(20));

                results.Add(new Tuple<Student, Bitmap[]>(student, codes.ToArray()));
            }

            return results.ToArray();
        }
    }
}
