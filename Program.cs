// See https://aka.ms/new-console-template for more information
using Medisys.Services;
using Medisys.UI;

var consoleMenu = new ConsoleMenu(new HospitalService());

consoleMenu.Run();
