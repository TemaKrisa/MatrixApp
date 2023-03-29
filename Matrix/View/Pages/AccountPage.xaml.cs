using Matrix.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage() => InitializeComponent();

        private async Task LoadData()
        {
            await using CosmeticshopContext dc = new();
            //Заполнение списка типов пользователя
            List<Usertype> result = await dc.Usertypes.ToListAsync();
            result.Insert(0, new Usertype { UsertypeName = "Все" });
            UsertypeCombo.ItemsSource = result;
            UsertypeCombo.SelectedIndex = 0;
            await UpdateData();
        }

        private async Task UpdateData()
        {
            using CosmeticshopContext dc = new();
            List<Account> accounts = await dc.Accounts.Include(u => u.UsertypeNavigation).ToListAsync();

            //Сортировка по фамилии
            if (!string.IsNullOrWhiteSpace(SurnameBox.Text)) accounts = accounts.Where(u => u.Surname.Contains(SurnameBox.Text)).ToList();
            //Сортировка по имени
            if (!string.IsNullOrWhiteSpace(NameBox.Text)) accounts = accounts.Where(u => u.Name.Contains(NameBox.Text)).ToList();
            //Сортировка по отчеству
            if (!string.IsNullOrWhiteSpace(MidnameBox.Text)) accounts = accounts.Where(u => u.Midname.Contains(MidnameBox.Text)).ToList();
            //Сортировка по почте
            if (!string.IsNullOrWhiteSpace(EmailBox.Text)) accounts = accounts.Where(u => u.Email.Contains(EmailBox.Text)).ToList();
            //Сортировка по типу пользователя
            if (UsertypeCombo.SelectedIndex != 0)
            {
                accounts = accounts.Where(u => u.Usertype == (UsertypeCombo.SelectedItem as Usertype).UsertypeId).ToList();
            }
            AccountTable.ItemsSource = accounts;
        }

        private void ChangeAccount() //Вызов окна редактирования выбранного аккаунта
        {
            AccountChange.Account1 = (Account)AccountTable.SelectedItem;
            AccountChange.Visibility = Visibility.Visible;
        }
        //Обновление данных при закрытии окна добавления аккаунта
        private async void AccountAdd_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (AccountAdd.Visibility == Visibility.Collapsed) { await UpdateData(); }
        }
        //Обновление данных при закрытии окна изменения аккаунта
        private async void AccountChange_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (AccountChange.Visibility == Visibility.Collapsed) { await UpdateData(); }
        }
        //Вызов окна добавления аккаунта
        private void AddBtn_Click(object sender, RoutedEventArgs e) => AccountAdd.Visibility = Visibility.Visible;
        //Вызов окна изменения аккаунта по двойному клику в таблице
        private void AccountTable_MouseDoubleClick(object sender, MouseButtonEventArgs e) => ChangeAccount();
        //Вызов окна добавления аккаунта через контекстное меню
        private void AddItem_Click(object sender, RoutedEventArgs e) => AccountAdd.Visibility = Visibility.Visible;
        //Вызов окна изменения аккаунта через контекстное меню
        private void ChangeItem_Click(object sender, RoutedEventArgs e) => ChangeAccount();
        //Фильтрация по типу пользователя
        private async void UsertypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) => await UpdateData();
        //Фильтрация по фамилии
        private async void SurnameBox_TextChanged(object sender, TextChangedEventArgs e) => await UpdateData();
        //Вызов загрузки данных при открытии страницы
        private async void AccountPage_Loaded(object sender, RoutedEventArgs e) => await LoadData();
    }
}