using System;
using static System.Console;
using static JobForImages.View.Visualize;
using static JobForImages.Model.AuxiliaryFunctions;
using JobForImages.Model;
//using JobForImages.Controller;

namespace JobForImages
{  
    namespace Model
    {
        public static class AuxiliaryFunctions
        {
           public static void InputIntValue(string text, out int value, int lBound, int hBound)
            {
                bool notParsed;
                ForegroundColor = ConsoleColor.DarkCyan;
                Write(text);  //для мене так більш наочніше ніж в різних рядках
                do
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    if(notParsed = !int.TryParse(ReadLine(),out value)) 
                {                        
                        MyResetColor();
                        Write("Введене значення не є натуральним числом. Спробуйте ще раз! : ");
                }  
                else if (value<lBound || value>hBound)
                {
                        MyResetColor();
                        Write($"Значення має бути вiд {lBound} до {hBound}! Спробуйте ще раз! : ");  
                        notParsed = true;
                }                 
                } while (notParsed);
                MyResetColor();
            } 
            public static void MyResetColor()//потрібно для коректного відображення при запуску в PowerShell
            {                               //для PowerShell в IDE і так все коректно працює
                ResetColor();
                BackgroundColor = ConsoleColor.Black;
            }  
        }
/*
* в класі Job реалізовано функціонал завдання з обробки зображень
* клас Crew є елементом цього класу як команда робітників, хоча можна було і навпаки
* зробити Job як завдання для певної команда, що реалізує клас Crew
* defaultImageAmount - дефолтне значення для кількості зображень, використ при невірному 
* значені  ImageAmount
* imageAmount - кількість зображень для обробки 
* currentCrew - команда робітників, що виконує дане завдання
*/
        class Job        
        {
            public static int lowImageAmount = 1; //public тимчасово поки нема проперті
            public static int highImageAmount = 10000; //public тимчасово поки нема проперті
            static private int defaultImageAmount = 1000;
            public static int DefaultImageAmount { 
                
                get { return defaultImageAmount; } 
            }
            private int imageAmount;
            public int ImageAmount { 
                set
                {   //ця перевірка вже не актуальна, залишив може напряму десь буде встановлюватись
                    if (value < lowImageAmount || value > highImageAmount)
                    {
                        WriteLine("Недопустипе значення кількості зображень, задано значення по замовчуванню");
                        imageAmount = DefaultImageAmount;
                    }
                    else
                    {
                        imageAmount = value;
                    }
                }
                get { return imageAmount; } 
            }
            public Crew currentCrew;
            public Job(int amount, Crew crew)
            {
                ImageAmount = amount;
                currentCrew = crew;
            }
            static public void SetDefaultImageAmount()
            {
                InputIntValue("Введiть кiлькiсть зображень за замовчуванням: ",out defaultImageAmount,Job.lowImageAmount,Job.highImageAmount);            
            }
            public double calcTime()
            {
                //int taskAmount = 0;
                int imageAmount= ImageAmount;
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
* в класі Crew реалізовано функціонал команди робітників, які представлені класом Worker
* crew - масив з робітниками
* 
*/
        class Crew
        {           
            public static int lowWorkerAmount = 1; //public тимчасово поки нема проперті
            public static int highWorkerAmount = 100;//public тимчасово поки нема проперті
            public static int lowWorkerSpeed = 1; //public тимчасово поки нема проперті
            public static int highWorkerSpeed = 10; //public тимчасово поки нема проперті
            private static int defaultWorkerAmount = 3;
            public static int DefaultWorkerAmount { 
                
                get { return defaultWorkerAmount; } 
            }
            private int workerAmount;
            public int WorkerAmount { 
                set
                {   //ця перевірка вже не актуальна, залишив може напряму десь буде встановлюватись
                    if (value < lowWorkerAmount || value > highWorkerAmount) 
                    {
                        WriteLine("Недопустипе значення кількості працівників, задано значення по замовчуванню");
                        workerAmount = DefaultWorkerAmount;
                    }
                    else
                    {
                        workerAmount = value;
                    }
                }
                get { return workerAmount; } 
            }
            public Worker[] crew;
            public Crew(int workerAmount, int[] workerSpeed)
            {   
                WorkerAmount = workerAmount;
                crew = new Worker [WorkerAmount]; //створив масив що містить робітників            
                for (int i = 0; i < crew.Length; i++)
                {
                    if (workerSpeed.Length<workerAmount)//перестраховка на некоректні данні через не повністтю дороблений функціонал
                    {                                   
                      crew[i] = new Worker($"worker{i}",workerSpeed[i%workerSpeed.Length]);  
                    } 
                    else
                    {
                        crew[i] = new Worker($"worker{i}",workerSpeed[i]);
                    }
                    
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
                InputIntValue("Введiть кiлькiсть працiвникiв за замовчуванням: ",out defaultWorkerAmount, Crew.lowWorkerAmount, Crew.highWorkerAmount);            
            }
        }

    }
    namespace Controller
    {   
        class Program
        {        
            static void Main(string[] args)
            {
                Job job = null;
                char choice;
                BackgroundColor = ConsoleColor.Black;//потрібно для коректного відображення при запуску в PowerShell
                RefreshConsole(job);
                ShowNextStep();                
                do
                {
                    choice = ReadKey().KeyChar;
                    RefreshConsole(job);   
                    WriteLine();
                    
                    switch (choice)
                    {
                        case '1':
                            ShowDefaultSettings();
                            break;
                        case '2':
                            job = CreateNewJobManual();
                            RefreshConsole(job);
                            break;
                        case '3':
                            job = CreateStandartJob();
                            RefreshConsole(job);
                            break;
                        case '4':
                        break;
                        case '5':
                        break;
                        case '6':
                            SetAllDefaultSettings();
                            break;
                        case '7':
                        break;
                        case '8':
                        break;
                        case '9':
                        break;
                    }
                    ShowNextStep();
                } while (choice != 'x');
                //Job job = new Job(1000, crew);
                //Job job = CreateNewJobManual();
                //ShowDefaultSettings(Job.DefaultImageAmount,Crew.DefaultWorkerAmount);
            }
            private static Job CreateStandartJob()
            {
               int[] workerSpeed = { 2, 3, 4};
               return new Job(Job.DefaultImageAmount, new Crew(Crew.DefaultWorkerAmount, workerSpeed)); 
            }
            
            private static Job CreateRandomNewJob()
            {                
                Random genRandom = new Random();
                int imagesAmount = genRandom.Next(Job.lowImageAmount, Job.highImageAmount);
                int workersAmount = genRandom.Next(Crew.lowWorkerAmount, Crew.highWorkerAmount);
                int[] workerSpeed = new int[workersAmount];
                for (int i = 0; i < workersAmount; i++)
                {
                    workerSpeed[i] = genRandom.Next(Crew.lowWorkerSpeed,Crew.highWorkerSpeed);  
                }                 
                return CreateNewJob(imagesAmount, workersAmount, workerSpeed);
            }
            //цей метод лише для можливості 
            private static Job CreateNewJob(int imagesAmount, int workersAmount, int[] workerSpeed) =>
                new Job(imagesAmount, new Crew(workersAmount, workerSpeed));
            
            private static Job CreateNewJobManual()
            {           
                int imageAmount, workerAmount;            
                InputIntValue("Введiть кiлькiсть зображень для обробки: ",out imageAmount, Job.lowImageAmount, Job.highImageAmount);
                InputIntValue("Введiть кiлькiсть працiвникiв для виконання завдання: ",out workerAmount, Crew.lowWorkerAmount, Crew.highWorkerAmount);    
                return new Job(imageAmount, CreateNewCrewManual(workerAmount));
            }        
            private static void SetAllDefaultSettings()
            {
                Crew.SetDefaultWorkerAmount();
                Job.SetDefaultImageAmount();

            }
            private static Crew CreateNewCrewManual(int amount)
            {
                string choice;
                ForegroundColor = ConsoleColor.DarkGreen;
                WriteLine("Створюємо бригаду"); MyResetColor();               
                int[] workerSpeed = new int[amount];
                Random genRandom = new Random();
                Write("Згенерувати продуктивнiсть працiвникiв автоматично? (y/n): ");                              
                choice = ReadLine();
                if ("y" == choice || "Y" == choice || "" == choice)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        workerSpeed[i] = genRandom.Next(Crew.lowWorkerSpeed,Crew.highWorkerSpeed);
                    } 
                }
                else
                {
                    for (int i = 0; i < amount; i++)
                    {
                    InputIntValue($"Введiть продуктивнiсть {i+1} працiвника: ", out workerSpeed[i], Crew.lowWorkerSpeed, Crew.highWorkerSpeed);                  
                    }
                }
                return new Crew(amount, workerSpeed);
            }
            private static void ShowDefaultSettings()
            {
                ShowJobSettings("Параметри по замовчуванню для завдання:",Job.DefaultImageAmount,Crew.DefaultWorkerAmount);
            }
        }
    }
    namespace View
    {
        public static class Visualize
        {            
            public static void ShowJobSettings(string text, int images, int worker)
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine(text);
                MyResetColor(); Write("   -кiлькiсть зображень ");
                ForegroundColor = ConsoleColor.Yellow; WriteLine(images);
                MyResetColor(); Write("   -кiлькiсть працiвникiв ");
                ForegroundColor = ConsoleColor.Yellow; WriteLine(worker);                
                MyResetColor(); 
            }
            public static void ShowMenu()
            {
                ForegroundColor = ConsoleColor.DarkCyan;                
                WriteLine();
                WriteLine("|       1       |       2       |       3       |       4       |       5       |       6       |"); 
                WriteLine("|ShowDefSettings| NewJobManual  | NewStandartJob|  NewRandomJob |   Statistics  | SetDefSettings|");
                MyResetColor(); 
                WriteLine("________________________________________________________________________________________________"); 
                
            }
            public static void ShowJobInfo(in object job)
            {
                if (job == null)
                {
                    WriteLine();
                    WriteLine("Немає жодного поточного завдання. Ви можете його створити натиснувши 2, 3 або 4 ");                   
                }
                else
                {
                    ShowJobSettings("Параметри поточного завдання:", (job as Job).ImageAmount, (job as Job).currentCrew.WorkerAmount);                     
                    ShowWorkerSpeed(job);
                } 
                 WriteLine();               
                WriteLine("________________________________________________________________________________________________");
            }
            public static void ShowWorkerSpeed(in object job)
            {
                Worker[] crew = (job as Job).currentCrew.crew;
                Write("   -продуктивнiсть працiвникiв (№/хв.): ");
                int i;
                for (i = 0; i < crew.Length-1; i++)
                {
                    if (i%10 == 0)  WriteLine();                    
                    Write($"({i+1}/{crew[i].Speed}), "); 
                }
                 Write($"({i+1}/{crew[i].Speed})");
            }
                
            public static void ShowNextStep()
            {
                ForegroundColor = ConsoleColor.DarkGreen;                
                WriteLine(); 
                Write("Будь-ласка виберiть наступну команду або 'x' для виходу: ");
                MyResetColor();   
            }
             public static void RefreshConsole(in object job)
             {
                Clear();
                ShowMenu();
                ShowJobInfo(job); 
             }
        }
            
        
    }
}
