using System;
using System.Collections.Generic;
using System.IO;

/* --------------------------------------------------- Record Class --------------------------------------------------- */
public class Record
{
    public string id, vehicleId, engineSize;
    public string forename, surname, birthDate, registration, manufacturer, model, regDate, intColour, helmetStorage, vehicleType;

    public Record(string lineId, string lineForename, string lineSurname, string lineBirthDate, string lineVehicleId, string lineReg,
        string lineMan, string lineModel, string lineEngineSize, string lineRegDate, string lineIntCol, string lineHelmet, string lineType)
    {
        id = lineId;
        forename = lineForename;
        surname = lineSurname;
        birthDate = lineBirthDate;
        vehicleId = lineVehicleId;
        registration = lineReg;
        manufacturer = lineMan;
        model = lineModel;
        engineSize = lineEngineSize;
        regDate = lineRegDate;
        intColour = lineIntCol;
        helmetStorage = lineHelmet;
        vehicleType = lineType;
    }
}

/* --------------------------------------------------- Reports --------------------------------------------------- */
public class AppReports
{
    /* ------------------------------------------ Functionality For Reports ----------------------------------------- */
    // All Records Report
    public static void allRecords(List<Record> myRecords)
    {
        // Display table headers
        Console.WriteLine(tableDivider("All"));
        Console.WriteLine(tableHeadings("All"));
        Console.WriteLine(tableDivider("All"));
        // Display the data
        displayResults(myRecords, "All");
    }

    // Engine size report
    public static void engineSize(List<Record> myRecords, int comparisonValue)
    {
        // Declare data
        int engineSize;
        List<Record> engineFilter = new List<Record>();
        // Loop through records for engine size
        for (int i = 0; i < myRecords.Count; i++)
        {
            // If the field has not been entered
            if (myRecords[i].engineSize == "")
            {
                // No data, do not process. Move to next iteration.
                continue;
            }
            // Parse engineSize field to integer
            engineSize = int.Parse(myRecords[i].engineSize);
            // Compare engine size
            if (engineSize > comparisonValue)
            {
                engineFilter.Add(myRecords[i]);
            }
        }
        // Add spacing to console
        Console.Write("\n \n \n ---------------------------------------- Engine Size Above 1100 ---------------------------------------- \n \n");
        // Display table headers
        Console.WriteLine(tableDivider("All"));
        Console.WriteLine(tableHeadings("All"));
        Console.WriteLine(tableDivider("All"));
        // Display results of age search
        displayResults(engineFilter, "All");
    }

    // Calculate age for age report
    public static int calculateAge(DateTime dateToday, string birthDateField)
    {
        // Get date from string
        DateTime birthDate = DateTime.Parse(birthDateField);
        // Age is todays year - birth year
        int age = dateToday.Year - birthDate.Year;
        // Make sure birthday has passed to add a year
        if (birthDate > dateToday.AddYears(-age))
        {
            age--;
        }
        return age;
    }

    // Customer age report
    public static void customerAge(List<Record> myRecords, int lowerAge, int higherAge)
    {
        // Store today's date
        DateTime dateToday = DateTime.Today;
        // Store matching records and unique ids in arrays
        List<Record> ageFilter = new List<Record>();
        List<string> customerID = new List<string>();
        int age;

        // For each record check if age is valid
        for (int i = 0; i < myRecords.Count; i++)
        {
            // Calculate customer age
            age = calculateAge(dateToday, myRecords[i].birthDate);
            // If the client is between lower and higher...
            if (age >= lowerAge && age <= higherAge)
            {
                string idToTest = myRecords[i].id;
                // If the client has not already been included... (avoid duplicate Client IDs)
                if (!customerID.Contains(idToTest))
                {
                    ageFilter.Add(myRecords[i]);
                    customerID.Add(myRecords[i].id);
                }
            }
        }
        // Add spacing to console
        Console.Write("\n \n \n ------------------------------------ Between {0} & {1} Years Old ------------------------------------ \n \n", lowerAge, higherAge);
        // Display table headers
        Console.WriteLine(tableDivider("Customer"));
        Console.WriteLine(tableHeadings("Customer"));
        Console.WriteLine(tableDivider("Customer"));
        // Display results of age search
        displayResults(ageFilter, "Customer");
    }

    // Registration date report
    public static void regDate(List<Record> myRecords, DateTime comparisonDate)
    {
        // Declare data
        List<Record> regFilter = new List<Record>();
        string regDateField;
        // Loop through records for reg date checks
        for (int i = 0; i < myRecords.Count; i++)
        {
            // If the field has not been entered...
            if (myRecords[i].regDate == "")
            {
                // No data, do not process. Move to next iteration.
                continue;
            }
            regDateField = myRecords[i].regDate;
            DateTime regDate = DateTime.Parse(regDateField);
            if (regDate < comparisonDate)
            {
                regFilter.Add(myRecords[i]);
            }
        }

        // Add spacing to console
        Console.Write("\n \n \n ---------------------------------------- Registered Before 2010 ---------------------------------------- \n \n");
        // Display table headers
        Console.WriteLine(tableDivider("All"));
        Console.WriteLine(tableHeadings("All"));
        Console.WriteLine(tableDivider("All"));
        // Display results of age search
        displayResults(regFilter, "All");
    }


    // Create horizontal table lines
    public static string tableDivider(string filter)
    {
        // Width of table
        int tableWidth;
        // Determine width by report (Fewer field in customer report)
        switch (filter)
        {
            case "Customer":
                tableWidth = 103;
                break;
            default:
                tableWidth = 272;
                break;
        }

        string divider = " ";
        // Loop through table until width reached
        for (int i = 0; i < tableWidth; i++)
        {
            divider += "-";
        }
        divider += " ";
        return divider;
    }
    // Create table headers
    public static string tableHeadings(string filter)
    {
        // Determine what to display based on report
        switch (filter)
        {
            case "Customer":
                return String.Format("|{0,-25}|{1,-25}|{2,-25}|{3,-25}|", "Customer ID", "Forename", "Surname", "DOB");
            default:
                return String.Format("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|{5,-20}|{6,-20}|{7,-20}|{8,-20}|{9,-20}|{10,-20}|{11,-20}|{12,-20}|",
                    "Customer ID", "Forename", "Surname", "DOB", "Vehicle ID",
                    "Registration", "Manufacturer", "Model", "Engine Size", "Registration Date",
                    "Interior Colour", "Helmet Storage", "Vehicle Type");
        }
    }
    // Display report results
    public static void displayResults(List<Record> myRecords, string filter)
    {
        // Sort by Customer ID
        myRecords.Sort((a, b) => string.Compare(a.id, b.id));
        // Display all information
        if (filter == "All")
        {
            // Loop through array to generate table
            for (int i = 0; i < myRecords.Count; i++)
            {
                string tableRow = String.Format("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|{5,-20}|{6,-20}|{7,-20}|{8,-20}|{9,-20}|{10,-20}|{11,-20}|{12,-20}|",
                    myRecords[i].id, myRecords[i].forename, myRecords[i].surname, myRecords[i].birthDate,
                    myRecords[i].vehicleId, myRecords[i].registration, myRecords[i].manufacturer, myRecords[i].model,
                    myRecords[i].engineSize, myRecords[i].regDate, myRecords[i].intColour, myRecords[i].helmetStorage,
                    myRecords[i].vehicleType);
                Console.WriteLine(tableRow);
                Console.WriteLine(tableDivider("All"));
            }
        }
        // Display only customer information
        if (filter == "Customer")
        {
            // Loop through array to generate table
            for (int i = 0; i < myRecords.Count; i++)
            {
                string tableRow = String.Format("|{0,-25}|{1,-25}|{2,-25}|{3,-25}|", myRecords[i].id, myRecords[i].forename, myRecords[i].surname, myRecords[i].birthDate);
                Console.WriteLine(tableRow);
                Console.WriteLine(tableDivider("Customer"));
            }
        }
    }
}

public class Interface
{
    /* ----------------------------------------- User Interface For Reports ----------------------------------------- */
    public static void allCustomersReport(List<Record> myRecords)
    {
        // Clear the console and load the headings
        Console.Clear();
        Interface.applicationHeader();
        // Run the report
        AppReports.allRecords(myRecords);
        Console.WriteLine("All Known Customers and Vehicles Report");
        Console.WriteLine("To return to the menu, press the 'M' key. To exit the application, enter 'Exit'");
        getInput(false, myRecords);
    }

    public static void ageReport(List<Record> myRecords)
    {
        // Clear the console and load the headings
        Console.Clear();
        Interface.applicationHeader();
        // Run the report with age parameters
        AppReports.customerAge(myRecords, 20, 30);
        Console.WriteLine("Age Report");
        Console.WriteLine("To return to the menu, press the 'M' key. To exit the application, enter 'Exit'");
        getInput(false, myRecords);
    }

    public static void regDateReport(List<Record> myRecords)
    {
        // Clear the console and load the headings
        Console.Clear();
        Interface.applicationHeader();
        // Run Report with date to compare against as paramater
        AppReports.regDate(myRecords, new DateTime(2010, 01, 01));
        Console.WriteLine("Registration Date Report");
        Console.WriteLine("To return to the menu, press the 'M' key. To exit the application, enter 'Exit'");
        getInput(false, myRecords);
    }

    public static void engineSizeReport(List<Record> myRecords)
    {
        // Clear the console and load the headings
        Console.Clear();
        Interface.applicationHeader();
        // Run report with engine size to compare against as parameter
        AppReports.engineSize(myRecords, 1100);
        Console.WriteLine("Engine Size Report");
        Console.WriteLine("To return to the menu, press the 'M' key. To exit the application, enter 'Exit'");
        getInput(false, myRecords);
    }

    public static void getInput(bool inMenu, List<Record> myRecords)
    {
        // Get user input for report ID
        string userInput = Console.ReadLine();
        // Not in the menu, only accept "M" or "Exit"
        if (inMenu == false)
        {
            switch (userInput.ToUpper())
            {
                case "M":
                    Console.Clear();
                    break;
                case "EXIT":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option selected. Please enter a valid option:");
                    getInput(inMenu, myRecords);
                    break;
            }
        }
        // Already in the menu, do not reload on "M" key
        else
        {
            switch (userInput.ToUpper())
            {
                case "1":
                    allCustomersReport(myRecords);
                    break;
                case "2":
                    ageReport(myRecords);
                    break;
                case "3":
                    regDateReport(myRecords);
                    break;
                case "4":
                    engineSizeReport(myRecords);
                    break;
                case "M":
                    Console.WriteLine("You are already at the menu. Please select an option");
                    getInput(inMenu, myRecords);
                    break;
                case "EXIT":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("You have entered an invalid input. Please enter a single digit from the list provided");
                    getInput(inMenu, myRecords);
                    break;
            }
        }
    }

    public static void applicationHeader()
    {
        Console.WriteLine("CRM Reports Application - Press The 'M' Key At Any Time To Return To Menu. Type 'Exit' to close the application.");
        Console.WriteLine("*--------------------------------------------------------------------------------------------------------------*");
    }

    public static void displayMenu()
    {
        applicationHeader();
        Console.WriteLine("Please Enter the Corresponding Number for the Report you Wish to Run [EG: '1', '2', Etc.]\n");
        Console.WriteLine("\t1. All Known Customers \n\t2. All Customers Aged 20 - 30 \n\t3. Vehicles Registered Before 2010 \n\t4. Vehicles with Engines Bigger than 1100CC");
    }
}

public class Program
{
    public static string validateDirectory(string currentDir)
    {
        // Read user input
        string userInput = Console.ReadLine();
        // Invalid response
        if (userInput.ToUpper() != "Y" && userInput.ToUpper() != "N")
        {
            Console.WriteLine("Please Enter a Valid Answer [Y/N]\n");
            return validateDirectory(currentDir);
        }
        else
        {
            if (userInput.ToUpper() == "Y")
            {
                // Directory is correct, proceed
                return currentDir;
            }
            else
            {
                // Allocate new value to directory
                Console.Write("Please enter full directory path (including filename and extension) [EG: 'C:\\Users\\Me\\CustomerInformation.csv']]\n");
                currentDir = Console.ReadLine();
                Console.WriteLine("Looking for file 'CustomerInformation.csv' with filepath '{0}'\nIs this the correct file path [Please Enter Y/N]?", currentDir);
                return validateDirectory(currentDir);
            }
        }
    }

    public static List <Record> importData(string directory)
    {
        // Declare storage array
        List<Record> myRecords = new List<Record>();

        using (var reader = new StreamReader(directory))
        {
            string headers = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                // Capture each line
                var line = reader.ReadLine();
                // Split values by ',' delimiter
                var values = line.Split(',');

                // Create new record object with data
                Record data = new Record(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9],
                    values[10], values[11], values[12]);
                myRecords.Add(data);
            }
        }

        return myRecords;
    }

    public static void Main()
    {
        // Launch App
        Console.WriteLine("Welcome to the CRM Reports Application \n*------------------------------------*");
        Console.WriteLine("Press Enter Key to Launch\n");
        Console.ReadLine();
        bool appRunning = true;

        // Confirm or locate Dir
        string directory = "C:\\Users\\Matt\\Desktop\\C#\\CRM_Test_App\\CustomerInformation.csv";
        LaunchApplication:
        Console.WriteLine("Looking for file 'CustomerInformation.csv' with filepath '{0}'\nIs this the correct file path [Please Enter Y/N]?\n", directory);
        directory = validateDirectory(directory);

        /* --------------------------------------------------- Data Import -------------------------------------------------------------- */
        try
        {
            List<Record> myRecords = importData(directory);
            Console.WriteLine("Data Imported Successfully.\nLoading Application Menu.");
            Console.WriteLine("\n*------------------------------------------------------------------------*");

            // Whilst the application is running
            while (appRunning)
            {
                // Load the menu
                Interface.displayMenu();
                Interface.getInput(true, myRecords);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured meaning the file could not be read.\nError Message: {0}\n", e.Message);
            goto LaunchApplication;
        }
    }
}
