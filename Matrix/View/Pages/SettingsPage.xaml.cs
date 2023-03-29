﻿using Matrix.Class;
using Matrix.Setting;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            ThemeCombo.SelectedIndex = AppSettings.Default.Theme switch // Определение текущей темы
            {
                "Dark" => 1,
                "Light" => 0,
                _ => ThemeCombo.SelectedIndex
            };
        }

        private void DarkItem_Selected(object sender, RoutedEventArgs e) //Установка тёмной темы
        {
            AppSettings.Default.Theme = "Dark";
            AppSettings.Default.Save();
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri($"/Style/Themes/{AppSettings.Default.Theme}.xaml", UriKind.RelativeOrAbsolute) });
        }

        private void LightItem_Selected(object sender, RoutedEventArgs e) //Установка светлой темы
        {
            AppSettings.Default.Theme = "Light";
            AppSettings.Default.Save();
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri($"/Style/Themes/{AppSettings.Default.Theme}.xaml", UriKind.RelativeOrAbsolute) });
        }

        private void AboutProgramLink_Click(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new AboutProgramPage());

        private void HelpLink_Click(object sender, RoutedEventArgs e) => Navigation.OpenHelp(); //Открытие файла справки
    }
}
