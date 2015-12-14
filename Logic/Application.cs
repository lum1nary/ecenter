using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Logic.Structure;

namespace Logic
{
    public partial class Application
    {
        public Collection<Category> Categories { get; }
        public Collection<CompanyCustomer> CompanyCustomers { get; }
        public Collection<UnEmployed> UnEmployeds { get; }
        public Collection<Summary> Summaries { get; }
        public Collection<Vacancy> Vacancies { get; } 

        public Application()
        {
            Categories = new Collection<Category>();
            CompanyCustomers = new Collection<CompanyCustomer>();
            UnEmployeds = new Collection<UnEmployed>();
            Summaries = new Collection<Summary>();
            Vacancies = new Collection<Vacancy>();
        }
        #region TryAdd
        public ActionResult AddCategory (Category item) 
        {
            if (CategoryValidation(item))
            {
                if (Categories.Select(existItem => existItem.Name == item.Name).Any())
                {
                    
                    return ActionResult.AlreadyExists;
                }
                
                Categories.Add(item);
                return ActionResult.Success;
            }

            return ActionResult.InvalidInsert;
        }
        public ActionResult AddSummary(Summary item)
        {
            if (SummaryValidation(item))
            {
                if (Summaries.Select(existItem => existItem.Title == item.Title).Any()) return ActionResult.AlreadyExists;
                Summaries.Add(item);
                return ActionResult.Success;
            }
            return ActionResult.InvalidInsert;
        }
        public ActionResult AddVacancy(Vacancy item)
        {
            if (VacancyValidation(item))
            {
                if(Vacancies.Select(existItem => existItem.Name == item.Name).Any()) return ActionResult.AlreadyExists;
                Vacancies.Add(item);
                return ActionResult.Success;
            }
            return ActionResult.InvalidInsert;
        }
        public ActionResult AddUnEmployed(UnEmployed item)
        {
            if (UnemployedValidation(item))
            {
                if(UnEmployeds.Select(existItem => existItem.Name == item.Name).Any()) return ActionResult.AlreadyExists;
                UnEmployeds.Add(item);
                return ActionResult.Success;
            }
            return ActionResult.InvalidInsert;
        }

        public ActionResult AddCompanyCustomer(CompanyCustomer item)
        {
            if (CompanyValidation(item))
            {
                if(CompanyCustomers.Select(exitItem => exitItem.Name == item.Name).Any()) return ActionResult.AlreadyExists;
                CompanyCustomers.Add(item);
                return ActionResult.Success;
            }
            return ActionResult.InvalidInsert;
        }
        #endregion
        #region TryEdit


        public void EditCategory(string name)
        {
            var res = FindCategory(name); 
            if (res != -1)
            {
                Categories[res].Vacancies.Clear();
                Categories[res].Summaries.Clear();
                Categories.RemoveAt(res);
                
            }
        }



        public bool CategoryExists(string name)
        {
            return FindCategory(name) != -1;
        }

        public bool CompanyCustomerExists(string name)
        {
            return FindCompanyCustomer(name) != -1;
        }

        public bool SummaryExists(string name)
        {
            return FindSummary(name) != -1;
        }

        public bool VacancyExists(string name)
        {
            return FindVacancy(name) != -1;
        }

        public bool UnEmployedExists(string name)
        {
            return FindUnEmployed(name) != -1;
        }

        #region Find
        protected int FindCategory(string name)
        {
            var res = Categories.First(item => item.Name == name);
            return res != null ? Categories.IndexOf(res) : -1;
        }

        protected int FindCompanyCustomer(string name)
        {
            var res = CompanyCustomers.First(item => item.Name == name);
            return res != null ? CompanyCustomers.IndexOf(res) : -1;
        }

        protected int FindSummary(string title)
        {
            var res = Summaries.First(item => item.Title == title);
            return res != null ? Summaries.IndexOf(res) : -1;
        }

        protected int FindUnEmployed(string name)
        {
            var res = UnEmployeds.First(item => item.Name == name);
            return res != null ? UnEmployeds.IndexOf(res) : -1;
        }

        protected int FindVacancy(string name)
        {
            var res = Vacancies.First(item => item.Name == name);
            return res != null ? Vacancies.IndexOf(res) : -1;
        }
        #endregion




        public ActionResult TryEdit<T>(string key, T newItem) where T : class 
        {
            try
            {
                if (typeof (T) == typeof (Category))
                {
                    if (CategoryValidation(newItem as Category))
                    {
                        Categories[Categories.IndexOf(Categories.First(cat => cat.Name == key))] = newItem as Category;
                        return ActionResult.Success;
                    }
                    return ActionResult.InvalidInsert;

                }
                if (typeof (T) == typeof (Summary))
                {
                    if (SummaryValidation(newItem as Summary))
                    {
                        Summaries[Summaries.IndexOf(Summaries.First(cat => cat.Title == key))] = newItem as Summary;
                        return ActionResult.Success;
                    }
                    return ActionResult.InvalidInsert;
                }
                if (typeof (T) == typeof (Vacancy))
                {
                    if (VacancyValidation(newItem as Vacancy))
                    {
                        Vacancies[Vacancies.IndexOf(Vacancies.First(cat => cat.Name == key))] = newItem as Vacancy;
                        return ActionResult.Success;
                    }
                    return ActionResult.InvalidInsert;
                }
                if (typeof (T) == typeof (UnEmployed))
                {
                    if (UnemployedValidation(newItem as UnEmployed))
                    {
                        UnEmployeds[UnEmployeds.IndexOf(UnEmployeds.First(cat => cat.Name == key))] = newItem as UnEmployed;
                        return ActionResult.Success;
                    }
                    return ActionResult.InvalidInsert;

                }
                if( typeof (T) == typeof (CompanyCustomer))
                {
                    if (CompanyValidation(newItem as CompanyCustomer))
                    {
                        CompanyCustomers[CompanyCustomers.IndexOf(CompanyCustomers.First(cat => cat.Name == key))] = newItem as CompanyCustomer;
                        return ActionResult.Success;
                    }
                    return ActionResult.InvalidInsert;
                }
                return ActionResult.Failed;
            }   
            catch (Exception)
            {
                Console.WriteLine("Совпадений не найдено.");
                return ActionResult.Failed;
            }
        }
        #endregion
        #region TryDelete
        public ActionResult TryDelete<T>(string key)
        {
            try
            {
                if (typeof(T) == typeof(Category))
                {
                    if (Categories.ToList().Exists(match => match.Name == key))
                    {
                        Categories.Remove(Categories.First(item => item.Name == key));
                        return ActionResult.Success;
                    }
                    return ActionResult.NotFound;
                }
                if (typeof(T) == typeof(Summary))
                {
                    if (Summaries.ToList().Exists(match => match.Title == key))
                    {
                        Summaries.Remove(Summaries.First(item => item.Title == key));
                        return ActionResult.Success;
                    }
                    return ActionResult.NotFound;
                }
                if (typeof(T) == typeof(Vacancy))
                {
                    if (Vacancies.ToList().Exists(match => match.Name == key))
                    {
                        Vacancies.Remove(Vacancies.First(item => item.Name == key));
                        return ActionResult.Success;
                    }
                    return ActionResult.NotFound;
                }
                if (typeof(T) == typeof(UnEmployed))
                {
                    if (UnEmployeds.ToList().Exists(match => match.Name == key))
                    {
                        UnEmployeds.Remove(UnEmployeds.First(item => item.Name == key));
                        return ActionResult.Success;
                    }
                    return ActionResult.NotFound;
                }
                if (typeof(T) == typeof(CompanyCustomer))
                {
                    if (CompanyCustomers.ToList().Exists(match => match.Name == key))
                    {
                        CompanyCustomers.Remove(CompanyCustomers.First(item => item.Name == key));
                        return ActionResult.Success;
                    }
                    return ActionResult.NotFound;
                }
                return ActionResult.Failed;
            }
            catch(Exception)
            {
                return ActionResult.Failed;
            }
            
        }
        #endregion
        #region Display
        public string Display<T>(string value)
        {
            string result = string.Empty;
            try
            {
                if (typeof (T) == typeof (Category))
                {
                    var category = Categories.First(item => item.Name == value);
                    result += new string('=', 20) + "\n";
                    result += "Название:" + category.Name + "\n";
                    result += "Количество вакансий:" + category.Vacancies.Count + "\n";
                    result += "Количество резюме:" + category.Summaries.Count + "\n";
                    return result;
                }
                if (typeof(T) == typeof(Vacancy))
                {
                    var vacancy = Vacancies.First(item => item.Name == value);
                    result += new string('=', 20) + "\n";
                    result += "Название:" + vacancy.Name + "\n";
                    result += "Категория:" + vacancy.Category.Name + "\n";
                    result += "Описание:" + vacancy.Description + "\n";
                    result += "Компания работодатель:" + vacancy.Employer.Name + "\n";
                    result += "Дата добавления:" +  vacancy.Date.ToLongDateString() + "\n";
                    return result;
                }
                if (typeof(T) == typeof(Summary))
                {
                    var summary = Summaries.First(item => item.Title == value);
                    result += new string('=', 20) + "\n";
                    result += "Название:" + summary.Title + "\n";
                    result += "Категория:" + summary.Category.Name + "\n";
                    result += "Описание:" + summary.Description + "\n";
                    result += "Безработный:" + summary.UnEmployed.Name + "\n";
                    result += "Дата добавления:" + summary.Date.ToLongDateString() + "\n";
                    return result;
                }
                if (typeof(T) == typeof(UnEmployed))
                {
                    var unemployed = UnEmployeds.First(item => item.Name == value);
                    result += new string('=', 20) + "\n";
                    result += "Имя:" + unemployed.Name + "\n";
                    result += "Резюме:" + unemployed.Summary + "\n";
                    result += "Дата добавления:" + unemployed+ "\n";
                    return result;
                }
                if (typeof(T) == typeof(CompanyCustomer))
                {
                    var company = CompanyCustomers.First(item => item.Name == value);
                    result += new string('=', 20) + "\n";
                    result += "Название:" + company.Name + "\n";
                    result += "Описание:" + company.Description + "\n";
                    result += "Вакансии" + company.Vacancies.Count() + "\n";
                    result += "Регистрационный номер:" + company.RegisterNumber + "\n";
                    return result;
                }
                return result;
            }
            catch (Exception)
            {
                
                return "Запись для отображения не найдена";
            }          
        }
        #endregion
        #region DisplayAll
        public string[] DisplayAll<T>()
        {
            List<string> result = new List<string>();
            if (typeof (T) == typeof (Category))
            {
                Categories.ToList().ForEach(item => result.Add(Display<Category>(item.Name)));
            }
            if (typeof (T) == typeof (Summary))
            {
                Summaries.ToList().ForEach(item => result.Add(Display<Summary>(item.Title)));
            }
            if (typeof (T) == typeof (Vacancy))
            {
                Vacancies.ToList().ForEach(item =>result.Add(Display<Vacancy>(item.Name)));
            }
            if (typeof (T) == typeof (UnEmployed))
            {
                UnEmployeds.ToList().ForEach(item => result.Add(Display<UnEmployed>(item.Name)));
            }
            if (typeof (T) == typeof (CompanyCustomer))
            {
                CompanyCustomers.ToList().ForEach(item => result.Add(Display<CompanyCustomer>(item.Name)));
            }
            return result.ToArray();
        }

        #region Validation

        protected bool CategoryValidation(Category item)
        {
            return !string.IsNullOrEmpty(item?.Name);
        }

        protected bool SummaryValidation(Summary item)
        {
            return !string.IsNullOrEmpty(item?.Title) && !string.IsNullOrEmpty(item.Description) &&
                   !UnEmployeds.Contains(item.UnEmployed);
        }

        protected bool VacancyValidation(Vacancy item)
        {
            return string.IsNullOrEmpty(item?.Name) && Categories.Contains(item.Category) &&
                   CompanyCustomers.Contains(item.Employer) && string.IsNullOrEmpty(item.Description);
        }

        protected bool UnemployedValidation(UnEmployed item)
        {
            return !string.IsNullOrEmpty(item?.Name);
        }

        protected bool CompanyValidation(CompanyCustomer item)
        {
            return !string.IsNullOrEmpty(item?.Name) && string.IsNullOrEmpty(item.Description) &&
                   string.IsNullOrEmpty(item.RegisterNumber);
        }

        #endregion



    }
    #endregion
        
        public enum ActionResult {Success, InvalidInsert, Failed, AlreadyExists, NotFound}
}
