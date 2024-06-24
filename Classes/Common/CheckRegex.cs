using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;

namespace CourseProject.Classes.Common
{
    public class CheckRegex
    {
        public static bool Match(string Pattern, string Input)
        {
            Match m = Regex.Match(Input, Pattern);
            return m.Success;


            //if (string.IsNullOrEmpty(Name.Text) || !Classes.Common.CheckRegex.Match("^[а-яА-я]*$", Name.Text))
            //{
            //    MessageBox.Show("Неправильно указано имя");
            //    return;
            //}
        }
    }
}
