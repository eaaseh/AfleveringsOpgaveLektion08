using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AfleveringsOpgaveLektion08 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ObservableCollection<User> users;

        public MainWindow() {
            InitializeComponent();
            users = new ObservableCollection<User>();
            listBox_users.ItemsSource = users;
        }


        private void OpenFile_Click(object sender, RoutedEventArgs e) {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true) {
                if ((myStream = openFileDialog1.OpenFile()) != null) {
                    using (myStream) {
                        string[] text = File.ReadAllLines(openFileDialog1.FileName);
                        foreach (string str in text) {
                            users.Add(new User(str));
                        }
                    }
                    barItem_count.Content = $"Person Count: {users.Count}";
                    barItem_time.Content = $"Last Updated: {DateTime.Now}";
                }
            }
        }

        private void listBox_users_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (listBox_users.SelectedIndex == -1) return;
            textBoxAge.DataContext = listBox_users.SelectedItem;
            textBoxName.DataContext = listBox_users.SelectedItem;
            textBoxScore.DataContext = listBox_users.SelectedItem;
            textBoxId.DataContext = listBox_users.SelectedItem;
        }

    }

    public class User {

        public User(string data) {
            // ID, Name, Age, Score
            var L = data.Split(';');
            Id = int.Parse(L[0]);
            Name = L[1];
            Age = int.Parse(L[2]);
            Score = int.Parse(L[3]);
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
        public override string ToString() {
            return String.Format($"{Id}\t{Name}, {Age}, {Score}");
        }

        public String ListBoxToString {
            get {
            return Name + ", " + Id;
            }
        }
    }
}
