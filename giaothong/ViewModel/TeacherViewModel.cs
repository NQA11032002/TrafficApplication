using giaothong.Model;
using Microsoft.Win32;
using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace giaothong.ViewModel
{
    public class TeacherViewModel : BaseViewModel
    {
        private giaothongEntities db;
        private ObservableCollection<GIAOVIEN> _listTeacher;
        public ObservableCollection<GIAOVIEN> ListTeacher { get => _listTeacher; set => _listTeacher = value; }

        private string _maGV;
        public string MaGV { get => _maGV; set { _maGV = value; OnPropertyChanged(); } }

        private string _hoDem;
        public string HoDem { get => _hoDem; set { _hoDem = value; OnPropertyChanged(); } }

        private string _tenGV;
        public string TenGV { get => _tenGV; set { _tenGV = value; OnPropertyChanged(); } }

        private DateTime _ngaySinh;
        public DateTime NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }

        private string _soCCCD;
        public string SoCCCD { get => _soCCCD; set { _soCCCD = value; OnPropertyChanged(); } }

        private ComboBoxItem _noiCT;
        public ComboBoxItem NoiCT { get => _noiCT; set { _noiCT = value; OnPropertyChanged(); } }

        private int _gioiTinh;
        public int GioiTinh { get => _gioiTinh; set { _gioiTinh = value; OnPropertyChanged(); } }

        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }


        public int pageSize = 10; // Số phần tử trên mỗi trang
        private int _currentPage; // Trang hiện tại
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }

        private int _totalPages;
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }


        private int _selectedIndexStatus;
        public int SelectedIndexStatus { get => _selectedIndexStatus; set { _selectedIndexStatus = value; OnPropertyChanged(); if (SelectedIndexStatus == 0) { TrangThai = false; } else { TrangThai = true; }; } }


        private bool _isCheckedGender;
        public bool IsCheckedGender
        {
            get => _isCheckedGender;
            set
            {
                _isCheckedGender = value;
                OnPropertyChanged();

                if (IsCheckedGender)
                {
                    GioiTinh = 0;
                }
                else
                {
                    GioiTinh = 1;
                }
            }
        }


        private ComboBoxItem _tuyenDung;
        public ComboBoxItem TuyenDung { get => _tuyenDung; set { _tuyenDung = value; OnPropertyChanged(); } }

        private string _trinhDo_VH;
        public string TrinhDo_VH { get => _trinhDo_VH; set { _trinhDo_VH = value; OnPropertyChanged(); } }

        private string _nganh_CM;
        public string Nganh_CM { get => _nganh_CM; set { _nganh_CM = value; OnPropertyChanged(); } }

        private string _trinhDo_SP;
        public string TrinhDo_SP { get => _trinhDo_SP; set { _trinhDo_SP = value; OnPropertyChanged(); } }

        private bool _gV_LT;
        public bool GV_LT { get => _gV_LT; set { _gV_LT = value; OnPropertyChanged(); } }

        private bool _isCheckedGV_LT;
        public bool IsCheckedGV_LT { get => _isCheckedGV_LT; set { _isCheckedGV_LT = value; OnPropertyChanged(); if (IsCheckedGV_LT) GV_LT = true; else GV_LT = false; } }

        private bool _gV_TH;
        public bool GV_TH { get => _gV_TH; set { _gV_TH = value; OnPropertyChanged(); } }

        private bool _isCheckedGV_TH;
        public bool IsCheckedGV_TH { get => _isCheckedGV_TH; set { _isCheckedGV_TH = value; OnPropertyChanged(); if (IsCheckedGV_TH) GV_TH = true; else GV_TH = false; } }

        private string _soGCN;
        public string SoGCN { get => _soGCN; set { _soGCN = value; OnPropertyChanged(); } }

        private string _maSoGTVT;
        public string MaSoGTVT { get => _maSoGTVT; set { _maSoGTVT = value; OnPropertyChanged(); } }

        private string _maCSDT;
        public string MaCSDT { get => _maCSDT; set { _maCSDT = value; OnPropertyChanged(); } }

        private bool _trangThai;
        public bool TrangThai { get => _trangThai; set { _trangThai = value; OnPropertyChanged(); } }

        private int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

        private string _searchTeacher;
        public string SearchTeacher { get => _searchTeacher; set => _searchTeacher = value; }

        private string _selectedImage;
        public string SelectedImage { get => _selectedImage; set { _selectedImage = value; OnPropertyChanged(); } }

        private string _anhCD;
        public string AnhCD { get => _anhCD; set => _anhCD = value; }

        public ICommand selectionChanged { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand viewInsertTeacher { get; set; }
        public ICommand closeTeacherWindow { get; set; }
        public ICommand closeInsertTeacherWindow { get; set; }
        public ICommand selectedImage { get; set; }
        public ICommand insertTeacher { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }

        public TeacherViewModel()
        {
            ListTeacher = new ObservableCollection<GIAOVIEN>();
            NgaySinh = DateTime.Now;
            GioiTinh = 1;
            CurrentPage = 1;

            teachers();

            //close view teacher window
            closeTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //close view insert teacher window
            closeInsertTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //selectected change filter status teacher
            selectionChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                var selectedValue = changeSelectedStatus(SelectedIndex);
                teachers(selectedValue);
            });

            //find teacher by ID or CCCD
            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                var selectedValue = changeSelectedStatus(SelectedIndex);
                teachers(selectedValue, SearchTeacher);
            });

            //load view insert teacher 
            viewInsertTeacher = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                getMaxMaGV();

                p.Hide();
                InsertTeacher inserTeacher = new InsertTeacher();
                inserTeacher.ShowDialog();
                p.ShowDialog();
            });

            //selected image
            selectedImage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                try
                {
                    var dateNow = DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    var url = getUrlSelectedImage();
                    var nameFile = Path.GetFileName(url);
                    var exFile = nameFile.Split('.')[1];
                    nameFile = dateNow + "." + exFile;

                    string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    var index = currentDirectory.IndexOf("giaothong");
                    var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\giaothong\images\avatar";

                    string filePath = Path.Combine(newCurrentUrl, nameFile);

                    if (!File.Exists(filePath))
                    {
                        newCurrentUrl = newCurrentUrl + "\\" + nameFile;
                        File.Copy(url, newCurrentUrl);
                    }

                    var indexCurrentUrl = newCurrentUrl.IndexOf("images");

                    SelectedImage = newCurrentUrl;

                    AnhCD = "/Images/avatar/" + nameFile;
                }
                catch { }
            });

            //insert teacher
            insertTeacher = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                bool check = true;
                try
                {
                    var checkCSDT = checkExists(MaCSDT);

                    if (!checkCSDT)
                    {
                        check = false;
                        MessageBox.Show("Giáo viên hiện tại đang dạy ở cơ sở khác!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    var checkBirthDay = checkTypeDate(DateTime.Parse(NgaySinh.ToString()));

                    if (!checkBirthDay)
                    {
                        check = false;
                        MessageBox.Show("Giáo viên phải đủ từ 15 tuổi trở lên!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    var checkCCCD = checkExists(SoCCCD);

                    if (!checkCCCD)
                    {
                        check = false;
                        MessageBox.Show("Số CCCD/CMND đã tồn tại!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    var checkSoGCN = checkExistsSoGCN(SoGCN);

                    if (!checkSoGCN)
                    {
                        check = false;
                        MessageBox.Show("Số giấy chứng nhận không tồn tại hoặc đang thuộc sở hữu của giáo viên khác!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (!checkLength(HoDem, 25))
                    {
                        check = false;
                        MessageBox.Show("Họ đệm không hợp lệ hoặc phải dưới 25 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (!checkLength(TenGV, 25))
                    {
                        check = false;
                        MessageBox.Show("Tên đệm không hợp lệ hoặc phải dưới 15 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (!checkLength(Phone, 14))
                    {
                        check = false;
                        MessageBox.Show("Số điện thoại không hợp lệ!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (!checkLength(TrinhDo_VH, 10))
                    {
                        check = false;
                        MessageBox.Show("Trình độ văn hóa không hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (!checkLength(TrinhDo_SP, 10))
                    {
                        check = false;
                        MessageBox.Show("Trình độ sư phạm không hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (!checkLength(Nganh_CM, 20))
                    {
                        check = false;
                        MessageBox.Show("Ngành chuyên môn không hợp lệ hoặc phải dưới 20 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (!checkLength(MaSoGTVT, 5))
                    {
                        check = false;
                        MessageBox.Show("Mã sở giao thông vận tải không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (!checkLength(MaCSDT, 5))
                    {
                        check = false;
                        MessageBox.Show("Mã cơ sở đào tạo không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (IsCheckedGV_LT == false && IsCheckedGV_TH == false)
                    {
                        check = false;
                        MessageBox.Show("Vui lòng chọn loại giáo viên dạy!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                finally
                {
                    if (check)
                    {

                        GIAOVIEN gv = new GIAOVIEN()
                        {
                            MaGV = MaGV,
                            HoDem = HoDem,
                            TenGV = TenGV,
                            NgaySinh = NgaySinh,
                            SoCCCD = SoCCCD,
                            NoiCT = NoiCT.Content.ToString(),
                            GioiTinh = GioiTinh,
                            Phone = Phone,
                            TuyenDung = TuyenDung.Content.ToString(),
                            TrinhDo_SP = TrinhDo_SP,
                            TrinhDo_VH = TrinhDo_VH,
                            Nganh_CM = Nganh_CM,
                            GV_LT = GV_LT,
                            GV_TH = GV_TH,
                            SoGCN = SoGCN,
                            MaSoGTVT = MaSoGTVT,
                            MaCSDT = MaCSDT,
                            TrangThai = TrangThai,
                            AnhCD = AnhCD,
                            NgayCapNhat = DateTime.Now,
                        };

                        insert(gv);
                    }
                }
            });

            //next page
            nextPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    teachers();
                }
            });

            //previous page
            previousPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    teachers();
                }
            });
        }

        //insert teacher
        public void insert(GIAOVIEN giaovien)
        {
            using (db = new giaothongEntities())
            {
                db.GIAOVIENs.Add(giaovien);
                db.SaveChanges();

                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                var index = currentDirectory.IndexOf("giaothong");
                var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\giaothong";

                giaovien.AnhCD = newCurrentUrl + giaovien.AnhCD;

                ListTeacher.Add(giaovien);
            }
        }

        //check length avaliable
        public bool checkLength(string value, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (value.Length > length)
            {
                return false;
            }

            return true;
        }

        //check type date
        public bool checkTypeDate(DateTime date)
        {
            DateTime dateNow = DateTime.Now;

            if (dateNow.Year - date.Year < 15)
            {
                return false;
            }

            return true;
        }

        //check number GCN exists
        public bool checkExistsSoGCN(string soGCN)
        {
            using (db = new giaothongEntities())
            {
                var query = (from gcn in db.GIAOVIEN_GCN where gcn.SoGCN == soGCN select gcn).Count();
                var queryExist = (from gv in db.GIAOVIENs join gcn in db.GIAOVIEN_GCN on gv.SoGCN equals gcn.SoGCN where gcn.SoGCN == soGCN select gcn).Count();

                if (query != 1 || queryExist > 0)
                {
                    return false;
                }
            }

            return true;
        }

        //check teacher is teaching csdt another
        public bool checkExists(string value)
        {
            using (db = new giaothongEntities())
            {
                var query = (from gv in db.GIAOVIENs where gv.MaGV == MaGV && gv.MaCSDT == value || gv.SoCCCD == value select gv).Count();

                if (query > 0)
                {
                    return false;
                }
            }

            return true;
        }

        //get MaGV max
        public string getMaxMaGV()
        {
            if (ListTeacher.Count > 0)
            {
                var maxMaGV = ListTeacher.Max(p => p.MaGV).Trim();
                var arrMaGV = maxMaGV.Split('-');
                var dateNow = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");

                if (arrMaGV.Length > 0)
                {
                    MaGV = arrMaGV[0] + "-" + dateNow + "-" + (Int32.Parse(arrMaGV[2]) + 1);
                }
            }

            return MaGV;
        }

        //selected image get url
        public string getUrlSelectedImage()
        {
            string url = "";

            OpenFileDialog file = new OpenFileDialog();

            if (file.ShowDialog() == true)
            {
                url = file.FileName;
            }
            else
            {
                MessageBox.Show("Đường Dẫn Không Hợp Lệ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return url;
        }

        //get value with index combobox
        public string changeSelectedStatus(int index)
        {
            var value = "";

            switch (index)
            {
                case 1:
                    value = "Hợp đồng";
                    break;
                case 2:
                    value = "Biên chế";
                    break;
                case 3:
                    value = "Thời vụ";
                    break;
                case 4:
                    value = "Lý thuyết";
                    break;
                case 5:
                    value = "Thực hành";
                    break;
                default:
                    value = "Tất cả";
                    break;
            }

            return value;
        }

        //get list teacher

        public void teachers(string keyword = "tất cả", string searchKey = null)
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    ListTeacher.Clear();

                    if (keyword.ToLower() == "tất cả")
                    {
                        var teachers = from gv in db.GIAOVIENs select gv;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.MaGV == searchKey || gv.SoCCCD == searchKey select gv;
                        }

                        addValueToListTeacher(teachers.ToList());
                    }
                    else if (keyword.ToLower() == "hợp đồng" || keyword.ToLower() == "biên chế" || keyword.ToLower() == "thời vụ")
                    {
                        var teachers = from gv in db.GIAOVIENs where gv.TuyenDung == keyword select gv;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.TuyenDung == keyword && gv.MaGV == searchKey || gv.SoCCCD == searchKey select gv;
                        }

                        addValueToListTeacher(teachers.ToList());
                    }
                    else if (keyword.ToLower() == "lý thuyết")
                    {
                        var teachers = from gv in db.GIAOVIENs where gv.GV_LT == true select gv;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.GV_LT == true && gv.TuyenDung == keyword && gv.MaGV == searchKey || gv.SoCCCD == searchKey select gv;
                        }

                        addValueToListTeacher(teachers.ToList());
                    }
                    else if (keyword.ToLower() == "thực hành")
                    {
                        var teachers = from gv in db.GIAOVIENs where gv.GV_TH == true select gv;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.GV_TH == true && gv.GV_LT == true && gv.TuyenDung == keyword && gv.MaGV == searchKey || gv.SoCCCD == searchKey select gv;
                        }

                        addValueToListTeacher(teachers.ToList());
                    }
                }
                catch { };
            }
        }

        //add value to list teacher
        public void addValueToListTeacher(List<GIAOVIEN> teachers)
        {
            TotalPages = (int)Math.Ceiling((double)teachers.Count / pageSize);

            var list = teachers.Skip((CurrentPage - 1) * pageSize).Take(pageSize);
            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var index = currentDirectory.IndexOf("giaothong");
            var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\giaothong";

            list.ToList().ForEach(p =>
            {
                if (p != null)
                {
                    p.AnhCD = newCurrentUrl + p.AnhCD;
                    ListTeacher.Add(p);
                }
            });
        }
    }
}
