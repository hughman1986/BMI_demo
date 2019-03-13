using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tuto
{
    public class Human
    {
        public string Name;
        public double weight;
        public double height;

        public string Gender;

        public Human(string name, string Gender, double weight, double height)
        {
            this.Name = name;
            this.weight = weight;
            this.height = height;
            this.Gender = Gender;
        }

        public double GetBMI()
        {
            double BMI = 0;
            BMI = weight / Math.Pow(height / 100, 2);
            return Math.Round(BMI, 3);
        }
    }
}