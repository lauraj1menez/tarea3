using System;
using System.Collections.Generic;
namespace OOP.Classes
{ 
    // Observer interface
    public interface IBankObserver {
        void Update(double newBalance);
    }

    // Concrete Observer (Client) class
    public class Client : IBankObserver {
        private string name;

        public Client(string name) {
            this.name = name;
        }

        public void Update(double newBalance) {
            Console.WriteLine($"{name}: Bank balance updated. New balance: {newBalance}");
        }
    }

    // Subject (Observable) class
    public class Bank {
        private static Bank bankInstance;
        private double balance;
        private List<IBankObserver> observers;

        private Bank() {
            balance = 0;
            observers = new List<IBankObserver>();
        }

        public static Bank GetBank() {
            if (bankInstance == null) {
                bankInstance = new Bank();
            }
            return bankInstance;
        }

        public double GetBalance() {
            return balance;
        }

        public void Deposit(double amount) {
            if (amount > 0) {
                balance += amount;
                NotifyObservers();
                Console.WriteLine($"Deposited {amount} dollars.");
            }
            else {
                Console.WriteLine("Deposit amount must be greater than zero.");
            }
        }

        public void Borrow(double amount) {
            if (amount > 0 && balance >= amount) {
                balance -= amount;
                NotifyObservers();
                Console.WriteLine($"Borrowed {amount} dollars.");
            }
            else if (amount <= 0) {
                Console.WriteLine("Borrow amount must be greater than zero.");
            }
            else {
                Console.WriteLine("Insufficient balance to borrow.");
            }
        }

        public void Subscribe(IBankObserver observer) {
            observers.Add(observer);
        }

        public void Unsubscribe(IBankObserver observer) {
            observers.Remove(observer);
        }

        private void NotifyObservers() {
            foreach (var observer in observers) {
                observer.Update(balance);
            }
        }
    }


}

