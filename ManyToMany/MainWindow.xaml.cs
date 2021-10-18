using ManyToMany.DataAccess;
using ManyToMany.Entities;
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

namespace ManyToMany
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (MyContext context = new MyContext())
            {
                context.Database.CreateIfNotExists();

                //context.Students.Add(new Student
                //{
                //    Firstname = "John",
                //    Lastname = "Johnlu"
                //});

                //context.Students.Add(new Student
                //{
                //    Firstname = "Aysel",
                //    Lastname = "Mammadova"
                //});

                //context.Courses.Add(new Course
                //{
                //     CourseName="StepIT Academy",
                //      Address="Koroglu Rehimov"
                //});


                //context.Courses.Add(new Course
                //{
                //    CourseName = "LIB Academy",
                //    Address = "Caspian Plaza 6 "
                //});
                var student2 = context.Students.FirstOrDefault(s => s.Id == 3);
                var course1 = context.Courses.FirstOrDefault(c => c.Id == 1);
                var course2 = context.Courses.FirstOrDefault(c => c.Id == 3);

                //student2.Courses.Add(course1);
                //student2.Courses.Add(course2);

                //context.Courses.Add(new Course
                //{
                //    CourseName = "MeetUp Academy",
                //    Address = "Vurgun Residence"
                //});

                context.SaveChanges();


                var students = context.Students.ToList();
                StudentDataGrid.ItemsSource = students;

                var courses = context.Courses.ToList();
                CourseDataGrid.ItemsSource = courses;


            }

        }

        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                var item = StudentDataGrid.SelectedItem as Student;
                var student = context.Students.Include("Courses").FirstOrDefault(s => s.Id == item.Id);
                if (item != null)
                {
                    var cources = student.Courses.ToList();
                    CourseDataGrid.ItemsSource = cources;
                }
            }
        }

        private void CourseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                try
                {
                var item = CourseDataGrid.SelectedItem as Course;
                var course = context.Courses.Include("Students").FirstOrDefault(s => s.Id == item.Id);
                if (item != null)
                {
                    var students = course.Students.ToList();
                    StudentDataGrid.ItemsSource = students;

                }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
