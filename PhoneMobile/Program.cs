using PhoneMobile;
using System;

try
{

    //Pass the file path and file name to the StreamReader constructor
    Clients clients = new Clients();
    bool flag = true;
    clients.WorkWithFile(PhoneMobile.CONSTANTS.PATH);
    while (flag)
    {
        clients.WhatToDo();
    }
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}
finally
{
    Console.WriteLine("Executing finally block.");
}
