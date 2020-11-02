using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Atomus.WpfApp2.ViewModel
{
    public class MainWindowViewModel : MVVM.ViewModel
    {
        private string userID;//주석
        private string password;
        private string email;
        private List<MainWindowViewModel> list;

        private MainWindowViewModel selectedItem;

        [Required]
        [Display(Name ="아이디")]
        public string UserID 
        {
            get
            {
                return this.userID;
            }
            set
            {
                if (this.userID != value)
                {
                    this.userID = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        [Required]
        [Display(Name = "패스워드")]
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        [Required]
        [Display(Name = "이메일")]
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public List<MainWindowViewModel> UserList
        {
            get
            {
                return list;
            }
            set
            {
                if (this.list != value)
                {
                    this.list = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private bool isEnabledControl;
        public bool IsEnabledControl
        {
            get
            {
              return  this.isEnabledControl;
            }
            set
            {
                if (this.isEnabledControl != value)
                {
                    this.isEnabledControl = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public MainWindowViewModel SelectedItem
        {
            get 
            {
                return this.selectedItem;
            }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand InitCommand { get; set; }

        public MainWindowViewModel()
        {
            this.isEnabledControl = true;
            //this.selectedItem = new MainWindowViewModel();

            this.list = new List<MainWindowViewModel>();

            this.SaveCommand = new MVVM.DelegateCommand(
                () => { this.SaveProcess(); }
                , () => { return this.isEnabledControl; }
                );

            this.InitCommand = new MVVM.DelegateCommand(
                () => { this.InitProcess(); }
                , () => { return this.isEnabledControl; }
                );
        }

        private void InitProcess()
        {
            this.SelectedItem = new MainWindowViewModel();
        }

        private void SaveProcess()
        {
            MainWindowViewModel mainWindowViewModel;
            List<MainWindowViewModel> models;

            try
            {
                this.isEnabledControl = false;
                (this.SaveCommand as MVVM.DelegateCommand).RaiseCanExecuteChanged();

                if (this.selectedItem == null)
                    return;
                //System.Threading.Thread.Sleep(10000);

                mainWindowViewModel = new MainWindowViewModel();
                mainWindowViewModel.UserID = this.selectedItem.UserID;
                mainWindowViewModel.Password = this.selectedItem.Password;
                mainWindowViewModel.Email = this.selectedItem.Email;

                this.list.Add(mainWindowViewModel);

                models = this.list;
                this.UserList = null;
                this.UserList = models;
            }
            catch (Exception) { }
            finally
            {
                this.isEnabledControl = true;
                (this.SaveCommand as MVVM.DelegateCommand).RaiseCanExecuteChanged();
            }
        }
    }
}