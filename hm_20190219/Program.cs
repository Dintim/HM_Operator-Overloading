using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hm_20190219
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            //Human a = new Human("Ivan", "Ivanov", "123456789123");
            //Human b = new Human("Ivan", "Ivanov", "123456789123");
            //Console.WriteLine(a.Equals(b));

            //Arr a1 = new Arr();
            //for (int i = 0; i < 5; i++)
            //{
            //    int tmp = rnd.Next(10);
            //    a1.a.Add(tmp);
            //}
            //Arr a2 = new Arr();
            //for (int i = 0; i < 6; i++)
            //{
            //    int tmp = rnd.Next(10);
            //    a2.a.Add(tmp);
            //}
            //Console.WriteLine(a1<a2);

            Money a = new Money(100000, (currency)0);
            Money b = new Money(200, (currency)1);            
            a = a + new Money(new ConvertMoney(b, 375.5).Sum, a.Currency);
            Console.WriteLine(a.getInfo());
            
        }
        
    }

    public class Human
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        private string IIN_;
        public string IIN
        {
            get
            {
                return IIN_;
            }
            set
            {
                int k = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    if (char.IsLetter(value[i]))
                        k++;
                }
                if (value.Length != 12 || k!=0)
                    throw new ArgumentException("Некорректный ИИН!");
                IIN_ = value;
            }
        }
        public Human() { }
        public Human (string name, string surname, string iin)
        {
            this.Name = name;
            this.Surname = surname;
            this.IIN = iin;
        }
        public static bool operator == (Human a, Human b)
        {
            return (a.Name == b.Name && a.Surname == b.Surname && a.IIN == b.IIN);
        }
        public static bool operator !=(Human a, Human b)
        {
            return (a.Name != b.Name && a.Surname != b.Surname && a.IIN != b.IIN);
        }
        public override bool Equals(object obj)
        {
            if (obj is Human)
            {
                return (this.Name == ((Human)obj).Name && this.Surname == ((Human)obj).Surname && this.IIN == ((Human)obj).IIN);
            }
            else
                return false;
        }

    }

    public class Arr
    {
        public List<int> a = null;
            
        public static bool operator > (Arr x, Arr y)
        {
            int sum1 = 0, sum2 = 0;
            for (int i = 0; i < x.a.Count; i++)
            {
                sum1 += x.a[i];
            }
            for (int i = 0; i < y.a.Count; i++)
            {
                sum2 += y.a[i];
            }
            return (sum1 > sum2);
        }
        public Arr()
        {
            a = new List<int>();
        }
        public Arr(int num)
        {
            a = new List<int>(num);
        }
        public static bool operator < (Arr x, Arr y)
        {
            int sum1 = 0, sum2 = 0;
            for (int i = 0; i < x.a.Count; i++)
            {
                sum1 += x.a[i];
            }
            for (int i = 0; i < y.a.Count; i++)
            {
                sum2 += y.a[i];
            }
            return sum1 < sum2;
        }
    }

    public enum currency {kzt=0, usd=1, rur=2};    
    public class Money
    {
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public currency Currency { get; set; }

        public Money(decimal amount, currency cur)
        {
            this.Amount = amount;
            this.Currency = cur;
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency == b.Currency)
                return new Money(a.Amount + b.Amount, a.Currency);
            else
            {
                throw new Exception("Валюты счетов разные!");
            }
        }        

        public static bool operator == (Money a, Money b)
        {
            return (a.Number == b.Number);
        }
        public static bool operator != (Money a, Money b)
        {
            return (a.Number != b.Number);
        }

        public string getInfo()
        {
            return string.Format("{0} {1}", Amount, Currency);
        }
    }
    public class ConvertMoney
    {
        public decimal Sum { get; set; }
        public ConvertMoney(Money b, double kurs)
        {
            Sum = b.Amount * Convert.ToDecimal(kurs);
        }
    }
}
