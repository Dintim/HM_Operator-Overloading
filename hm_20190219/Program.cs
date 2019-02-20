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

    //1.	Создать класс с несколькими свойствами. Реализовать перегрузку операторов ==, != и Equals.
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

    //2.	Дан класс содержащий внутри себя массив. 
    //Реализовать перегрузку операторов < , > так, чтобы если сумма элементов массива 1 класса больше, возвращалось значение true и наоборот.
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

    //3.	Задание будет базироваться на примере в этом уроке. 
    //Необходимо реализовать второй вариант сложения денег – чтобы можно было суммировать деньги в разных валютах. 
    //Для этого создайте отдельный класс, который будет предоставлять механизм конвертации денег по заданному курсу. 
    //Кроме этого, перегрузите для класса Money оператор сравнения «==» (при перегрузке данного оператора, 
    //обязательной является и перегрузка противоположного ему оператора «!=»).
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

    //4.	Класс – одномерный массив. Дополнительно перегрузить следующие операции: 
    //* – умножение массивов; [] – доступ по индексу, int() – размер массива; == – проверка на равенство; <= – сравнение
    public class A
    {
        private int[] arr = null;
        public int this[int index]
        {
            get
            {
                return arr[index];
            }
            set
            {
                arr[index] = value;
            }
        }
        public A()
        {
            arr = new int[10];
        }
        public A(int num)
        {
            arr = new int[num];
        }
        public static A operator * (A x, A y)
        {
            if (x.arr.Length != y.arr.Length)
                throw new Exception("Размеры массивов не равны!");
            A z = new A(x.arr.Length);
            for (int i = 0; i < x.arr.Length; i++)
            {
                z.arr[i] = x.arr[i] * y.arr[i];
            }
            return z;
        }
        public static bool operator ==(A x, A y)
        {
            if (x.arr.Length != y.arr.Length)
                return false;
            else
            {
                for (int i = 0; i < x.arr.Length; i++)
                {
                    if (x.arr[i] != y.arr[i])
                        return false;
                }
                return true;
            }
        }

        public static bool operator !=(A x, A y)
        {
            if (x.arr.Length != y.arr.Length)
                return true;
            else
            {
                for (int i = 0; i < x.arr.Length; i++)
                {
                    if (x.arr[i] != y.arr[i])
                        return true;
                }
                return false;
            }
        }

        public static bool operator <=(A x, A y)
        {
            int sum1 = 0, sum2 = 0;
            for (int i = 0; i < x.arr.Length; i++)
            {
                sum1 += x.arr[i];
            }
            for (int i = 0; i < y.arr.Length; i++)
            {
                sum2 += y.arr[i];
            }
            return sum1 <= sum2;
        }

        public static bool operator >=(A x, A y)
        {
            int sum1 = 0, sum2 = 0;
            for (int i = 0; i < x.arr.Length; i++)
            {
                sum1 += x.arr[i];
            }
            for (int i = 0; i < y.arr.Length; i++)
            {
                sum2 += y.arr[i];
            }
            return sum1 >= sum2;
        }


    }
}
