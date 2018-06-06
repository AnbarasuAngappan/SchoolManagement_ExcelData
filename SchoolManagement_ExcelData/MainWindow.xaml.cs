using System;
using System.Collections.Generic;
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

namespace SchoolManagement_ExcelData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ExcelDataService _objExcelSer;
        Student _stud = new Student();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetStudentData();
        }

        private void GetStudentData()
        {
            _objExcelSer = new ExcelDataService();
            try
            {
                dataGridStudent.ItemsSource = _objExcelSer.ReadRecordFromEXCELAsync().Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefreshRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetStudentData();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        private void dataGridStudent_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                FrameworkElement stud_ID = dataGridStudent.Columns[0].GetCellContent(e.Row);
                if (stud_ID.GetType() == typeof(TextBox))
                {
                    _stud.StudentID = Convert.ToInt32(((TextBox)stud_ID).Text);
                }

                FrameworkElement stud_Name = dataGridStudent.Columns[1].GetCellContent(e.Row);
                if (stud_Name.GetType() == typeof(TextBox))
                {
                    _stud.Name = ((TextBox)stud_Name).Text;
                }

                FrameworkElement stud_Email = dataGridStudent.Columns[2].GetCellContent(e.Row);
                if (stud_Email.GetType() == typeof(TextBox))
                {
                    _stud.Email = ((TextBox)stud_Email).Text;
                }

                FrameworkElement stud_Class = dataGridStudent.Columns[3].GetCellContent(e.Row);
                if (stud_Class.GetType() == typeof(TextBox))
                {
                    _stud.Class = ((TextBox)stud_Class).Text;
                }

                FrameworkElement stud_Address = dataGridStudent.Columns[4].GetCellContent(e.Row);
                if (stud_Address.GetType() == typeof(TextBox))
                {
                    _stud.Address = ((TextBox)stud_Address).Text;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridStudent_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                bool IsSave = _objExcelSer.ManageExcelRecordsAsync(_stud).Result;
                if (IsSave)
                {
                    MessageBox.Show("Student Record Saved Successfully.");
                }
                else
                {
                    MessageBox.Show("Some Problem Occured.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _stud = dataGridStudent.SelectedItem as Student;                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
