﻿using NLog;
// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";
// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");
string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
logger.Info(scrubbedFile);
MovieFile movieFile = new MovieFile(scrubbedFile);

Console.ForegroundColor = ConsoleColor.Green;

// LINQ - Where filter operator & Contains quantifier operator
var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
// LINQ - Count aggregation method
Console.WriteLine($"There are {Movies.Count()} movies from 1990");

Console.ForegroundColor = ConsoleColor.White;

logger.Info("Program ended");