using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace PhoneMobile
{
    internal class Client
    {
        public string name = "Undefined";
        public string phone = "Undefined";
        public bool vip = false;
        public bool block = false;
        public int region = 0;
        public double sum = 10;
        
        protected Client Edit(string[] clientt)
        {
            
                Client m = new Client();
                m.name = clientt[0];
                m.phone = clientt[1];
                m.vip = clientt[2].Equals("true");
                m.block = clientt[3].Equals("true");
                m.region = Int32.Parse(clientt[4]);
                return m;
        }
    }

    internal class Clients : Client
    {
        public List<Client> clients = new List<Client>();
        private Log newLog = new Log();
        


        public void WorkWithFile(string Path)
        { 
            StreamReader sr = new StreamReader(Path);
            WorkInFile(sr);
            sr.Close();

        }

        void WorkInFile(StreamReader sr)
        {
            String? line;

            line = sr.ReadLine();

            while (line != null)
            {

                string[] words = line.Split(' ');
                this.clients.Add(Edit(words));

                Console.WriteLine(line);

                line = sr.ReadLine();

            }

        }

        

        public void WhatToDo() {
            Console.WriteLine("Что Вы хотите сделать?\n1 - Показать баланс; 2 - Позвонить; 3 - Показать историю звонков; 4 - Закончить");
            string? answer = Console.ReadLine();

            if (answer == "1")
            {
                ShowBalance();
            }
            else if (answer == "2")
            {
                Phonecaller();
            }
            else if (answer == "3")
            {
                newLog.ShowLog();
            }
            else if (answer == "4")
            {
                EndProgramm();
            }
            else throw new ArgumentOutOfRangeException(nameof(answer), "Check your answer pls and try again!");
        }

        void ShowBalance() {
            Console.WriteLine($"Текущий баланс {sum}");
        }

        void Phonecaller()
        {
            Console.WriteLine("Введите номер телефона на который хотите позвонить ");
            String? phone_num = Console.ReadLine();
            for (int i = 0; i < clients.Count(); i++)
            {
                if (phone_num == clients[i].phone)
                {
                    DoBlock(clients[i], phone_num);
                    break;
                }
                else if (i == clients.Count() - 1)
                {
                    Console.WriteLine($"Номер {phone_num} не найден");
                    newLog.ChangeLog($"{phone_num} not_found");
                }

            }
        }

        

        void EndProgramm() {
            System.Environment.Exit(1);
        }

        void DoBlock(Client el, String phone_num)
        {
            if (!el.block)
            {
                double cost_call = CheckCost(el);
                
                ChangeBalance(el,cost_call);
                
                
            }
            else
            {
                Console.WriteLine($"Совершен звонок на имя {el.name} Пользователь заблокирован");
                newLog.ChangeLog($"{ el.name} blocked");
            }
        }

        void ChangeBalance(Client el, double cost_call) {
            if (sum - cost_call < 0)
            {
                Console.WriteLine($"На вашем счету недостаточно средств.\nНа Вашем счету {sum} стоимость звонка {cost_call}\n" +
                    $"Попробуйте позвонить на другой номер или пополните баланс.");
                newLog.ChangeLog($"{el.name} {cost_call} failed");
            }
            else
            {
                sum -= cost_call;
                Console.WriteLine($"Совершен звонок на имя {el.name} стоимость звонка {cost_call}");
                newLog.ChangeLog($"{el.name} {cost_call} done");
                ShowBalance();
                
            }
        }

        private double CheckCost(Client el)
        {
            String PhoneNum4 = el.phone.Substring(0, 4);

            if (PhoneNum4 == "8800") return 0;
            double cost = 3;
            cost += 0.5 * el.region;
            if (el.vip) cost *= 1.5;
            return cost;
        }

    }

}


