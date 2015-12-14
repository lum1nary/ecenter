using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Structure;

namespace EmploymentCenter
{
    public class ApplicationAdapter
    {
        public Application Application { get; }

        public TextWriter Out
        {
            get
            {
                return _out ?? (_out = Console.Out);
            }
            set
            {
                if (_out == value) return;
                _out = value;
            }
        }
        public TextReader In
        {
            get
            {
                return _in ?? (_in = Console.In);
            }
            set
            {
                if (_in == value) return;
                _in = value;
            }
        }


        private TextWriter _out;
        private TextReader _in;



        public ApplicationAdapter(Application application)
        {
            Application = application;

        }


        #region Безработный

        public void AddUnEmployed()
        {
            _out.WriteLine("[Добавление безработного]");
            UnEmployed ue = new UnEmployed();
            var res = InsertFieldForm("имя");
            if (string.IsNullOrEmpty(res))
            {
                ErrorReport("Введено пустое имя, добавление не удалось");
                return;
            }
            ue.Name = res;
            Application.UnEmployeds.Add(ue);
            _out.WriteLine("OK");

        }

        public void EditUnEmployed()
        {
            _out.WriteLine("[Изменение безработного]");
            var res = InsertFieldForm("имя безработного");
            if (Application.UnEmployedExists(res))
            {
                var _new = InsertFieldForm("новое имя безработного");
                var _new_summ = InsertFieldForm("новое название вакансии \n (если резюме то же, оставьте поле пустым\n");
                if (!string.IsNullOrWhiteSpace(_new))
                {
                    Application.UnEmployeds.First(item => item.Name == res).Name = _new;
                    if (!string.IsNullOrEmpty(_new_summ))
                    {
                        if (Application.SummaryExists(Application.UnEmployeds.First(item => item.Name == _new).Summary.Title))
                        {
                           Application.Summaries.First(item => item.UnEmployed.Name == _new).Title = _new_summ;
                        }
                    }
                    _out.WriteLine("OK");
                    return;
                }
            }
        }

        #endregion

        #region Категория
        public void AddCategory()
        {
            _out.WriteLine("[Добавление категории]");
            Category cat = new Category();
            var res = InsertFieldForm("название категории");
            if (string.IsNullOrEmpty(res))
            {
                ErrorReport("Введено пустое имя, добавление не удалось");
                return;
            }
            cat.Name = res;
            Application.Categories.Add(cat);
            _out.WriteLine("OK");
        }

        public void EditCategory()
        {
            _out.WriteLine("[Изменение категории]");
            var res = InsertFieldForm("название требуемой категории");
            if (Application.CategoryExists(res))
            {
                var _new = InsertFieldForm("новое название категории");
                if (!string.IsNullOrEmpty(_new))
                {
                    Application.Categories.First(item => item.Name == res).Name = _new;
                    _out.WriteLine("OK");
                    return;
                }
                ErrorReport("Введено пустое имя, изменение не удалсоь");

            }
            ErrorReport("Категория с таким именем не найдена");
        }

        public void RemoveCategory()
        {
            _out.WriteLine("[Удаление категории]");
            var res = InsertFieldForm("название требуемой категории");
            if (Application.CategoryExists(res))
            {
                Application.Categories.Remove(
                    Application.Categories.First(item => item.Name == res));
                _out.WriteLine("OK");
            }
            ErrorReport("Категория с таким именем не найдена");
        }

        public void DisplayCategory()
        {
            _out.WriteLine("[Отображение категории]");
            var res = InsertFieldForm("Название требуемой категории");
            if (Application.VacancyExists(res))
            {
                DisplayCategoryNode(Application.Categories.First(item => item.Name == res));
                return;
            }
            ErrorReport("Категория с таким именем не существует");

            
        }

        public void DisplayAllCategories()
        {
            foreach (var category in Application.Categories)
            {
                DisplayCategoryNode(category);
            }
        }

        private void DisplayCategoryNode(Category node)
        {
            _out.WriteLine($"Название категории:{node.Name}");
            _out.WriteLine($"\tКоличество резюме:{node.Summaries.Count}");
            _out.WriteLine($"\tКоличетсво вакансий:{node.Vacancies.Count}");
        }
        #endregion

        #region Базовые методы 
        protected bool ContinueForm()
        {
            string res;
            do
            {
                _out.Write("Продолжить ввод?(д/н)");
                res = In.ReadLine();
            } while (res != "д" || res != "н");
            return res == "д";
        }

        public string InsertFieldForm(string fieldName)
        {
            string temp = null;
            while (string.IsNullOrEmpty(temp))
            {
                _out.WriteLine($"Введите {fieldName}:");
                temp = In.ReadLine();
                if (!string.IsNullOrEmpty(temp)) continue;
                ErrorReport("Введено пустое поле");
                if (!ContinueForm())
                {
                    return string.Empty;
                }
            }
            return temp;
        }


        private void ErrorReport(string message)
        {
            _out.WriteLine($@"[Ошибка]:{message}");
        }
        #endregion
    }
}
