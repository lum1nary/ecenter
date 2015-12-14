using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Structure;

namespace Logic
{
    public partial class Application
    {

        class ConsoleManager
        {
            private TextWriter Out { get; }
            private TextReader In { get; }
            private TextWriter Error { get; }

            private Application App { get; set; }

            private Action clear;
            private Func<bool> ContinueForm;
            private Func<string, string> InsertFieldForm;

            public ConsoleManager(TextWriter output, TextReader input, TextWriter error, Action clearAction)
            {
                Out = output;
                In = input;
                Error = error;
                clear = clearAction;
                ContinueForm = () =>
                {
                    string res;
                    do
                    {
                        Out.Write("Продолжить ввод?(д/н)");
                        res = In.ReadLine();
                    } while (res != "д" || res != "н");
                    return res == "д";
                };

                InsertFieldForm = (arg) =>
                {
                    string temp = null;
                    while (string.IsNullOrEmpty(temp))
                    {
                        Out.WriteLine($"Введите {arg}:");
                        temp = In.ReadLine();
                        if (!string.IsNullOrEmpty(temp)) continue;
                        ErrorReport("Пустое имя категории");
                        if (!ContinueForm.Invoke())
                        {
                            return null;
                        }
                    }
                    return temp;
                };



            }

           

            public Category AddCategoryProcess()
            {
                Out.Flush();
                Category cat = new Category();
                Out.WriteLine("Добавление Категории:");
                cat.Name = InsertFieldForm("Название");
                return string.IsNullOrEmpty(cat.Name) ? null : cat;
            }

            public CompanyCustomer AddCompanyCustomerProcess()
            {
                Out.Flush();
                CompanyCustomer com = new CompanyCustomer();
                Out.WriteLine("Добавлении Компании Работодателя:");
                com.Name = InsertFieldForm("Название");
                com.Description = InsertFieldForm("Описание");
                return string.IsNullOrEmpty(com.Name) || string.IsNullOrEmpty(com.Description) ? null : com;
            }

            public Summary AddSummaryProcess()
            {
                Out.Flush();
                Summary sum = new Summary();
                Out.WriteLine("Добавление вакансии:");
                sum.Title = InsertFieldForm("Название");
                var tempCategoryName = InsertFieldForm("Название категории");
                if (App.Categories.First(item => item.Name == tempCategoryName) != null)
                {
                    sum.Category = App.Categories.First(item => item.Name == tempCategoryName);
                }
            }
        }
    }
}
