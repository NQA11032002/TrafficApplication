using giaothong.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace giaothong.ViewModel
{
    public class TeacherViewModel
    {
        private giaothongEntities db;
        private ObservableCollection<GIAOVIEN> listTeacher;
        public ObservableCollection<GIAOVIEN> ListTeacher { get => listTeacher; set => listTeacher = value; }

        public TeacherViewModel()
        {
            ListTeacher = new ObservableCollection<GIAOVIEN>();

            teachers();
        }


        //get list teacher

        public ObservableCollection<GIAOVIEN> teachers()
        {
            using(db = new giaothongEntities())
            {
                var teachers = from gv in db.GIAOVIENs
                               select gv;

                teachers.ToList().ForEach(p =>
                {
                    if(p != null)
                    {
                        ListTeacher.Add(p);
                    }
                    
                });
            }

            return ListTeacher;
        }
    }
}
