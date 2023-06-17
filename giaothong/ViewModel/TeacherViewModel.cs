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

        private List<province_city> _listCity;
        public List<province_city> ListCity { get => _listCity; set => _listCity = value; }

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

        private string _noiCT;
        public string NoiCT { get => _noiCT; set { _noiCT = value; OnPropertyChanged(); } }

        private int _gioiTinh;
        public int GioiTinh { get => _gioiTinh; set { _gioiTinh = value; OnPropertyChanged(); } }

        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }

        private string _trinhDo_CM;
        public string TrinhDo_CM { get => _trinhDo_CM; set => _trinhDo_CM = value; }

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


        private string _tuyenDung;
        public string TuyenDung { get => _tuyenDung; set { _tuyenDung = value; OnPropertyChanged(); } }

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
        public string AnhCD { get => _anhCD; set { _anhCD = value; OnPropertyChanged(); } }


        private int _selectedIndexNoiCT;
        public int SelectedIndexNoiCT
        {
            get => _selectedIndexNoiCT; set
            {
                _selectedIndexNoiCT = value; OnPropertyChanged();

                int index = 0;

                for (index = 0; index < ListCity.Count(); index++)
                {
                    if (index == SelectedIndexNoiCT)
                    {
                        NoiCT = ListCity[index].name;
                        break;
                    }

                    index++;
                }
            }
        }

        private int _selectedIndexTD;
        public int SelectedIndexTD
        {
            get => _selectedIndexTD; set
            {
                _selectedIndexTD = value;
                OnPropertyChanged();

                if (SelectedIndexTD == 0)
                {
                    TuyenDung = "Hợp đồng";
                }
                else if (SelectedIndexTD == 1)
                {
                    TuyenDung = "Biên chế";
                }
                else
                {
                    TuyenDung = "Thời vụ";
                }
            }
        }


        private GIAOVIEN _selectedItem;
        public GIAOVIEN SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                reset();

                if (SelectedItem != null)
                {
                    MaGV = SelectedItem.MaGV;
                    HoDem = SelectedItem.HoDem;
                    TenGV = SelectedItem.TenGV;
                    NgaySinh = SelectedItem.NgaySinh.Value;
                    SoCCCD = SelectedItem.SoCCCD;
                    NoiCT = SelectedItem.NoiCT;
                    GioiTinh = SelectedItem.GioiTinh.Value;
                    Phone = SelectedItem.Phone;
                    TuyenDung = SelectedItem.TuyenDung;

                    int index = 0;

                    foreach (var item in ListCity)
                    {
                        if (item.name.CompareTo(NoiCT) == 0)
                        {
                            SelectedIndexNoiCT = index;
                            NoiCT = item.name;
                            break;
                        }

                        index++;
                    }

                    if (TuyenDung.CompareTo("Hợp đồng") == 0)
                    {
                        SelectedIndexTD = 0;
                    }
                    else if (TuyenDung.CompareTo("Biên chế") == 0)
                    {
                        SelectedIndexTD = 1;
                    }
                    else
                    {
                        SelectedIndexTD = 2;
                    }

                    TrinhDo_VH = SelectedItem.TrinhDo_VH;
                    TrinhDo_SP = SelectedItem.TrinhDo_SP;


                    if (!string.IsNullOrEmpty(SelectedItem.GV_LT.ToString()))
                    {
                        GV_LT = SelectedItem.GV_LT.Value;
                        IsCheckedGV_LT = GV_LT;
                    }

                    if (!string.IsNullOrEmpty(SelectedItem.GV_TH.ToString()))
                    {
                        GV_TH = SelectedItem.GV_TH.Value;
                        IsCheckedGV_TH = GV_TH;
                    }
                    TrinhDo_CM = SelectedItem.TrinhDo_CM;
                    Nganh_CM = SelectedItem.Nganh_CM;
                    SoGCN = SelectedItem.SoGCN;
                    MaSoGTVT = SelectedItem.MaSoGTVT;
                    MaCSDT = SelectedItem.MaCSDT;
                    TrangThai = SelectedItem.TrangThai.Value;
                    AnhCD = SelectedItem.AnhCD;
                    SelectedImage = AnhCD;
                }
            }
        }

        public ICommand selectionChanged { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand viewInsertTeacher { get; set; }
        public ICommand closeTeacherWindow { get; set; }
        public ICommand closeInsertTeacherWindow { get; set; }
        public ICommand selectedImage { get; set; }
        public ICommand insertTeacher { get; set; }
        public ICommand editTeacher { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }

        public TeacherViewModel()
        {
            ListTeacher = new ObservableCollection<GIAOVIEN>();
            ListCity = new List<province_city>();
            NgaySinh = DateTime.Now;
            GioiTinh = 1;
            CurrentPage = 1;

            teachers();
            cities();

            //open window edit teacher
            previewMouseLeftButtonUp = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                EditTeacherWindow teacher = new EditTeacherWindow();
                teacher.ShowDialog();
                p.ShowDialog();

            });

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
                reset();

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
            insertTeacher = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    var check = validation();

                    var checkCSDT = checkExists(MaCSDT);

                    if (!checkCSDT)
                    {
                        check = false;
                        MessageBox.Show("Giáo viên hiện tại đang dạy ở cơ sở khác!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    var checkSoGCN = checkExistsSoGCN(SoGCN);

                    if (!checkSoGCN)
                    {
                        check = false;
                        MessageBox.Show("Số giấy chứng nhận không tồn tại hoặc đang thuộc sở hữu của giáo viên khác!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    var checkCCCD = checkExists(SoCCCD);

                    if (!checkCCCD)
                    {
                        check = false;
                        MessageBox.Show("Số CCCD/CMND đã tồn tại!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                    if (check)
                    {
                        GIAOVIEN gv = new GIAOVIEN()
                        {
                            MaGV = MaGV.Trim(),
                            HoDem = HoDem.Trim(),
                            TenGV = TenGV.Trim(),
                            NgaySinh = NgaySinh,
                            SoCCCD = SoCCCD.Trim(),
                            NoiCT = NoiCT.Trim(),
                            GioiTinh = GioiTinh,
                            Phone = Phone.Trim(),
                            TuyenDung = TuyenDung.Trim(),
                            TrinhDo_SP = TrinhDo_SP.Trim(),
                            TrinhDo_CM = TrinhDo_CM.Trim(),
                            TrinhDo_VH = TrinhDo_VH.Trim(),
                            Nganh_CM = Nganh_CM.Trim(),
                            GV_LT = GV_LT,
                            GV_TH = GV_TH,
                            SoGCN = SoGCN.Trim(),
                            MaSoGTVT = MaSoGTVT.Trim(),
                            MaCSDT = MaCSDT.Trim(),
                            TrangThai = TrangThai,
                            AnhCD = AnhCD,
                            NgayCapNhat = DateTime.Now,
                        };

                        insert(gv);

                        p.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Thêm giáo viên thất bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            //edit teacher 
            editTeacher = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new giaothongEntities())
                {
                    try
                    {
                        var check = validation();

                        var kh = db.GIAOVIENs.Find(MaGV.Trim());

                        if (kh != null)
                        {
                            kh.HoDem = HoDem.Trim();
                            kh.TenGV = TenGV.Trim();
                            kh.NgaySinh = NgaySinh;
                            kh.SoCCCD = SoCCCD.Trim();
                            kh.NoiCT = NoiCT.Trim();
                            kh.GioiTinh = GioiTinh;
                            kh.Phone = Phone.Trim();
                            kh.TuyenDung = TuyenDung.Trim();
                            kh.TrinhDo_SP = TrinhDo_SP.Trim();
                            kh.TrinhDo_CM = TrinhDo_CM.Trim();
                            kh.TrinhDo_VH = TrinhDo_VH.Trim();
                            kh.Nganh_CM = Nganh_CM.Trim();
                            kh.GV_LT = GV_LT;
                            kh.GV_TH = GV_TH;
                            kh.SoGCN = SoGCN.Trim();
                            kh.MaSoGTVT = MaSoGTVT.Trim();
                            kh.MaCSDT = MaCSDT.Trim();
                            TrangThai = TrangThai;

                            var checkFile = AnhCD.Contains("giaothong");

                            if (checkFile)
                            {
                                var index = AnhCD.IndexOf("Images");
                                AnhCD = AnhCD.Remove(0, index - 1);
                            }   
                            
                            kh.AnhCD = AnhCD;


                            kh.NgayCapNhat = DateTime.Now;
                            db.SaveChanges();
                            teachers();

                            p.Close();
                        }
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                    {
                        MessageBox.Show("Sửa thông tin giáo viên thất bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public bool validation()
        {
            bool check = true;


            var checkBirthDay = checkTypeDate(DateTime.Parse(NgaySinh.ToString()));

            if (!checkBirthDay)
            {
                check = false;
                MessageBox.Show("Giáo viên phải đủ từ 15 tuổi trở lên!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (!checkLength(Phone.Trim(), 14))
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

            if (!checkLength(TrinhDo_CM, 10))
            {
                check = false;
                MessageBox.Show("Trình độ chuyên môn không hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            return check;
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
            var dateNow = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");

            if (ListTeacher.Count > 0)
            {
                var maxMaGV = ListTeacher.Max(p => p.MaGV).Trim();
                var arrMaGV = maxMaGV.Split('-');

                if (arrMaGV.Length > 0)
                {
                    MaGV = arrMaGV[0] + "-" + dateNow + "-" + (Int32.Parse(arrMaGV[2]) + 1);
                }
            }
            else
            {
                MaGV = "52-" + dateNow + "-1";
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

        //get cities
        public List<province_city> cities()
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    var teachers = from ct in db.province_city select ct;

                    teachers.ToList().ForEach(p =>
                    {
                        province_city ct = new province_city
                        {
                            matp = p.matp,
                            name = p.name,
                            type = p.type,
                            slug = p.slug,
                        };

                        ListCity.Add(ct);
                    });
                }
                catch { }
            }

            return ListCity;
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

        public void reset()
        {
            TenGV = null;
            HoDem = null;
            SoCCCD = null;
            NoiCT = null;
            GioiTinh = 0;
            Phone = null;
            TuyenDung = null;
            TrinhDo_VH = null;
            TrinhDo_CM = null;
            Nganh_CM = null;
            TrinhDo_SP = null;
            GV_LT = false;
            GV_TH = false;
            SoGCN = null;
            MaSoGTVT = null;
            MaCSDT = null;
            TrangThai = false;
            AnhCD = null;
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
