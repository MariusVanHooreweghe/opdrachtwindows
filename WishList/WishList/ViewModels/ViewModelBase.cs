using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WishList.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private DataTemplate _template;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase()
        {
            Template = GetTemplate();
        }

        public DataTemplate Template
        {
            get { return _template; }
            set { _template = value; RaisePropertyChanged("Template"); }
        }


        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DataTemplate GetTemplate()
        {
            string s = GetType().Name;
            return (DataTemplate)App.Current.Resources[s];
        }
    }
}
