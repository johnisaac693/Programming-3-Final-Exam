using System;
using System.Diagnostics.SymbolStore;
using System.Collections.Generic;
using System.Linq;
namespace RizonFinals
{
    /*
	1. Main Menu as the starting page
	2. One can start encoding their grades, which results in the program asking for the student name
	3. If the program fails during the encoding process, the student name is removed in order to avoid index errors
	4. The program also offers the user to view the students they have already graded
	5. Also within that vein, they can go back to the main menu to remove a student they wish to remove. The changes are reflected when the
	user selects 2 from the main menu after removing
	6. There is also an exit prompt in the main menu
	7. The name method does not accept whitespaces and thus will throw you back to the main menu
	*/

    class ProgramBody
    {
        private const string Format = "     {0}      {1}";

        //Public list so that the grades can be added into the list and be viewed later.
        public static List<double> Grades = new List<double>();
        public static List<string> Names = new List<string>();

        static void Main() //Main Program
        {


            MainMenu();
            char menuselect = ReadMenuSelect();

            while (menuselect != 'x')
            {
                switch (menuselect)
                {
                    case '1':

                        Console.WriteLine();
                        Console.WriteLine("Student Calculator");
                        Console.WriteLine("*******************");

                        Name();
                        ExamCalculations();
                        MainMenu();
                        menuselect = ReadMenuSelect();
                        break;

                    case '2':

                        if (Names.Count == 0)
                        {

                            Console.WriteLine("\nThere is nothing here\n");
                            Thread.Sleep(100);
                            MainMenu();
                            menuselect = ReadMenuSelect();
                        }
                        else
                        {
                            Console.WriteLine();
                            NameGradesView();
                            MainMenu();
                            menuselect = ReadMenuSelect();
                        }
                        break;

                    case '3':
                        if (Names.Count == 0)
                        {
                            Console.WriteLine("\nThere is nothing here\n");
                            Thread.Sleep(100);
                            MainMenu();
                            menuselect = ReadMenuSelect();
                        }
                        else
                        {
                            Console.WriteLine();
                            StudentRemove();
                            Thread.Sleep(100);
                            MainMenu();
                            menuselect = ReadMenuSelect();

                        }
                        break;



                    default:
                        menuselect = 'x';
                        break;
                }
            }



        }//End of Main Method

        static void NameGradesView()
        {
            Console.WriteLine();
            Console.WriteLine("{0,-6}{1,6}", "NAME", "GRADE");
            for (int i = 0; i < Names.Count; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine("{0,-6}{1,6}", Names[i], Grades[i]);
            }


        }

        static void Name()
        {
            Console.Write("Enter Student Name: ");
            string studentname = Console.ReadLine();
            string studentnameupper = studentname.ToUpper();
            bool isstudentwhitespace = IsNameWhiteSpaces(studentnameupper);

            if (isstudentwhitespace == false)
            {
                Names.Add(studentnameupper);
            }
            else
            {
                Console.WriteLine("Name cannot be whitespaces");
                Main();
            }
        }

        static void MainMenu() //Method for the Main Menu
        {
            Console.WriteLine();
            Console.WriteLine("Press 1 to Encode Written Works");
            Console.WriteLine("Press 2 to View Students and Grades");
            Console.WriteLine("Press 3 to Remove a Student");
            Console.WriteLine("Press X to Exit");
        }

        static void ExamCalculations() //The method when the user selects 1 from the main menu
        {
            try
            {
                int worknum;
                string confirm;
                Console.WriteLine("Welcome to the Student Written Works Calculator");
                Console.Write("How many works do your wish to grade?: ");

                //Program reads user input for number of works
                worknum = Convert.ToInt32(Console.ReadLine());


                Console.Write("Do your works share the same number of items? (Yes or No): ");
                //Program reads for yes or no input
                confirm = Console.ReadLine();
                string uppercaseconfirm = confirm.ToUpper();

                switch (uppercaseconfirm)
                {
                    case "YES":
                        ConstantItemScore(worknum);
                        break;

                    case "NO":
                        DynamicItemScore(worknum);
                        break;


                    default:
                        Console.WriteLine("Yes or No Only");
                        Names.RemoveAt(Names.Count - 1);
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid Input Detected!");
                Names.RemoveAt(Names.Count - 1);
            }
        }

        static void ConstantItemScore(int x) //If the Written Works share the same number of items
        {

            try
            {
                int Constantitems;
                int score;
                double sum = 0.0;
                Console.Write("What is your constant score?: ");
                Constantitems = Convert.ToInt32(Console.ReadLine());
                for (int i = 1; i <= x; i++)
                {
                    Console.Write("{0}. Written Work Score: ", i);
                    score = Convert.ToInt32(Console.ReadLine());
                    sum += score;

                    if (score > Constantitems)
                    {
                        Console.WriteLine("Score cannot be higher than item limit. Returning to Main Menu");
                        Names.RemoveAt(Names.Count - 1);
                        return;
                    }

                }

                double itemall = Constantitems * x;
                double grade = (sum / itemall) * 100;
                Console.WriteLine($"Your grade is {grade}");
                Grades.Add(grade);
            }
            catch
            {
                Console.WriteLine("An Invalid Input was Detected");
            }


        }

        static void DynamicItemScore(int x) //If the Written Works have different number of items
        {

            try
            {
                int Dynamicitems = 0;
                int score;
                double sum = 0.0;
                double scoresum = 0.0;

                for (int i = 1; i <= x; i++)
                {
                    Console.Write("{0}. Written Work Score: ", i);
                    score = Convert.ToInt32(Console.ReadLine());


                    Console.Write("{0}. Score Over: ", i);
                    Dynamicitems = Convert.ToInt32(Console.ReadLine());
                    sum += score;
                    scoresum += Dynamicitems;

                    if (score > Dynamicitems)
                    {
                        Console.WriteLine("Score cannot be higher than constant item. Returning to Main Menu");
                        Names.RemoveAt(Names.Count - 1);
                        return;
                    }
                }




                double grade = (sum / scoresum) * 100;
                Console.WriteLine($"Your grade is {grade}");
                Grades.Add(grade);
            }
            catch
            {
                Console.WriteLine("An invalid input was detected");
            }


        }

        static void StudentRemove() // Removes a Student
        {

            Console.WriteLine("\nList of Students: ");
            NameGradesView();

            Console.Write("Type the Student Name to Remove: ");
            string nameremove = Console.ReadLine();
            string nameremoveupper = nameremove.ToUpper();

            if (Names.Contains(nameremoveupper))
            {
                Grades.RemoveAt(Names.IndexOf(nameremoveupper));
                Names.Remove(nameremoveupper);
                Console.WriteLine("Student Removed");

            }
            else
            {
                Console.WriteLine("Student does not exist or name is typed incorrectly");
            }


        }

        static char ReadMenuSelect() //Menu Selection
        {
            Console.Write("Enter your Input: ");
            char input = Console.ReadLine()[0];
            Char.ToLower(input);
            return input;
        }

        static bool IsNameWhiteSpaces(string x) // Checks if name input is only comprised of whitespaces or null
        {
            bool namecheck = string.IsNullOrWhiteSpace(x);

            if (namecheck == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }//End of Class
}//End of Program