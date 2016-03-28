//*******************************************************************
//       Content Replacer		                                     
//                                                                   
//       Copyright © 2016 ByteScout - http://www.bytescout.com       
//       ALL RIGHTS RESERVED                                         
//                                                                   
//*******************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ContentReplacer
{
    class Program
    {
        static TextWriter cout = System.Console.Out;

        static void Main(string[] args)
        {
            // validate parameters
            if (args.Length < 3 || args.Length > 4)
            {
                showUsage();
                return;
            }

            // load parameters to local variables
            string inputFilename = args[0];
            string dataFilename = args[1];
            string outputFilename = args[2];
            string textToLookFor = "<!-- INSERT-CONTENT-HERE -->";
            if (args.Length == 4)
                textToLookFor = args[3];

            // text to hold the data
            string textInput;
            string textOuptut;
            string textToReplaceWith;

            // open input files
            try
            {
                cout.WriteLine("Reading input from file {0}", inputFilename);
                textInput = File.ReadAllText(inputFilename);

                cout.WriteLine("Reading data to replace with from file {0}", dataFilename);
                textToReplaceWith = File.ReadAllText(dataFilename).Trim();
            }
            catch (IOException e)
            {
                cout.WriteLine("Failed to open file. Error: {0}", e.Message);
                return;
            }

            // replace text
            cout.WriteLine("Replacing {0} in the input data", textToLookFor);
            textOuptut = textInput.Replace(textToLookFor, textToReplaceWith);

            // save tex
            try
            {
                cout.WriteLine("Saving output to file {0}", outputFilename);
                File.WriteAllText(outputFilename, textOuptut);
            }
            catch (IOException e)
            {
                cout.WriteLine("Failed to write to file. Error: {0}", e.Message);
            }

            cout.WriteLine("All done");
        }

        static void showUsage()
        {
            cout.WriteLine("Missing or invalid arguments\n");
            cout.WriteLine("Usage: ContentReplacer.exe inputFile dataFile outputFile [textToReplace]");
            cout.WriteLine("where");
            cout.WriteLine(" inputFile - full/ relative path to the source file");
            cout.WriteLine(" dataFile - full/ relative path to the file that contains replacement text");
            cout.WriteLine(" outputFile - full/ relative path to the output file");
            cout.WriteLine(" textToReplace - optional - text to replace in the inputFile with dataFile");
        }
    }
}
