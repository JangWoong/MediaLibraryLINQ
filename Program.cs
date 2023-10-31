﻿using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");
string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
logger.Info(scrubbedFile);

MovieFile movieFile = new MovieFile(scrubbedFile);
string choice = "";

do
{
    // display choices to user
    Console.WriteLine("1) Add Movie");
    Console.WriteLine("2) Display All Movies");
    Console.WriteLine("3) Find Movie");
    Console.Write("Enter to quit: ");
    // input selection
    choice = Console.ReadLine();

    logger.Info("User choice: {Choice}", choice);

    long choiceNum = 0;
    bool checkInput = long.TryParse(choice, out choiceNum);

    if(checkInput)
    {
        switch (choiceNum)
        {
            case 1: 
                // Add movie
                Movie movie = new Movie();
                // ask user to input movie title
                Console.Write("Enter movie title: ");
                // input title
                movie.title = Console.ReadLine();

                // verify title is unique
                if (movieFile.isUniqueTitle(movie.title))
                {
                    // input genres
                    string input;
                    do
                    {
                        // ask user to enter genre
                        Console.Write("Enter genre (or done to quit): ");
                        // input genre
                        input = Console.ReadLine();
                        // if user enters "done"
                        // or does not enter a genre do not add it to list
                        if (input != "done" && input.Length > 0)
                        {
                            movie.genres.Add(input);
                        }
                    } while (input != "done");

                    // specify if no genres are entered
                    if (movie.genres.Count == 0)
                    {
                        movie.genres.Add("(no genres listed)");
                    }
                    movieFile.AddMovie(movie);
                }
                else
                {
                    logger.Info("The title entered by the user already exists.");
                }
            break;

            case 2:
                // Display All Movies
                foreach(Movie m in movieFile.Movies)
                {
                    Console.WriteLine(m.Display());
                }
            break;

            case 3:
                // Find Movie
                Console.ForegroundColor = ConsoleColor.Green;

                // ask user to input movie title
                Console.Write("Enter find movie title: ");
                // input title
                string findTitle = Console.ReadLine();
                // LINQ - Where filter operator & Contains quantifier operator
                var Movies = movieFile.Movies.Where(m => m.title.Contains(findTitle));
                // LINQ - Count aggregation method
                Console.WriteLine($"There are {Movies.Count()} movies from {findTitle}");

                foreach(Movie m in Movies)
                {
                    Console.WriteLine($"  {m.title}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            break;
        }

    }
    else
    {
        logger.Info("The content entered by the user is not in the menu.");
        break;
    }

}while (choice == "1" || choice == "2" || choice =="3");


//Console.ForegroundColor = ConsoleColor.Green;

/*
// LINQ - Where filter operator & Contains quantifier operator
var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
// LINQ - Count aggregation method
Console.WriteLine($"There are {Movies.Count()} movies from 1990");
// LINQ - Any quantifier operator & Contains quantifier operator
var validate = movieFile.Movies.Any(m => m.title.Contains("(1921)"));
Console.WriteLine($"Any movies from 1921? {validate}");
// LINQ - Where filter operator & Contains quantifier operator & Count aggregation method
int num = movieFile.Movies.Where(m => m.title.Contains("(1921)")).Count();
Console.WriteLine($"There are {num} movies from 1921");
// LINQ - Where filter operator & Contains quantifier operator
var Movies1921 = movieFile.Movies.Where(m => m.title.Contains("(1921)"));
foreach(Movie m in Movies1921)
{
    Console.WriteLine($"  {m.title}");
}
// LINQ - Where filter operator & Select projection operator & Contains quantifier operator
var titles = movieFile.Movies.Where(m => m.title.Contains("Shark")).Select(m => m.title);
// LINQ - Count aggregation method
Console.WriteLine($"There are {titles.Count()} movies with \"Shark\" in the title:");
foreach(string t in titles)
{
    Console.WriteLine($"  {t}");
}

// LINQ - First element operator
var FirstMovie = movieFile.Movies.First(m => m.title.StartsWith("Z", StringComparison.OrdinalIgnoreCase));
Console.WriteLine($"First movie that starts with letter 'Z': {FirstMovie.title}");
*/

//Console.ForegroundColor = ConsoleColor.White;

logger.Info("Program ended");