# Word Step Application

### Table of Contents
1. [Prerequisites](https://github.com/ddh4/WordStepApp#prerequisites)
2. [About](https://github.com/ddh4/WordStepApp#about)
3. [Usage](https://github.com/ddh4/WordStepApp#usage)

### Prerequisites
•	Visual Studio 2017
•	.NET 4.7 Framework

### About
A program in C#.NET with a command line interface which calls a procedure that requires four parameters. The program allows for the following configuration scenarios:

1. Uses four command line arguments; or 
2. Prompts the user to input the four command line arguments if they are not configured or the user selects to do so.

The four required parameters are:
1.	DictionaryFile – a path for a text file containing the four letter words.
2.	StartWord – a four letter word.
3.	EndWord – a four letter word.
4.	ResultFile – a path to a text file that will contain the result.

The problem is that given a starting word and an end word, can the starting word be transformed into the end word by swapping one character at a time where each intermediate word is present in the dictionary file. 

### Usage

Build the solution and run the application.

Press "Y" to run with command line arguments or "N" to enter parameters on request.
By entering parameters via user input, the algorithm can be run multiple times with different sets of words.
