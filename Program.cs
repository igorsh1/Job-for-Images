using System;
using static System.Console;
using static JobForImages.ViewingLayer.View;

namespace JobForImages
{  
    /*
    * в класі Job реалізовано функціонал завдання з обробки зображень
    * клас Crew є елементом цього класу як бригада робітників, хоча можна було і навпаки
    * зробити Job як завдання для певної бригади, що реалізує клас Crew
    * defaultImageAmount - дефолтне значення для кількості зображень, використ при невірному 
    * значені  ImageAmount
    * imageAmount - кількість зображень для обробки 
    * currentCrew - бригада робітників, що виконує дане завдання
    */
    class Job
    {
        static private int defaultImageAmount = 1000;
        private int imageAmount;
        public int ImageAmount { 
            set
            {
                if (value < 1)
                {
                    WriteLine("Недопустипе значення кількості зображень, задано значення по замовчуванню");
                    imageAmount = defaultImageAmount;
                }
                else
                {
                    imageAmount = value;
                }
            }
            get { return imageAmount; } 
        }
        Crew currentCrew;
        public Job(int amount, Crew crew)
        {
            ImageAmount = amount;
            currentCrew = crew;
        }
        static public void SetDefaultImageAmount()
        {
            bool notParsed;
            Console.Write("Введiть кiлькiсть зображень за замовчуванням: ");            
            do
            {               
               if(notParsed = !int.TryParse(Console.ReadLine(),out defaultImageAmount)) 
               {
                    Console.Write("Введене значення не є натуральним числом. Спробуйте ще раз!");
               }  
               else if (defaultImageAmount<1)
               {
                    Console.Write("Значення має бути бiльше нуля! Спробуйте ще раз!");  
                    notParsed = true;
               }
            } while (notParsed);
        }
        public double calcTime()
        {
            //int taskAmount = 0;
            int imageAmount= this.ImageAmount;
            while (imageAmount>0)
            {
                
            }
            return 88.0;            

        }
    }
    class Worker
    {
        public int WorkingTime { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
        public Worker(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
    }
    /*
    * в класі Crew реалізовано функціонал бригади робітників, які представлені класом Worker
    * crew - масив з робітниками
    * 
    */
    class Crew
    {
        int[] workerSpeed = { 2, 3, 4};
        static private int defaultWorkerAmount = 3;
        private int workerAmount;
        public int WorkerAmount { 
            set
            {
                if (value < 1)
                {
                    WriteLine("Недопустипе значення кількості працівників, задано значення по замовчуванню");
                    workerAmount = defaultWorkerAmount;
                }
                else
                {
                    workerAmount = value;
                }
            }
            get { return workerAmount; } 
        }
        Worker[] crew;
        public Crew(int workerAmount)
        {
            crew = new Worker [workerAmount]; //створив масив що містить робітників            
            for (int i = 0; i < crew.Length; i++)
            {
                crew[i] = new Worker($"worker{i}",workerSpeed[i]);
            } 
        }        
        public Worker this[int index]
        {
            get
            {
                return crew[index];
            }
            set
            {
                crew[index] = value;
            }   
        }
        static public void SetDefaultWorkerAmount()
        {
            bool notParsed;
            Write("Введiть кiлькiсть працiвникiв за замовчуванням: ");            
            do
            {               
               if(notParsed = !int.TryParse(Console.ReadLine(),out defaultWorkerAmount)) 
               {
                    Console.Write("Введене значення не є натуральним числом. Спробуйте ще раз!");
               }  
               else if (defaultWorkerAmount<1)
               {
                    Console.Write("Значення має бути бiльше нуля! Спробуйте ще раз!");  
                    notParsed = true;
               }
            } while (notParsed);
        }
    }
    
    class Program
    {        
        static void Main(string[] args)
        {
            //Crew crew = new Crew(3);
            //Job job = new Job(1000, crew);
            CreateNewJobManual();
        }
        
        private static void CreateNewJobManual()
        {           
            int imageAmount, workerAmount;            
            InputIntValue("Введiть кiлькiсть зображень для обробки: ",out imageAmount);
            InputIntValue("Введiть кiлькiсть працiвникiв для виконання завдання: ",out workerAmount);    
        }        
        private static void SetAllDefaultSettings()
        {
            Crew.SetDefaultWorkerAmount();
            Job.SetDefaultImageAmount();

        }
    }
    namespace ViewingLayer
    {
        class View
        {
            public static void InputIntValue(string text, out int value)
            {
                bool notParsed;
                Console.Write(text);
                do
                {
                    if(notParsed = !int.TryParse(Console.ReadLine(),out value)) 
                {
                        Console.Write("Введене значення не є натуральним числом. Спробуйте ще раз! : ");
                }  
                else if (value<1)
                {
                        Console.Write("Значення має бути бiльше нуля! Спробуйте ще раз! : ");  
                        notParsed = true;
                }                 
                } while (notParsed);
            }  
            public static void ShowDefaultSettings()
            {

            }
        }
    }
}
